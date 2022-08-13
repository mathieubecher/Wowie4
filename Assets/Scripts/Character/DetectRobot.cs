using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectRobot : MonoBehaviour
{
    [SerializeField] private float m_maxDist = 5.0f;
    
    private bool m_detectRobot;
    private Robot m_robotRef;
    public bool detectRobot => m_detectRobot;
    public Robot robotRef => m_robotRef;
    public float maxDist => m_maxDist;

    private void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D _other)
    {
        if (_other.isTrigger || _other.gameObject.layer == LayerMask.NameToLayer("Character")) return;
        m_robotRef = _other.transform.GetComponentInParent<Robot>();
        m_detectRobot = true;
    }

    private void OnTriggerExit2D(Collider2D _other)
    {
        if (_other.isTrigger || _other.gameObject.layer != LayerMask.NameToLayer("Robot")) return;
        m_robotRef = null;
        m_detectRobot = false;
    }
}
