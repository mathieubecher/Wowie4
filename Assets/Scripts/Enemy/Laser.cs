using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    [SerializeField] private float m_dist = 10;
    [SerializeField] private float m_damagePerSeconds = 0.1f;

    private bool m_hasTarget = false;
    private LifeManager m_target;
    private LineRenderer m_laser;

    private void Awake()
    {
        m_target = FindObjectOfType<Character>().GetComponent<LifeManager>();
    }
    private void Update()
    {
        if (m_hasTarget && (m_target == null || m_target.dead)) m_hasTarget = false;
        if (m_hasTarget)
        {
            
        }
    }

    void FixedUpdate()
    {
        if (m_hasTarget)
        {
            Vector2 direction = m_target.transform.position - transform.position;
            float distance = direction.magnitude;
            
            
        }

    }

    public void SetTarget(LifeManager _target)
    {
        m_target = _target;
    }
}
