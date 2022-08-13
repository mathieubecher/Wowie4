using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "GunBehavior/GunBehavior", order = 1)]
public class GunBehavior : ScriptableObject
{
    [SerializeField] protected float m_angleDegrees = 0.0f;
    [SerializeField] protected GunType m_gunType;

    public virtual bool CanShoot()
    {
        return true;
    }

    public virtual void Shoot(Vector3 _startPos, bool _goRight)
    {
        if(_goRight)
        {
            m_gunType.Shoot(_startPos, m_angleDegrees);
        }
        else
        {
            m_gunType.Shoot(_startPos, m_angleDegrees + 180.0f);
        }
    }

    public void SetGunType(GunType _gunType)
    {
        m_gunType = _gunType;
    }
}
