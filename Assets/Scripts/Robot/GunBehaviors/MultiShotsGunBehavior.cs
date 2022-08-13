using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "GunBehavior/MultiShotsGunBehavior", order = 2)]
public class MultiShotsGunBehavior : GunBehavior
{
    [SerializeField] private int m_numberOfShoot = 3;
    [SerializeField] private float m_angleBetweenShoot = 45.0f;

    public override void Shoot(Vector3 startPos)
    {
        m_timer += Time.deltaTime;
        if(m_timer >= m_firingRate)
        {
            for(int i = 0 ; i < m_numberOfShoot ; i++)
            {
                Quaternion rotation = Quaternion.Euler(0, 0, m_angleDegrees + (m_angleBetweenShoot * i));
                Instantiate(m_bullet, startPos, rotation);
            }
            
            m_timer = 0.0f;
        }
    }
}
