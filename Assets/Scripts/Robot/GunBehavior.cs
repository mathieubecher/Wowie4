using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunBehavior : Object
{
    public struct Config
    {
        public float rate;
        public float angleInDegree;
    }

    private GunBehavior.Config m_config;
    private float m_timer = 0.0f;
    private GameObject m_bullet;

    public GunBehavior(GunBehavior.Config _config, GameObject _bullet)
    {
        m_config = _config;
        m_bullet = _bullet
    }

    public void Shoot(Vector3 startPos)
    {
        m_timer += Time.deltaTime;
        if(m_timer >= m_config.rate)
        {
            Debug.Log("Piou Piou");
            
            Instantiate(_bullet, startPos, Quaternion.identity);
            m_timer = 0.0f;
        }
    }
}
