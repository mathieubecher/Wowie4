using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HurtBox : MonoBehaviour
{
    [SerializeField] private LayerMask m_hitLayers;
    [SerializeField] private LifeManager m_lifeManager;
    private void Awake()
    {
        if (!m_lifeManager) m_lifeManager = GetComponentInParent<LifeManager>();
    }

    private void Update()
    {
    }
    
    private void OnTriggerEnter2D(Collider2D _other)
    {
        if (_other.isTrigger) return;
        if ((m_hitLayers.value & (1 << _other.gameObject.layer)) > 0)
        {
            HitBox hitbox;
            if (_other.TryGetComponent(out hitbox))
            {
                if (hitbox.damageTick)
                {
                    hitbox.OnTick += Tick;
                }
                else
                {
                    if (m_lifeManager.Hit(this, hitbox))
                    {
                        hitbox.Hurt(this);
                    }
                }
            }
        }
        
    }
    private void OnTriggerExit2D(Collider2D _other)
    {
        if (_other.isTrigger) return;
        
        if ((m_hitLayers.value & (1 << _other.gameObject.layer)) > 0)
        {
            HitBox hitbox;
            if (_other.TryGetComponent(out hitbox))
            {
                if (hitbox.damageTick)
                {
                    hitbox.OnTick -= Tick;
                }
            }
        }

    }

    private void Tick(HitBox _hit)
    {
        m_lifeManager.Hit(this, _hit, false);
    }
}
