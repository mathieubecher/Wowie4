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
