using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectRobot : MonoBehaviour
{
    [SerializeField] private float m_maxDist = 5.0f;
    
    private bool m_detectRobot;
    private Robot m_robotRef;
    public bool detectRobot => m_detectRobot && CanReachRobot();
    public Robot robotRef => m_robotRef;
    public float maxDist => m_maxDist;

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

    private bool CanReachRobot()
    {
        if (!m_detectRobot) return false;
        
        Vector2 direction = robotRef.transform.position - transform.position;
        Vector2 size = new Vector2(1.0f, 2.0f);
        Vector2 origin = transform.position + Vector3.up * size.y / 2f;
        float distance = direction.magnitude;
        direction.Normalize();
        LayerMask mask = LayerMask.GetMask("Default");
        
        RaycastHit2D hitInfo = Physics2D.BoxCast(origin, size * 0.95f, 0.0f, direction: direction, distance: distance, mask);
        if (hitInfo)
        {
            Debug.DrawLine(transform.position, hitInfo.point, Color.red);
            return false;
        }
        else
        {
            Debug.DrawLine(transform.position, transform.position + (Vector3)direction * distance, Color.cyan);
            return true;
        }
    }
}
