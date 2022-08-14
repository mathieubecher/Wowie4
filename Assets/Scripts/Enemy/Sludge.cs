using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sludge : MonoBehaviour
{
    [SerializeField] private Transform m_body;
    protected AudioSource m_audio;
    protected LifeManager m_lifeManager;

    public delegate void DeadDelegate(Sludge _sludge);

    public static event DeadDelegate OnDead;

    private void Awake()
    {
        m_lifeManager = GetComponent<LifeManager>();
        m_audio = GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        m_lifeManager.OnHit += Hit;
    }

    private void OnDisable()
    {
        
        m_lifeManager.OnHit -= Hit;
    }

    private void Hit(Vector2 _origin, float _damage, bool _dead)
    {
        if (_dead)
        {
            OnDead?.Invoke(this);
            Destroy(gameObject);
        }
    }
}
