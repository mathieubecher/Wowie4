using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitBox : MonoBehaviour
{
    [SerializeField] private float m_damage = 1.0f;
    public float damage => m_damage;

    public delegate void OnHurtDelegate(HurtBox _other);

    public event OnHurtDelegate OnHurt;
    public void Hurt(HurtBox _other)
    {
        OnHurt?.Invoke(_other);
    }
}
