using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "GunType/MultiShotsGunType", order = 2)]
public class MultiShotsGunType : GunType
{
    [SerializeField] private int m_numberOfShoot = 3;
    [SerializeField] private float m_angleBetweenShoot = 45.0f;

    public override bool Shoot(Vector3 _startPos, float _angle, bool _isRight)
    {
        m_timer += Time.deltaTime;
        if(m_timer >= m_firingRate)
        {
            for(int i = 0 ; i < m_numberOfShoot ; i++)
            {
                Quaternion rotation = Quaternion.Euler(0, 0, _angle + (m_angleBetweenShoot * i));
                Instantiate(m_bullet, _startPos, rotation);
            }  
            m_timer = 0.0f;
            return true;
        }
        return false;
    }
}
