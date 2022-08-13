using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CharacterFSM
{
    public class MoveGroundState : StateMachineBehaviour
    {
        private Character m_character;

        [SerializeField] private float m_speed = 5.0f;
        override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if (!m_character) m_character = animator.GetComponent<Character>();
            animator.ResetTrigger("Jump");
        }

        override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
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
