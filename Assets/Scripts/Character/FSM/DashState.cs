using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

namespace CharacterFSM
{
    public class DashState : VirtualState
    {
        [SerializeField] private AnimationCurve m_dashDynamic;
        
        private float m_dashTimer = 0f;
        private float m_previousRelativeOffset = 0f;
        private float m_direction = 1f;

        protected override void StateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            m_dashTimer = 0f;
            m_previousRelativeOffset = 0.0f;
            
            float moveInput = m_character.GetMoveInput();
            float currentVelocity = m_character.GetCurrentVelocity().x;
            m_direction = math.abs(moveInput) > Character.EPSILON ? math.sign(moveInput) 
                        : math.abs(currentVelocity) > Character.EPSILON ? math.sign(currentVelocity)
                        : math.sign(m_character.body.localScale.x);
        }
        
        public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            m_dashTimer += Time.deltaTime;
            float maxTime = m_dashDynamic.keys[m_dashDynamic.length-1].time;
            
            if (m_dashTimer >= maxTime)
            {
                m_animator.SetBool("forceExitState", true);
            }
        }

        public override void OnFixedUpdate()
        {
            float desiredRelativeOffset = m_dashDynamic.Evaluate(m_dashTimer) ;
            float desiredHorizontalSpeed = Time.deltaTime > 0.0f ? (desiredRelativeOffset - m_previousRelativeOffset) / Time.deltaTime : 0.0f;
            
            m_character.SetDesiredVelocity(new Vector2(desiredHorizontalSpeed * m_direction, 0.0f), false);
            m_previousRelativeOffset = desiredRelativeOffset;
        }
        
        protected override void StateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            m_character.SetDesiredVelocity(new Vector2(0.0f, 0.0f), false);
            animator.SetBool("forceExitState", false);
        }
    }
}