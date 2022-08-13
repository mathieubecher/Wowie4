using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CharacterFSM
{
    public class FallState : VirtualState
    {
        [SerializeField] private float m_lossOfControl = 0.2f;

        protected override void StateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            
        }

        override public void OnFixedUpdate()
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