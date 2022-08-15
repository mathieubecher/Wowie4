using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.PlayerLoop;
using Random = UnityEngine.Random;

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

        [SerializeField] private MoveState m_state;
        
        [Header("Audio")] 
        [SerializeField] private List<AudioClip> m_touchGroundSounds;
        
        private float m_lastDir;
        private float m_stateTimer;
        private AnimationCurve m_currentCurve;
        private float m_turnDirection;
        protected override void StateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if(animator.GetFloat("TriggerJumpBuffer") > 0.2f) animator.ResetTrigger(Jump);
            animator.ResetTrigger(Dash);
            m_animator.SetInteger(DashInAir, 0);
            
            UpdateMoveState();
            
            int randomSoundId = (int)math.floor(Random.Range(0, m_touchGroundSounds.Count));
            m_character.audio.PlayOneShot(m_touchGroundSounds[randomSoundId]);
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
                    m_stateTimer = TimeFromValue(m_currentCurve, math.abs(velocityInput));
                    
                }
            }
            else if (math.abs(moveInput) > 0.3f && (m_state == MoveState.TURN || math.abs(velocityInput) > 0.1f && math.abs(math.sign(velocityInput) - math.sign(moveInput)) > Character.EPSILON))
            {
                if (m_state != MoveState.TURN || Math.Abs(m_turnDirection - math.sign(moveInput)) > Character.EPSILON)
                {
                    m_state = MoveState.TURN;
                    m_currentCurve = m_turnCurve;
                    m_turnDirection = math.sign(moveInput);
                    m_stateTimer = TimeFromValue(m_currentCurve, velocityInput * math.sign(moveInput));
                    Debug.Log(m_stateTimer + " " + velocityInput);
                }
            }
            else if((m_state is MoveState.START or MoveState.STOP || math.abs(velocityInput) < 0.1f) && math.abs(moveInput) > 0.3f)
            {
                if (m_state != MoveState.START && m_state != MoveState.TURN)
                {
                    m_state = MoveState.START;
                    m_currentCurve = m_startCurve;
                    m_stateTimer = TimeFromValue(m_currentCurve, math.abs(velocityInput));
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
            
            m_stateTimer += Time.deltaTime * (m_state == MoveState.STOP? -1f : 1f);
            if (m_state != MoveState.UPDATE)
            {
                if(m_state == MoveState.STOP && m_stateTimer < 0f)
                    m_state = MoveState.UPDATE;
                else if(m_stateTimer > m_currentCurve.keys[m_currentCurve.length - 1].time)
                    m_state = MoveState.UPDATE;
                    
            }
            
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
         
        public static float TimeFromValue(AnimationCurve c, float value, float precision = 1e-6f)
        {
            float minTime = c.keys[0].time;
            float maxTime = c.keys[c.keys.Length-1].time;
            float best = (maxTime + minTime) / 2;
            float bestVal = c.Evaluate(best);
            int it=0;
            const int maxIt = 1000;
            float sign = Mathf.Sign(c.keys[c.keys.Length-1].value -c.keys[0].value);
            while(it < maxIt && Mathf.Abs(minTime - maxTime) > precision) {
                if((bestVal - value) * sign > 0) {
                    maxTime = best;
                } else {
                    minTime = best;
                }
                best = (maxTime + minTime) / 2;
                bestVal = c.Evaluate(best);
                it++;
            }
            return best;
        }
    }
}
