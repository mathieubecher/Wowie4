using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class Enemy : MonoBehaviour
{
    private Animator m_fsm;
    private Rigidbody2D m_rigidbody;
    private LifeManager m_lifeManager;
    private AudioSource m_audio;

    private EnemyFSM.VirtualState m_currentState;
    
    public LifeManager lifeManager => m_lifeManager;
    public AudioSource audio => m_audio;
    
    [Header("Sound")]
    [SerializeField] private List<AudioClip> m_hitSoundsAtStart;
    
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
    
    private void Hit(Vector2 _origin, float _damage, bool _dead)
    {
        m_fsm.SetTrigger("Hit");
    }
}
