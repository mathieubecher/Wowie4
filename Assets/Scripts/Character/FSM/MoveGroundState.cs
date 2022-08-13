using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CharacterFSM
{
    public class MoveGroundState : VirtualState
    {
        protected override void StateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            animator.ResetTrigger("Jump");
        }

        public override void OnFixedUpdate()
        {
            float moveInput = m_character.GetMoveInput();
            m_character.SetDesiredVelocity(Vector2.right * (moveInput * m_character.maxSpeed));
        }

        // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
        //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        //{
        //    
        //}
    }
}
