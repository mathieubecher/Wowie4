using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

namespace CharacterFSM
{
    public class DashState : VirtualState
    {
        [SerializeField] private AnimationCurve m_dashDynamic;
        [SerializeField] private float m_dashCooldown = 0.2f;
        [SerializeField] private float m_dashInvulnerability = 0.5f;
        
        private float m_dashTimer = 0f;
        private float m_previousRelativeOffset = 0f;
        private float m_direction = 1f;
        private bool m_forceExit = false;
        
        protected override void StateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            m_dashTimer = 0f;
            m_previousRelativeOffset = 0.0f;
            m_forceExit = false;
            
            float moveInput = m_character.GetMoveInput();
            float currentVelocity = m_character.GetCurrentVelocity().x;
            m_direction = math.abs(moveInput) > Character.EPSILON ? math.sign(moveInput) 
                        : math.abs(currentVelocity) > Character.EPSILON ? math.sign(currentVelocity)
                        : math.sign(m_character.body.localScale.x);
            
            m_character.body.localScale = new Vector3(m_direction,1.0f,1.0f);
            m_character.lifeManager.SetInvulnerability(true);
            
            m_animator.SetInteger(DashInAir, m_animator.GetInteger(DashInAir) + 1);
        }
        
        public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if (m_forceExit) return;
            
            float maxTime = m_dashDynamic.keys[m_dashDynamic.length-1].time;

            if (m_dashInvulnerability < m_dashTimer && m_dashInvulnerability - m_dashTimer <= Time.deltaTime)
            {
                m_character.lifeManager.SetInvulnerability(false);
            }
            
            m_dashTimer += Time.deltaTime;
            
            if (m_dashTimer >= maxTime)
            {
                m_animator.SetTrigger(ForceExitState);
                m_forceExit = true;
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
            
            m_character.lifeManager.SetInvulnerability(false);
            m_animator.ResetTrigger(ForceExitState);
            m_animator.ResetTrigger(Dash);
            m_animator.SetBool(DashCoolDown, true);
            
            m_character.StartCoroutine(Cooldown());
        }

        private IEnumerator Cooldown()
        {
            yield return new WaitForSeconds(m_dashCooldown);
            m_animator.SetBool(DashCoolDown, false);
        }
    }
}