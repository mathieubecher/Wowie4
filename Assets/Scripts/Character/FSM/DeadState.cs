using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CharacterFSM
{
    public class DeadState : VirtualState
    {
        protected override void StateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if( m_animator.GetBool("grabRobot")) m_character.DropRobot(true);
        }

        override public void OnFixedUpdate()
        {
            if (m_animator.GetBool("isOnGround"))
            {
                m_character.SetDesiredVelocity(Vector2.zero);
            }
        }
        // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
        //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        //{
        //    
        //}
    }
}
