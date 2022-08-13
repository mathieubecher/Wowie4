using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectRobot : MonoBehaviour
{
    private bool m_detectRobot;
    public bool detectRobot => m_detectRobot;
    private Robot m_robotRef;
    public bool robotRef => m_robotRef;
    
    private void Start()
    {
        
    }

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
