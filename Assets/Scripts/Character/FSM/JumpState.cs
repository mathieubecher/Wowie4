using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CharacterFSM
{
    public class JumpState : StateMachineBehaviour
    { 
        private Character m_character;
        private float m_jumpTimer;
        private float m_jumpReleaseTime;
        private float m_previousRelativeHeight = 0.0f;
        private float m_gravityScaleAtStart = 0.0f;
        
        [SerializeField] private AnimationCurve m_jumpReleaseTimeToExitTime;
        [SerializeField] private AnimationCurve m_jumpVerticalDynamic;
        [SerializeField] private AnimationCurve m_lossOfControl;
        [SerializeField] private float m_minJumpTime = 0.1f;
        override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if (!m_character) m_character = animator.GetComponent<Character>();
            
            m_jumpTimer = 0f;
            m_jumpReleaseTime = 1f;
            
            InputController.OnReleaseJump += ReleaseJump;
            m_previousRelativeHeight = 0.0f;

            m_gravityScaleAtStart = m_character.GetGravityScaler();
            m_character.SetGravityScaler(0.0f);
        }

        override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            m_jumpTimer += Time.deltaTime;

            float desiredRelativeHeight = m_jumpVerticalDynamic.Evaluate(m_jumpTimer) ;
            float desiredVerticalSpeed = Time.deltaTime > 0.0f ? (desiredRelativeHeight - m_previousRelativeHeight) / Time.deltaTime : 0.0f;

            float desiredHorizontalSpeed = m_character.GetMoveInput() * m_character.maxSpeed;
            float diff = desiredHorizontalSpeed - m_character.GetCurrentVelocity().x;
            desiredHorizontalSpeed = m_character.GetCurrentVelocity().x + diff * m_lossOfControl.Evaluate(m_jumpTimer);
            
            m_character.SetDesiredVelocity(new Vector2(desiredHorizontalSpeed, desiredVerticalSpeed), false);
            m_previousRelativeHeight = desiredRelativeHeight;
    
            float maxTime = m_jumpVerticalDynamic.keys[m_jumpVerticalDynamic.length-1].time;
            float exitTime = m_jumpReleaseTimeToExitTime.Evaluate(m_jumpReleaseTime) * maxTime;
            if ((m_jumpTimer > exitTime && m_jumpTimer > m_minJumpTime) || m_jumpTimer > maxTime)
            {
                Debug.Log(m_jumpTimer + " > " + exitTime + " && " + m_minJumpTime + " || " + maxTime);
                animator.SetBool("forceExitJump", true);
            }
        }
        
        override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            InputController.OnReleaseJump -= ReleaseJump;
            animator.SetBool("forceExitJump", false);
            m_character.SetDesiredVelocity(new Vector2(m_character.GetCurrentVelocity().x, 0.0f), false);
            m_character.SetGravityScaler(m_gravityScaleAtStart);
        }

        private void ReleaseJump()
        {
            m_jumpReleaseTime = m_jumpTimer;
        }
    }
}
