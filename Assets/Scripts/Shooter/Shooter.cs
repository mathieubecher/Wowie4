using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class Shooter : MonoBehaviour
{
    
    [SerializeField] private GunBehavior m_equippedGunBehavior;
    [SerializeField] private DetectTargets m_detectTargets;
    [SerializeField] private string m_bulletLayer = "Bullet";
    [SerializeField] protected GunType m_gunType;
    [SerializeField] private Transform m_spawnBulletPos;
    
    protected AudioSource m_audio;
    [Header("Sound")]
    [SerializeField] protected List<AudioClip> m_shootSounds;
    
    // Private
    private bool m_isActive = true;
    
    // Getter
    public GunBehavior gunBehavior => m_equippedGunBehavior;
    public DetectTargets detectTargets => m_detectTargets;

    private void Start()
    {
        m_equippedGunBehavior.SetGunType(m_gunType);
        m_equippedGunBehavior.Reset();
        m_audio = GetComponent<AudioSource>();
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
        if (m_equippedGunBehavior.Shoot(m_spawnBulletPos.position, transform.lossyScale.x > 0f, m_bulletLayer))
        {
            if (m_audio && m_shootSounds.Count > 0)
            {
                int randomSoundId = (int)math.floor(Random.Range(0, m_shootSounds.Count));
                m_audio.PlayOneShot(m_shootSounds[randomSoundId]);
            }
        }
    }

}
