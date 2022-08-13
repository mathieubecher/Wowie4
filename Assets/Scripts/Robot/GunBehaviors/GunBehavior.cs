using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "GunBehavior/GunBehavior", order = 1)]
public class GunBehavior : ScriptableObject
{
    [SerializeField] protected float m_firingRate = 1.0f;
    [SerializeField] protected float m_angleDegrees = 0.0f;
    [SerializeField] protected GunType m_gunType;
    
    protected float m_timer = 0.0f;

    public virtual void Shoot(Vector3 _startPos, float _currentAngle)
    {
        m_timer += Time.deltaTime;
        if(m_timer >= m_firingRate)
        {
            m_gunType.Shoot(_startPos, _currentAngle + m_angleDegrees);
            m_timer = 0.0f;
        }
    }
}
