using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

namespace EnemyFSM
{
    
    public class FollowOnGroundState :  VirtualState
    {
        [SerializeField] private float m_speed = 5.0f;
        [SerializeField] private float m_smoothFactor = 0.05f;
        
        //protected override void StateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        //{
        //
        //}

        //public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        //{
        //    
        //}
        
        public override void OnFixedUpdate()
        {
            float currentLinearDirection = m_enemy.GetCurrentVelocity().x;
            float currentLinearSpeed = math.abs(currentLinearDirection);
            currentLinearDirection = math.sign(currentLinearDirection);
            
            
            float desiredLinearDirection = (m_enemy.target.position - m_enemy.transform.position).x;
            float distance = math.abs(desiredLinearDirection);
            desiredLinearDirection = math.sign(desiredLinearDirection);
            if (distance < 0.1f || distance < m_speed * Time.deltaTime)
            {
                m_enemy.SetDesiredVelocity(Vector2.zero, true);
                return;
            }

            float desiredLinearVelocity = math.lerp(currentLinearDirection * currentLinearSpeed, desiredLinearDirection * m_speed, m_smoothFactor);
            m_enemy.SetDesiredVelocity(new Vector2(desiredLinearVelocity, 0.0f), true);
            
            m_enemy.body.localScale = new Vector3(math.abs(desiredLinearVelocity) > Character.EPSILON? math.sign(desiredLinearVelocity) * math.abs( m_enemy.body.localScale.x) : m_enemy.body.localScale.x, m_enemy.body.localScale.y, m_enemy.body.localScale.z);

        }

        //protected override void StateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        //{
        //    
        //}
    }
}