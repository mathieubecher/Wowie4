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
    
    private float m_timeIdle = 0.0f;
    private float m_elapsedTimeLastJump = 0.0f;
    private float m_elapsedTimeFirstJump = 0.0f;
    
    private State m_state = State.Idle;
    private Character m_myHuman; // for human front dir ?
    private Rigidbody2D m_rigidbody;
    
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

    public bool IsGrabbed()
    {
        return m_state == State.Grabbed;
    }

    public void Grab()
    {
        Debug.Log("Sac à dos, sac à dos!");
        m_state = State.Grabbed;
    }

    public void Drop()
    {
        Debug.Log("Pourquoi m'abandonner ?");
        m_state = State.Idle;
        m_timeIdle = 0.0f;
    }
    
    // --- Actions ---

    private void Shoot()
    {
        m_equippedGunBehavior.Shoot(transform.position, transform.eulerAngles.z);
    }

    private void Speak()
    {
        Debug.Log("Eve dis : Je suis là");
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

    public void setGunBehavior(GunBehavior _newGunBehavior)
    {
        m_equippedGunBehavior = _newGunBehavior;
    }
}
