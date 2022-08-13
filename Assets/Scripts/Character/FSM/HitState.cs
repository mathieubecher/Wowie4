using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

namespace CharacterFSM
{
    public class HitState : VirtualState
    {
        [SerializeField] private AnimationCurve m_lossOfControlInAir;
        [SerializeField] private AnimationCurve m_lossOfControlOnGround;
        private bool m_forceExit = false;
        private float m_hitTimer = 0.0f;
        
        [Header("Audio")] 
        [SerializeField] private List<AudioClip> m_hitSoundsAtStart;
        protected override void StateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if( m_animator.GetBool("grabRobot")) m_character.DropRobot();
            m_animator.ResetTrigger(ForceExitState);
            m_forceExit = false;
            m_hitTimer = 0.0f;
            
            int randomSoundId = (int)math.floor(Random.Range(0, m_hitSoundsAtStart.Count));
            m_character.audio.PlayOneShot(m_hitSoundsAtStart[randomSoundId]);
        }
        public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if (m_forceExit) return;
            m_hitTimer += Time.deltaTime;
            float maxTime = math.min(m_lossOfControlInAir.keys[m_lossOfControlInAir.length-1].time,
                                    m_lossOfControlOnGround.keys[m_lossOfControlOnGround.length-1].time);
            
            if (m_hitTimer > maxTime)
            {
                m_animator.SetTrigger(ForceExitState);
                m_forceExit = true;
            }
        }
        override public void OnFixedUpdate()
        {

            var lossOfControl = m_animator.GetBool("isOnGround") ? m_lossOfControlOnGround : m_lossOfControlInAir;
            float desiredHorizontalSpeed = m_character.GetMoveInput() * m_character.maxSpeed;
            desiredHorizontalSpeed = math.lerp(m_character.GetCurrentVelocity().x, desiredHorizontalSpeed, lossOfControl.Evaluate(m_hitTimer));
            
            m_character.SetDesiredVelocity(new Vector2(desiredHorizontalSpeed, 0.0f));
        }
        
        protected override void StateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            m_animator.ResetTrigger(ForceExitState);
        }
        // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
        //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        //{
        //    
        //}
    }
}
