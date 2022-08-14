using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    
    [SerializeField] private GunBehavior m_equippedGunBehavior;
    [SerializeField] private DetectTargets m_detectTargets;
    [SerializeField] private string m_bulletLayer = "Bullet";
    [SerializeField] protected GunType m_gunType;
    
    // Private
    private bool m_isActive = true;
    [SerializeField] private Transform m_spawnBulletPos;
    
    // Getter
    public GunBehavior gunBehavior => m_equippedGunBehavior;

    private void Awake()
    {
        m_equippedGunBehavior.SetGunType(m_gunType);
        m_equippedGunBehavior.Reset();
    }

    void Update()
    {
        if(m_isActive)
        {
            Shoot();
        }
    }

    public void Activate(bool _active)
    {
        m_isActive = _active;
    }
    
    public void SetEquippedGunBehavior(GunBehavior _newGunBehavior)
    {
        m_equippedGunBehavior = _newGunBehavior;
        m_equippedGunBehavior.SetGunType(m_gunType);
        m_equippedGunBehavior.Reset();
    }

    public void SetGunType(GunType _newGunType)
    {
        m_gunType = _newGunType;
        m_equippedGunBehavior.SetGunType(_newGunType);
        m_equippedGunBehavior.Reset();
    }
    public void Reset()
    {
        m_equippedGunBehavior.Reset();
    }
    
    public void Shoot()
    {
        if (m_equippedGunBehavior.FindTarget(m_detectTargets.targets))
        {
            bool goRight = transform.lossyScale.x > 0.0f;
            m_equippedGunBehavior.Shoot(m_spawnBulletPos.position, goRight, m_bulletLayer);
        }
    }

}