using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "GunBehavior/GunBehavior", order = 1)]
public class GunBehavior : ScriptableObject
{
    [SerializeField] protected float m_firingRate = 1.0f;
    [SerializeField] protected float m_angleDegrees = 0.0f;
    [SerializeField] protected GameObject m_bullet;
    
    protected float m_timer = 0.0f;

    public virtual void Shoot(Vector3 startPos)
    {
        m_timer += Time.deltaTime;
        if(m_timer >= m_firingRate)
        {
            Quaternion rotation = Quaternion.Euler(0, 0, m_angleDegrees);
            Instantiate(m_bullet, startPos, rotation);
            m_timer = 0.0f;
        }
    }
}
