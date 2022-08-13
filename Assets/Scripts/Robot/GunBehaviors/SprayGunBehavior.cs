using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "GunBehavior/SprayGunBehavior", order = 3)]
public class SprayGunBehavior : GunBehavior
{
    [SerializeField] private float m_angleBetweenShoot = 10.0f;
    [SerializeField] private float m_startAngle = 180.0f;
    [SerializeField] private float m_endAngle = 0.0f;
    [SerializeField] private bool m_add = false;

    private float m_currentAngle = 0.0f;

    public override void Reset()
    {
        m_currentAngle = m_startAngle;
    }
    
    public override void Shoot(Vector3 _startPos, bool _goRight)
    {
        if(m_currentAngle > 360 || m_currentAngle < 0.0f)
        {
            Reset();
        }

        if(m_add && m_currentAngle <= m_endAngle)
        {
            if(m_gunType.Shoot(_startPos, m_currentAngle, _goRight))
            {
                m_currentAngle += m_angleBetweenShoot;
            }
        }
        else if(!m_add && m_currentAngle >= m_endAngle)
        {
            if(m_gunType.Shoot(_startPos, m_currentAngle, _goRight))
            {
                m_currentAngle -= m_angleBetweenShoot;
            }
        }
        else
        {
            Reset();
        }
        
    }
}
