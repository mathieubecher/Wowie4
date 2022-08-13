using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "GunType/MultiShotsGunType", order = 2)]
public class MultiShotsGunType : GunType
{
    [SerializeField] private int m_numberOfShoot = 3;
    [SerializeField] private float m_angleBetweenShoot = 45.0f;

    public override void Shoot(Vector3 _startPos, float _angle)
    {
        for(int i = 0 ; i < m_numberOfShoot ; i++)
        {
            Quaternion rotation = Quaternion.Euler(0, 0, _angle + (m_angleBetweenShoot * i));
            Instantiate(m_bullet, _startPos, rotation);
        }   
    }
}
