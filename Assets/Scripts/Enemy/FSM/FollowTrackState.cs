using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EnemyFSM
{
    public class FollowTrackState : VirtualState
    {
        [SerializeField] private float m_speed;
        private float m_gravityScaleAtStart = 0.0f;
        
        protected override void StateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            m_gravityScaleAtStart = m_enemy.GetGravityScaler();
            m_enemy.SetGravityScaler(0.0f);
        }

        //public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        //{
            
        //}
                
        public override void OnFixedUpdate()
        {
            m_enemy.SetDesiredVelocity(m_enemy.track.GetVelocity(m_enemy.transform.position, m_speed));
        }
        protected override void StateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            m_enemy.SetGravityScaler(m_gravityScaleAtStart);    
        }
    }
}