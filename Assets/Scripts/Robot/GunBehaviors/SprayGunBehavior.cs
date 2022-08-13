using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "GunBehavior/SprayGunBehavior", order = 3)]
public class SprayGunBehavior : GunBehavior
{
    [SerializeField] private float m_angleBetweenShoot = 10.0f;

    private float m_currentAngle = 0.0f;
    
    public override void Shoot(Vector3 _startPos, bool _goRight)
    {
        if(m_currentAngle <= m_angleDegrees)
        {
            m_gunType.Shoot(_startPos, m_currentAngle);
            m_currentAngle += m_angleBetweenShoot;
        }
        else
        {
            m_currentAngle = 0.0f;
        }
    }
}
