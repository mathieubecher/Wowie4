using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "GunType/GunType", order = 1)]
public class GunType : ScriptableObject
{
    [SerializeField] protected GameObject m_bullet;

    public virtual void Shoot(Vector3 _startPos, float _angle)
    {
        Quaternion rotation = Quaternion.Euler(0, 0, _angle);
        Instantiate(m_bullet, _startPos, rotation);
    }
}
