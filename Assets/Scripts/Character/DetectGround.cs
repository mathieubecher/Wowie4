using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectGround : MonoBehaviour
{
    private int m_isOnGround = 0;
    private Animator m_fsm;


    private void Start()
    {
        m_fsm = GetComponent<Animator>();
    }

    private void Update()
    {
        m_fsm.SetBool("isOnGround", m_isOnGround > 0);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        ++m_isOnGround;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        --m_isOnGround;
    }
}
