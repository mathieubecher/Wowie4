using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.PlayerLoop;

namespace CharacterFSM
{
    public class MoveGroundState : VirtualState
    {
        [Serializable]
        private enum MoveState
        {
            START,
            STOP,
            UPDATE,
            TURN
        }
        
        [SerializeField] private AnimationCurve m_startCurve; 
        [SerializeField] private AnimationCurve m_stopCurve; 
        [SerializeField] private AnimationCurve m_turnCurve;

        private float m_lastDir;
        private float m_stateTimer;
        [SerializeField] private MoveState m_state;
        private AnimationCurve m_currentCurve;
        private float m_turnDirection;
        protected override void StateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if(animator.GetFloat("TriggerJumpBuffer") > 0.2f) animator.ResetTrigger(Jump);
            animator.ResetTrigger(Dash);
            m_animator.SetInteger(DashInAir, 0);
            
            UpdateMoveState();
        }

        private void UpdateMoveState()
        {
            
            float moveInput = m_character.GetMoveInput();
            float velocityInput = m_character.GetCurrentVelocity().x / m_character.maxSpeed;
            if (math.abs(moveInput) < 0.3f && (m_state is MoveState.START or MoveState.STOP || math.abs(velocityInput) > 0.3f))
            {
                if (m_state != MoveState.STOP)
                {
                    m_state = MoveState.STOP;
                    m_currentCurve = m_stopCurve;
                    m_stateTimer = GetCurveTimeForValue(m_currentCurve, math.abs(velocityInput),20, true);
                    
                }
            }
            else if (math.abs(moveInput) > 0.3f && (m_state == MoveState.TURN || math.abs(velocityInput) > 0.3f && math.abs(math.sign(velocityInput) - math.sign(moveInput)) > Character.EPSILON))
            {
                if (m_state != MoveState.TURN || Math.Abs(m_turnDirection - math.sign(moveInput)) > Character.EPSILON)
                {
                    m_state = MoveState.TURN;
                    m_currentCurve = m_turnCurve;
                    m_turnDirection = math.sign(moveInput);
                    m_stateTimer = GetCurveTimeForValue(m_currentCurve, velocityInput * math.sign(moveInput),20);
                }
            }
            else if((m_state is MoveState.START or MoveState.STOP || math.abs(velocityInput) < 0.1f) && math.abs(moveInput) > 0.3f)
            {
                if (m_state != MoveState.START && m_state != MoveState.TURN)
                {
                    m_state = MoveState.START;
                    m_currentCurve = m_startCurve;
                    m_stateTimer = GetCurveTimeForValue(m_currentCurve, math.abs(velocityInput),20);
                }
            }
            else if(m_state != MoveState.UPDATE)
            {
                m_state = MoveState.UPDATE;
                m_stateTimer = 0.0f;
            }
            
        }

        public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            float moveInput = m_character.GetMoveInput();
            m_character.body.localScale = new Vector3(math.abs(moveInput) > Character.EPSILON? moveInput : m_character.body.localScale.x,1.0f,1.0f);
        }
        public override void OnFixedUpdate()
        {
            float moveInput = m_character.GetMoveInput();
            UpdateMoveState();
            if (m_state != MoveState.UPDATE && m_stateTimer > m_currentCurve.keys[m_currentCurve.length - 1].time)
            {
                m_state = MoveState.UPDATE;
            }
            
            m_stateTimer += Time.deltaTime;
            float desiredVelocity = moveInput * m_character.maxSpeed;
            if (m_state != MoveState.UPDATE)
            {
                float direction = math.abs(moveInput) > Character.EPSILON ? math.sign(moveInput) : math.sign(m_character.GetCurrentVelocity().x);
                desiredVelocity = m_currentCurve.Evaluate(m_stateTimer) * m_character.maxSpeed * direction;
            }
            
            m_character.SetDesiredVelocity(Vector2.right * desiredVelocity);
        }

        // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
        protected override void StateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            
        }
         
        private float GetCurveTimeForValue( AnimationCurve curveToCheck, float value, int accuracy, bool down = false) {

             float startTime = curveToCheck.keys [0].time;
             float endTime = curveToCheck.keys [curveToCheck.length - 1].time;
             float nearestTime = startTime;
             float step = endTime - startTime;

             for (int i = 0; i < accuracy; i++) {

                 float valueAtNearestTime = curveToCheck.Evaluate (nearestTime);
                 float distanceToValueAtNearestTime = Mathf.Abs (value - valueAtNearestTime);

                 float timeToCompare = nearestTime + step;
                 float valueAtTimeToCompare = curveToCheck.Evaluate (timeToCompare);
                 float distanceToValueAtTimeToCompare = Mathf.Abs (value - valueAtTimeToCompare);

                 if ((!down && distanceToValueAtTimeToCompare < distanceToValueAtNearestTime) || (down && distanceToValueAtTimeToCompare > distanceToValueAtNearestTime)) {
                     nearestTime = timeToCompare;
                     valueAtNearestTime = valueAtTimeToCompare;
                 }
                 step = Mathf.Abs(step * 0.5f) * Mathf.Sign(value-valueAtNearestTime);
             }

             return nearestTime;
        }
    }
}
