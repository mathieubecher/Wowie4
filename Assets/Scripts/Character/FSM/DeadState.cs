using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

namespace CharacterFSM
{
    public class DeadState : VirtualState
    {
        [Header("Audio")]
        [SerializeField] private List<AudioClip> m_deathSoundsAtStart;

        protected override void StateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if( m_animator.GetBool("grabRobot")) m_character.DropRobot(true);

            int randomSoundId = (int)math.floor(Random.Range(0, m_deathSoundsAtStart.Count));
            m_character.audio.PlayOneShot(m_deathSoundsAtStart[randomSoundId]);
        }

        override public void OnFixedUpdate()
        {
            if (m_animator.GetBool("isOnGround"))
            {
                m_character.SetDesiredVelocity(Vector2.zero);
            }
        }
        // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
        //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        //{
        //    
        //}
    }
}
