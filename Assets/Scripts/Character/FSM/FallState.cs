using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CharacterFSM
{
    public class FallState : StateMachineBehaviour
    {
        private Character m_character;

        [SerializeField] private AnimationCurve m_jumpVerticalDynamic;

        override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if (!m_character) m_character = animator.GetComponent<Character>();
        }

        override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {

        }
        // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
        //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        //{
        //    
        //}
    }
}