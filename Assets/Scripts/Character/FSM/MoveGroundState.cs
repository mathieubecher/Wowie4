using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

namespace CharacterFSM
{
    public class MoveGroundState : VirtualState
    {
        protected override void StateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            animator.ResetTrigger(Jump);
            animator.ResetTrigger(Dash);
            m_animator.SetInteger(DashInAir, 0);
        }

        public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            float moveInput = m_character.GetMoveInput();
            m_character.body.localScale = new Vector3(math.abs(moveInput) > Character.EPSILON? moveInput : m_character.body.localScale.x,1.0f,1.0f);
        }
        public override void OnFixedUpdate()
        {
            float moveInput = m_character.GetMoveInput();
            m_character.SetDesiredVelocity(Vector2.right * (moveInput * m_character.maxSpeed));
        }

        // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
         protected override void StateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            
        }
    }
}
