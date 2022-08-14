using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

namespace EnemyFSM
{
    public class FollowInAirState : VirtualState
    {
        [SerializeField] private float m_speed;
        [SerializeField] private float m_smoothFactor = 0.05f;
        
        private float m_gravityScaleAtStart = 0.0f;
        protected override void StateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            m_gravityScaleAtStart = m_enemy.GetGravityScaler();
            m_enemy.SetGravityScaler(0.0f);
        }

        public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            
        }
        
        public override void OnFixedUpdate()
        {
            Vector2 currentDirection = m_enemy.GetCurrentVelocity();
            float currentSpeed = currentDirection.magnitude;
            currentDirection.Normalize();
            
            
            Vector2 desiredDirection = m_enemy.target.position - m_enemy.transform.position;
            float distance = desiredDirection.magnitude;
            desiredDirection.Normalize();

            Vector2 desiredVelocity = Vector2.Lerp(currentDirection * currentSpeed, desiredDirection * m_speed, m_smoothFactor);
            m_enemy.SetDesiredVelocity(desiredVelocity, false);
            
            m_enemy.body.localScale = new Vector3(math.abs(desiredVelocity.x) > Character.EPSILON? math.sign(desiredVelocity.x) * math.abs( m_enemy.body.localScale.x) : m_enemy.body.localScale.x, m_enemy.body.localScale.y, m_enemy.body.localScale.z);

        }

        //protected override void StateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        //{
        //    
        //}
    }
}
