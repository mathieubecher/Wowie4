using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CharacterFSM
{
    public class JumpState : StateMachineBehaviour
    { 
        private Character m_character;
        private float m_jumpTimer;
        private float m_jumpReleaseTime;
        
        [SerializeField] private AnimationCurve m_jumpReleaseTimeToExitTime;
        [SerializeField] private AnimationCurve m_jumpVerticalDynamic;
        [SerializeField] private float m_minJumpTime = 0.1f;
        override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if (!m_character) m_character = animator.GetComponent<Character>();
            
            m_jumpTimer = 0f;
            m_jumpReleaseTime = 1f;
            
            InputController.OnReleaseJump += ReleaseJump;

        }

        override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            m_jumpTimer += Time.deltaTime;
        }
        override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            InputController.OnReleaseJump -= ReleaseJump;
        }

        private void ReleaseJump()
        {
            m_jumpReleaseTime = m_jumpTimer;
        }
    }
}

