using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    [SerializeField] private float m_dist = 10;
    [SerializeField] private float m_damagePerSeconds = 0.1f;
    [SerializeField] private float m_angularSpeed = 30f;
    [SerializeField] private Transform m_shootSpawner;

    [SerializeField] private Transform m_laserDot;

    [SerializeField] private LifeManager m_target;

    private LineRenderer m_laser;
    private bool m_hasTarget = true;
    private Vector2 m_currentDir;
    
    private void Awake()
    {
        m_target = FindObjectOfType<Character>().GetComponent<LifeManager>();
        m_laser = GetComponent<LineRenderer>();
        m_currentDir = (m_target.transform.position - transform.position).normalized;
    }

    private void FixedUpdate()
    {
        if (m_hasTarget && (m_target == null || m_target.dead)) m_hasTarget = false;
        
        Vector2 desiredDirection =m_target.transform.position - transform.position;

        float angle = Vector2.Angle(m_currentDir, desiredDirection);
        m_currentDir = Vector3.Slerp(m_currentDir, desiredDirection, m_angularSpeed * Time.deltaTime / angle);
        Vector2 direction = m_currentDir;
        
        float distance = desiredDirection.magnitude;
        
        if (m_hasTarget && distance < m_dist)
        {
            
            int mask = 1 << LayerMask.NameToLayer("Default") | 1 << m_target.gameObject.layer;
            RaycastHit2D hit = Physics2D.Raycast(m_shootSpawner.transform.position, direction, m_dist, mask);
            if (hit)
            {
                DrawLineRenderer(hit.point);
                if (hit.collider.gameObject.layer == m_target.gameObject.layer)
                {
                    m_target.Damaged(m_damagePerSeconds * Time.deltaTime);
                }
            }
            else
            {
                DrawLineRenderer((Vector2)transform.position + direction * m_dist);
            }
            
        }

    }

    private void DrawLineRenderer(Vector2 hitPoint)
    {
        Vector3[] positions = new Vector3[2];
        positions[0] = m_shootSpawner.transform.position;
        positions[1] = hitPoint;

        m_laserDot.position = hitPoint;
        m_laser.SetPositions(positions);
    }

    public void SetTarget(LifeManager _target)
    {
        m_target = _target;
    }
}
