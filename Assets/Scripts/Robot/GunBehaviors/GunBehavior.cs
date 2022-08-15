using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "GunBehavior/GunBehavior", order = 1)]
public class GunBehavior : ScriptableObject
{
    [SerializeField] protected Condition m_condition;
    protected GunType m_gunType;

    public virtual void Reset()
    {}

    public virtual void UpdateTargets(List<Transform> _targets)
    {}

    public virtual bool CanShoot()
    {
        return m_condition && m_condition.Poll();
    }

    public virtual bool Shoot(Vector3 _startPos, bool _goRight, string _bulletLayer)
    {
        return m_gunType.Shoot(_startPos, 0f, _goRight, _bulletLayer);
    }

    public void SetGunType(GunType _gunType)
    {
        m_gunType = _gunType;
    }
}
