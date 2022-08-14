using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "GunBehavior/GunBehavior", order = 1)]
public class GunBehavior : ScriptableObject
{
    protected GunType m_gunType;

    public virtual void Reset()
    {}

    public virtual bool FindTarget(List<Transform> _detectedTargets)
    {
        return true;
    }

    public virtual void Shoot(Vector3 _startPos, bool _goRight, string _bulletLayer)
    {
        m_gunType.Shoot(_startPos, 0f, _goRight, _bulletLayer);
    }

    public void SetGunType(GunType _gunType)
    {
        m_gunType = _gunType;
    }
}
