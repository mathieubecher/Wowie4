using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EnemyFSM{
    public abstract class VirtualState : StateMachineBehaviour
    {
        protected Enemy m_enemy;
        protected Animator m_animator;
        
        protected static readonly int ForceExitState = Animator.StringToHash("ForceExitState");
        protected static readonly int Hit = Animator.StringToHash("Hit");
        
        public sealed override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if (!m_enemy) m_enemy = animator.GetComponent<Enemy>();
            if (!m_animator) m_animator = animator.GetComponent<Animator>();
            m_enemy.SetState(this);
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