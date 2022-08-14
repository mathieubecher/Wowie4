using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "GunBehavior/SprayGunBehavior", order = 3)]
public class SprayGunBehavior : GunBehavior
{
    [SerializeField] private float m_angleSpeed = 30f;
    [SerializeField] private float m_startAngle = 180f;
    [SerializeField] private float m_endAngle = 0f;
    [SerializeField] private bool m_clockwise = true;

    private float m_currentAngle = 0f;

    public override void Reset()
    {
        m_currentAngle = m_startAngle;
    }
    
    public override bool Shoot(Vector3 _startPos, bool _goRight , string _bulletLayer)
    {
        if( m_currentAngle > 360f 
            || m_currentAngle < 0f 
            || m_clockwise && m_currentAngle <= m_endAngle 
            || !m_clockwise && m_currentAngle >= m_endAngle)
        {
            Reset();
        }

        bool hasShoot = m_gunType.Shoot(_startPos, m_currentAngle, _goRight, _bulletLayer);
        m_currentAngle += (m_clockwise ? -1f : 1f) * m_angleSpeed * Time.deltaTime;

        return hasShoot;
    }
}
