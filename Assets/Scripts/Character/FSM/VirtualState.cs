using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CharacterFSM
{
    public abstract class VirtualState : StateMachineBehaviour
    {
        protected Character m_character;
        protected Animator m_animator;
        // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
        public sealed override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if (!m_character) m_character = animator.GetComponent<Character>();
            if (!m_animator) m_animator = animator.GetComponent<Animator>();
            m_character.SetState(this);
            StateEnter(animator, stateInfo, layerIndex);
        }

        protected virtual void StateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex){}

        public virtual void OnFixedUpdate(){}
        
        public sealed override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            StateExit(animator, stateInfo, layerIndex);
        }
        protected virtual void StateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex){}
    }
}