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

    [SerializeField] private float m_life;
    private State m_state = State.Idle;
    private GunBehavior m_equippedGunBehavior;
    private Character m_myHuman; // for human front dir ?
    private Rigidbody2D m_rigidbody;
    [SerializeField] Bullet m_bullet;

    void Awake()
    {
        m_rigidbody = GetComponent<Rigidbody2D>();
    }

    // Start is called before the first frame update
    void Start()
    {
        GunBehavior.Config gunConfig;
        gunConfig.rate = 0.5f;
        gunConfig.angleInDegree = 360;
        m_equippedGunBehavior = new GunBehavior(gunConfig);
    }

    // Update is called once per frame
    void Update()
    {
        Shoot();
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
        Debug.Log("Pourquoi m'abandonner");
        m_state = State.Idle;
    }
    
    // --- Actions ---

    private void Shoot()
    {
        m_equippedGunBehavior.Shoot(transform.position);
    }

    private void Speak()
    {
        Debug.Log("Eve dis : Je suis là");
    }

    private void JumpOnPlace()
    {
        Debug.Log("Hop .. Hop");
    }
}
