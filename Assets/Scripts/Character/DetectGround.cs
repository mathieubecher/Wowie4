using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectGround : MonoBehaviour
{
    private int m_isOnGround = 0;
    [SerializeField] private Animator m_fsm;


    private void Start()
    {
    }

    private void Update()
    {
        m_fsm.SetBool("isOnGround", m_isOnGround > 0);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.isTrigger || other.gameObject.layer == LayerMask.NameToLayer("Character")) return;
        ++m_isOnGround;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.isTrigger || other.gameObject.layer == LayerMask.NameToLayer("Character")) return;
        --m_isOnGround;
    }
}
