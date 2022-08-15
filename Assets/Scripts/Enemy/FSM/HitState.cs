using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

namespace EnemyFSM
{
    public class HitState : VirtualState
    {
    
        private float m_gravityScaleAtStart = 0.0f;
        
        [Header("Sound")]
        [SerializeField] private List<AudioClip> m_hitSoundsAtStart;
        
        protected override void StateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            m_gravityScaleAtStart = m_enemy.GetGravityScaler();
            m_enemy.SetGravityScaler(0.0f);
            
            m_enemy.SetDesiredVelocity(Vector2.zero, false);
            
            int randomSoundId = (int)math.floor(Random.Range(0, m_hitSoundsAtStart.Count));
            m_enemy.audio.PlayOneShot(m_hitSoundsAtStart[randomSoundId]);
        }

        //override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        //{
        //    
        //}
        
        //public override void OnFixedUpdate()
        //{
            
        //}

        protected override void StateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            
            m_enemy.SetGravityScaler(m_gravityScaleAtStart);
        }
    }
}
