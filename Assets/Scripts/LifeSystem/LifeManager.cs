using UnityEngine;

public class LifeManager : MonoBehaviour
{
    public delegate void OnHitDelegate(Vector2 _origin, float _damage, bool _dead);

    public OnHitDelegate OnHit;
    
    
    [SerializeField] private float m_life = 5.0f;
    [SerializeField] private float m_invulnerableAfterHitTime = 1.0f;
    public bool dead => m_life <= Character.EPSILON;

    private float m_lastHit;
    private bool m_invunlerability = false;

    private void Update()
    {
        m_lastHit -= Time.deltaTime;
    }
    public void SetInvulnerability(bool _enable)
    {
        m_invunlerability = _enable;
    }

    public bool Hit(HurtBox _hurtBox, HitBox _other, bool _hit = true)
    {
        if (m_invunlerability || dead || m_lastHit > 0f) return false;

        m_life -= _other.damage;
        if (_hit || dead)
        {
            m_lastHit = m_invulnerableAfterHitTime;
            OnHit?.Invoke(_other.transform.position, _other.damage, dead);
            
        }
        return true;
    }
}
