using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Mathematics;

namespace CharacterFSM
{
    public class JumpState : VirtualState
    { 
        private float m_jumpTimer;
        private float m_jumpReleaseTime;
        private float m_previousRelativeHeight = 0.0f;
        private float m_gravityScaleAtStart = 0.0f;
        
        [SerializeField] private AnimationCurve m_jumpReleaseTimeToExitTime;
        [SerializeField] private AnimationCurve m_jumpVerticalDynamic;
        [SerializeField] private AnimationCurve m_lossOfControl;
        [SerializeField] private float m_minJumpTime = 0.1f;
        protected override void StateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            m_jumpTimer = 0f;
            m_jumpReleaseTime = 1f;
            
            InputController.OnReleaseJump += ReleaseJump;
            m_previousRelativeHeight = 0.0f;

            m_gravityScaleAtStart = m_character.GetGravityScaler();
            m_character.SetGravityScaler(0.0f);
        }

        public override void OnFixedUpdate()
        {
            m_jumpTimer += Time.deltaTime;

            float desiredRelativeHeight = m_jumpVerticalDynamic.Evaluate(m_jumpTimer) ;
            float desiredVerticalSpeed = Time.deltaTime > 0.0f ? (desiredRelativeHeight - m_previousRelativeHeight) / Time.deltaTime : 0.0f;

            float desiredHorizontalSpeed = m_character.GetMoveInput() * m_character.maxSpeed;
            desiredHorizontalSpeed = math.lerp(m_character.GetCurrentVelocity().x, desiredHorizontalSpeed, m_lossOfControl.Evaluate(m_jumpTimer));
            
            m_character.SetDesiredVelocity(new Vector2(desiredHorizontalSpeed, desiredVerticalSpeed), false);
            m_previousRelativeHeight = desiredRelativeHeight;
    
            float maxTime = m_jumpVerticalDynamic.keys[m_jumpVerticalDynamic.length-1].time;
            float exitTime = m_jumpReleaseTimeToExitTime.Evaluate(m_jumpReleaseTime) * maxTime;
            if ((m_jumpTimer > exitTime && m_jumpTimer > m_minJumpTime) || m_jumpTimer > maxTime)
            {
                m_animator.SetBool("forceExitJump", true);
            }
        }
        
        protected override void StateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            InputController.OnReleaseJump -= ReleaseJump;
            
            float maxTime = m_jumpVerticalDynamic.keys[m_jumpVerticalDynamic.length-1].time;
            if (m_jumpTimer < maxTime)
            {
                m_character.SetDesiredVelocity(new Vector2(m_character.GetCurrentVelocity().x, 0.0f), false);
            }
            animator.SetBool("forceExitJump", false);
            m_character.SetGravityScaler(m_gravityScaleAtStart);
        }

        private void ReleaseJump()
        {
            m_jumpReleaseTime = m_jumpTimer;
        }
    }
}

