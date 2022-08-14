using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class Enemy : MonoBehaviour
{
    public static readonly float EPSILON = 1.401298E-45f;
    
    [SerializeField] private bool m_follower = true;
    [SerializeField] private bool m_onGround;
    [SerializeField] private Transform m_body;
    
    // Private
    private Animator m_fsm;
    private Rigidbody2D m_rigidbody;
    private LifeManager m_lifeManager;
    private AudioSource m_audio;
    private EnemyFSM.VirtualState m_currentState;

    private Character m_characterRef;
    
    // Getter
    public LifeManager lifeManager => m_lifeManager;
    public AudioSource audio => m_audio;
    
    public Transform target => m_characterRef.transform;
    public Transform body => m_body;

    // Event
    public delegate void DeadDelegate(Enemy _enemy);

    public static event DeadDelegate OnDead;
    
    
    public void SetState(EnemyFSM.VirtualState _state)
    {
        m_currentState = _state;
    }
    void Awake()
    {
        m_fsm = GetComponent<Animator>();
        m_rigidbody = GetComponent<Rigidbody2D>();
        m_lifeManager = GetComponent<LifeManager>();
        m_audio = GetComponent<AudioSource>();
        m_characterRef = FindObjectOfType<Character>();
        
        m_fsm.SetBool("follow", m_follower);
        m_fsm.SetBool("onGround", m_onGround);

    }
    private void OnEnable()
    {
        lifeManager.OnHit += Hit;
    }

    private void OnDisable()
    {
        lifeManager.OnHit -= Hit;
    }
    
    private void FixedUpdate()
    {
        if(m_currentState) m_currentState.OnFixedUpdate();
    }
    
    
#region Physics
    public Vector2 GetCurrentVelocity()
    {
        return new Vector2(m_rigidbody.velocity.x, m_rigidbody.velocity.y);
    }
    public void SetDesiredVelocity(Vector2 _velocity, bool _ignoreVertical = true)
    {
        m_rigidbody.velocity = Vector2.right * _velocity.x + Vector2.up * (_ignoreVertical ? m_rigidbody.velocity.y : _velocity.y);
    }
    public float GetGravityScaler(){ return m_rigidbody.gravityScale; }
    public void SetGravityScaler(float _gravityScaler){ m_rigidbody.gravityScale = _gravityScaler; }

#endregion
    
    private void Hit(Vector2 _origin, float _damage, bool _dead)
    {
        m_fsm.SetTrigger("Hit");
        
        m_fsm.SetBool("dead", _dead);
        if(_dead) Enemy.OnDead?.Invoke(this);
    }
}
