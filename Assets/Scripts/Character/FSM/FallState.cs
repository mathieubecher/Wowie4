using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CharacterFSM
{
    public class FallState : StateMachineBehaviour
    {
        private Character m_character;

        [SerializeField] private float m_lossOfControl = 0.2f;

        override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if (!m_character) m_character = animator.GetComponent<Character>();
        }

        override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            float moveInput = m_character.GetMoveInput();
            
            float desiredHorizontalSpeed = m_character.GetMoveInput() * m_character.maxSpeed;
            float diff = desiredHorizontalSpeed - m_character.GetCurrentVelocity().x;
            desiredHorizontalSpeed = m_character.GetCurrentVelocity().x + diff * m_lossOfControl;

            m_character.SetDesiredVelocity(Vector2.right * desiredHorizontalSpeed);
        }
        // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
        //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        //{
        //    
        //}
    }
}