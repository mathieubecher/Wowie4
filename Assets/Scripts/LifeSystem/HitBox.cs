using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitBox : MonoBehaviour
{
    [SerializeField] private float m_damage = 1.0f;
    [SerializeField] private float m_tickRate = 0.0f;
    [SerializeField] private bool m_damageTick = false;
    private float m_tickTimer = 0.0f;
    public float damage => m_damage;
    public bool damageTick => m_damageTick;

    public delegate void OnHurtDelegate(HurtBox _other);

    public event OnHurtDelegate OnHurt;

    public delegate void OnTickDelegate(HitBox _hit);

    public event OnTickDelegate OnTick;
    
    private void Update()
    {
        if (m_damageTick && m_tickRate > 0.0f)
        {
            if (m_tickTimer <= 0.0f)
            {
                Debug.Log("Tick");
                m_tickTimer = m_tickRate;
                OnTick?.Invoke(this);
            }

            m_tickTimer -= Time.deltaTime;
        }
    }
    public void Hurt(HurtBox _other)
    {
        OnHurt?.Invoke(_other);
    }
}
