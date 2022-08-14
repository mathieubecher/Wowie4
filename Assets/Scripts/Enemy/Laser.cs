using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    [SerializeField] private float m_dist = 10;
    [SerializeField] private float m_damagePerSeconds = 0.1f;
    [SerializeField] private Transform m_shootSpawner;

    private bool m_hasTarget = true;
    [SerializeField] private LifeManager m_target;
    private LineRenderer m_laser;

    private void Awake()
    {
        m_target = FindObjectOfType<Character>().GetComponent<LifeManager>();
        m_laser = GetComponent<LineRenderer>();
    }

    private void FixedUpdate()
    {
        if (m_hasTarget && (m_target == null || m_target.dead)) m_hasTarget = false;
        Vector2 direction = m_target.transform.position - transform.position;
        float distance = direction.magnitude;
        
        if (m_hasTarget && distance < m_dist)
        {
            
            LayerMask mask = LayerMask.GetMask("Default");
            RaycastHit2D hit = Physics2D.Raycast(m_shootSpawner.transform.position, direction, distance, mask);
            if (hit)
            {
                DrawLineRenderer(hit.point);
            }
            else
            {
                m_target.Damaged(m_damagePerSeconds * Time.deltaTime);
                DrawLineRenderer(m_target.transform.position + Vector3.up);
            }
            
        }

    }

    private void DrawLineRenderer(Vector2 hitPoint)
    {
        Vector3[] positions = new Vector3[2];
        positions[0] = m_shootSpawner.transform.position;
        positions[1] = hitPoint;
        
        m_laser.SetPositions(positions);
    }

    public void SetTarget(LifeManager _target)
    {
        m_target = _target;
    }
}
