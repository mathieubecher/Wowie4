using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CharacterFSM
{
    public abstract class VirtualState : StateMachineBehaviour
    {
        protected Character m_character;
        protected Animator m_animator;
        
        protected static readonly int ForceExitState = Animator.StringToHash("ForceExitState");
        protected static readonly int DashInAir = Animator.StringToHash("dashInAir");
        protected static readonly int DashCoolDown = Animator.StringToHash("dashCooldown");
        protected static readonly int Dash = Animator.StringToHash("Dash");
        protected static readonly int IsGrabing = Animator.StringToHash("isGrabing");
        
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