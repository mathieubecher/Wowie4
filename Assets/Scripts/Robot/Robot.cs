using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Robot : MonoBehaviour
{   
    private enum State
    {
        Idle,
        Grabbed
    }

    [SerializeField] private GunBehavior m_equippedGunBehavior;

    [SerializeField] private float m_jumpAmount = 2.0f;
    [SerializeField] private float m_timeWaitingBeforeJumpOnPlace = 0.0f;
    [SerializeField] private float m_timeBetweenJump = 0.5f;
    [SerializeField] private float m_jumpOnPlaceDuration = 2.0f;

    [SerializeField] private DetectEnemy m_detectEnemy;
    
    private float m_timeIdle = 0.0f;
    private float m_elapsedTimeLastJump = 0.0f;
    private float m_elapsedTimeFirstJump = 0.0f;
    
    private State m_state = State.Idle;
    private Character m_myHuman; // for human front dir ?
    private Rigidbody2D m_rigidbody;

    private bool m_isActive = true;
    
    void Awake()
    {
        m_rigidbody = GetComponent<Rigidbody2D>();
        m_myHuman = FindObjectOfType<Character>();
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if(m_isActive)
        {
            Shoot();

            if(!IsGrabbed())
            {
                m_timeIdle += Time.deltaTime;
                if(m_timeIdle >= m_timeWaitingBeforeJumpOnPlace) // want attention
                {
                    if(m_elapsedTimeFirstJump <= m_jumpOnPlaceDuration) 
                    {
                        JumpOnPlace(); // need a jump anim ;)
                    }
                    else // enought jumpOnPlace for now
                    {
                        m_elapsedTimeFirstJump = 0.0f;
                        m_elapsedTimeLastJump = 0.0f;
                        m_timeIdle = 0.0f;
                    }
                    m_elapsedTimeFirstJump += Time.deltaTime;
                }
            }
        }
    }

    private bool IsGrabbed()
    {
        return m_state == State.Grabbed;
    }

    public void Grab()
    {
        Debug.Log("Sac à dos, sac à dos!");
        SetState(State.Grabbed);
        m_rigidbody.isKinematic = true;
        m_rigidbody.velocity = Vector2.zero;
    }

    public void Drop(Vector2 _velocity)
    {
        Debug.Log("Pourquoi m'abandonner ?");
        SetState(State.Idle);
        m_timeIdle = 0.0f;
        m_rigidbody.isKinematic = false;
        m_rigidbody.velocity = _velocity;
    }

    private void SetState(State _newState)
    {
        m_state = _newState;
        m_equippedGunBehavior.Reset();
    }

    private void Shoot()
    {
        if(m_equippedGunBehavior.CanShoot())
        {
            bool goRight = transform.lossyScale.x > 0.0f;
            m_equippedGunBehavior.Shoot(transform.Find("Body/spawnBullet").position, goRight, m_detectEnemy.enemies);
        }
    }

    public void Speak()
    {
        Debug.Log("Eve dis : Je suis là");
    }

    public void setEmote()
    {
        Debug.Log("Eve sourie");
    }

    private void JumpOnPlace()
    {
        m_elapsedTimeLastJump += Time.deltaTime;
        if(m_elapsedTimeLastJump >= m_timeBetweenJump)
        {
            m_rigidbody.AddForce(Vector2.up * m_jumpAmount, ForceMode2D.Impulse);
            m_elapsedTimeLastJump = 0.0f;
        }
    }

    public void SetGunBehavior(GunBehavior _newGunBehavior)
    {
        m_equippedGunBehavior = _newGunBehavior;
        m_equippedGunBehavior.Reset();
    }

    public void SetGunType(GunType _newGunType)
    {
        m_equippedGunBehavior.SetGunType(_newGunType);
        m_equippedGunBehavior.Reset();
    }

    public void Activated(bool _activated)
    {
        m_isActive = _activated;
    }
}
