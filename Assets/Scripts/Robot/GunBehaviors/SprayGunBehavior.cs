using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "GunBehavior/SprayGunBehavior", order = 3)]
public class SprayGunBehavior : GunBehavior
{
    [SerializeField] private float m_angleBetweenShoot = 10.0f;

    private float m_currentAngle = 0.0f;

    public override void Shoot(Vector3 startPos)
    {
        m_timer += Time.deltaTime;
        if(m_timer >= m_firingRate)
        {
            if(m_currentAngle <= m_angleDegrees)
            {
                Quaternion rotation = Quaternion.Euler(0, 0, m_currentAngle);
                Instantiate(m_bullet, startPos, rotation);
                m_currentAngle += m_angleBetweenShoot;
            }
            else
            {
                m_currentAngle = 0.0f;
            }
            
            m_timer = 0.0f;
        }
    }
}
