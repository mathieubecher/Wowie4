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
    public DetectTargets detectTargets => m_detectTargets;

    private void Start()
    {
        m_equippedGunBehavior.SetGunType(m_gunType);
        m_equippedGunBehavior.Reset();
    }

    void Update()
    {
        if(m_isActive)
        {
            m_equippedGunBehavior.UpdateTargets(m_detectTargets.targets);
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
        //m_equippedGunBehavior.Shoot(m_spawnBulletPos.position, transform.lossyScale.x > 0f, m_bulletLayer);
    }

}
