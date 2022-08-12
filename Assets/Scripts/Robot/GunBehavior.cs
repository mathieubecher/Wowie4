using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "GunBehavior/GunBehavior", order = 1)]
public class GunBehavior : ScriptableObject
{
    [Serializable]
    public struct Config
    {
        public float rate;
        public float angleDegrees;
    }

    [SerializeField] private Config m_config;
    [SerializeField] private GameObject m_bullet;
    
    private float m_timer = 0.0f;

    public void Shoot(Vector3 startPos)
    {
        m_timer += Time.deltaTime;
        if(m_timer >= m_config.rate)
        {
            Quaternion rotation = Quaternion.Euler(0, 0, m_config.angleDegrees);
            Instantiate(m_bullet, startPos, rotation);
            m_timer = 0.0f;
        }
    }
}
