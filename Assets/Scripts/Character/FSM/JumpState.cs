using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Mathematics;
using Random = UnityEngine.Random;

namespace CharacterFSM
{
    public class JumpState : VirtualState
    { 
        private float m_jumpTimer;
        private float m_jumpReleaseTime;
        private float m_previousRelativeHeight = 0.0f;
        private float m_gravityScaleAtStart = 0.0f;
        private bool m_forceExit = false;
        
        [SerializeField] private AnimationCurve m_jumpReleaseTimeToExitTime;
        [SerializeField] private AnimationCurve m_jumpVerticalDynamic;
        [SerializeField] private AnimationCurve m_lossOfControl;
        [SerializeField] private float m_minJumpTime = 0.1f;
        
        [Header("Audio")] 
        [SerializeField] private List<AudioClip> m_jumpSoundsAtStart;
        protected override void StateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            m_animator.ResetTrigger(ForceExitState);
            m_jumpTimer = 0f;
            m_jumpReleaseTime = 10f;
            m_forceExit = false;
            
            InputController.OnJump += ReleaseJump;
            m_character.detectPhysics.OnHitRoof += HitRoof;
            m_previousRelativeHeight = 0.0f;

            m_gravityScaleAtStart = m_character.GetGravityScaler();
            m_character.SetGravityScaler(0.0f);
            
            int randomSoundId = (int)math.floor(Random.Range(0, m_jumpSoundsAtStart.Count));
            m_character.audio.PlayOneShot(m_jumpSoundsAtStart[randomSoundId]);
        }
        public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if (m_forceExit) return;
            m_jumpTimer += Time.deltaTime;
            
            float maxTime = m_jumpVerticalDynamic.keys[m_jumpVerticalDynamic.length-1].time;
            float exitTime = m_jumpReleaseTimeToExitTime.Evaluate(m_jumpReleaseTime);
            
            if ((m_jumpTimer > exitTime && m_jumpTimer > m_minJumpTime) || m_jumpTimer > maxTime)
            {
                m_animator.SetTrigger(ForceExitState);
                m_forceExit = true;
            }
        }

        public override void OnFixedUpdate()
        {
            float desiredRelativeHeight = m_jumpVerticalDynamic.Evaluate(m_jumpTimer) ;
            float desiredVerticalSpeed = Time.deltaTime > 0.0f ? (desiredRelativeHeight - m_previousRelativeHeight) / Time.deltaTime : 0.0f;

            float desiredHorizontalSpeed = m_character.GetMoveInput() * m_character.maxSpeed;
            desiredHorizontalSpeed = math.lerp(m_character.GetCurrentVelocity().x, desiredHorizontalSpeed, m_lossOfControl.Evaluate(m_jumpTimer));
            
            m_character.SetDesiredVelocity(new Vector2(desiredHorizontalSpeed, desiredVerticalSpeed), false);
            m_previousRelativeHeight = desiredRelativeHeight;
            
            m_character.body.localScale = new Vector3(math.abs(desiredHorizontalSpeed) > Character.EPSILON? math.sign(desiredHorizontalSpeed) : m_character.body.localScale.x,1.0f,1.0f);
        }
        
        protected override void StateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            InputController.OnJump -= ReleaseJump;
            m_character.detectPhysics.OnHitRoof -= HitRoof;
            
            float maxTime = m_jumpVerticalDynamic.keys[m_jumpVerticalDynamic.length-1].time;
            if (m_forceExit && m_jumpTimer < maxTime)
            {
                m_character.SetDesiredVelocity(new Vector2(m_character.GetCurrentVelocity().x, 0.0f), false);
            }
            m_animator.ResetTrigger(ForceExitState);
            m_character.SetGravityScaler(m_gravityScaleAtStart);
        }

        private void ReleaseJump(bool _enable)
        {
            if (_enable) return;
            m_jumpReleaseTime = m_jumpTimer;
        }

        private void HitRoof()
        {
            m_animator.SetTrigger(ForceExitState);
        }
    }
}

