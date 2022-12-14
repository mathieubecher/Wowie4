using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

namespace CharacterFSM
{
    public class GrabState : VirtualState
    {
        [SerializeField] private AnimationCurve m_grabDynamic;
        [SerializeField] private float m_exitDashSpeed = 4f;
        
        private float m_grabTimer = 0f;
        private float m_previousRelativeOffset = 0f;
        private Vector2 m_direction;
        private float m_distance;
        private bool m_forceExit = false;
        private Robot m_robotRef;
        
        protected override void StateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            m_grabTimer = 0f;
            m_previousRelativeOffset = 0.0f;
            m_forceExit = false;

            m_robotRef = m_character.detectRobot.robotRef;
            m_direction = m_robotRef.transform.position - m_character.transform.position;
            m_distance = m_direction.magnitude;
            
            m_direction.Normalize();
            
            if(math.abs(m_direction.x) > Character.EPSILON )
                m_character.body.localScale = new Vector3(math.sign(m_direction.x),1.0f,1.0f);

            m_animator.SetInteger(DashInAir, m_animator.GetInteger(DashInAir) + 1);
            m_animator.SetBool(IsGrabing, true);
            
            Character.EnablePlatform(false);
            m_character.lifeManager.SetInvulnerability(true);
            m_character.StartGrabRobot(m_robotRef);
        }
        
        public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if (m_forceExit) return;
            
            float maxTime = m_grabDynamic.keys[m_grabDynamic.length-1].time;
            
            m_grabTimer += m_distance > Character.EPSILON ? Time.deltaTime * m_character.detectRobot.maxDist / m_distance : 0.2f;
            
            if (m_grabTimer >= maxTime)
            {
                m_animator.SetTrigger(ForceExitState);
                m_forceExit = true;
            }
            
        }

        public override void OnFixedUpdate()
        {
            float desiredRelativeOffset = m_grabDynamic.Evaluate(m_grabTimer) ;
            float desiredHorizontalSpeed = Time.deltaTime > 0.0f ? (desiredRelativeOffset - m_previousRelativeOffset) / Time.deltaTime : 0.0f;
            desiredHorizontalSpeed *= m_distance;
            
            m_character.SetDesiredVelocity(m_direction * desiredHorizontalSpeed, false);
            m_previousRelativeOffset = desiredRelativeOffset;
        }
        
        protected override void StateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            m_character.SetDesiredVelocity( m_character.GetCurrentVelocity().normalized * m_exitDashSpeed, false);
            m_character.GrabRobot(m_robotRef);
            
            m_animator.ResetTrigger(ForceExitState);
            m_animator.ResetTrigger(Dash);
            
            m_animator.SetBool(IsGrabing, false);
            
            Character.EnablePlatform(true);
            m_character.lifeManager.SetInvulnerability(false);
        }

    }
}
