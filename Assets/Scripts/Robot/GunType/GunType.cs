using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "GunType/GunType", order = 1)]
public class GunType : ScriptableObject
{
    [SerializeField] protected float m_firingRate = 1.0f;
    [SerializeField] protected GameObject m_bullet;
  
    protected float m_timer = 0.0f;

    public virtual void Shoot(Vector3 _startPos, float _angle)
    {
        m_timer += Time.deltaTime;
        if(m_timer >= m_firingRate)
        {
            Quaternion rotation = Quaternion.Euler(0, 0, _angle);
            Instantiate(m_bullet, _startPos, rotation);
            m_timer = 0.0f;
        }
    }
}
