using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Robot : MonoBehaviour
{   
    private enum State
    {
        Idle,
        Grabbed
    }
    [SerializeField] private float m_upVelocityOnDrop = 20.0f;

    //[SerializeField] private float m_timeWaitingBeforeJumpOnPlace = 0.0f;

    [SerializeField] private Animator m_bodyAnimator;
    [SerializeField] private List<GunBehavior> m_gunBehaviors;
    
    private Shooter m_shooter;
    
    //private float m_timeIdle = 0.0f;
    //private bool m_jumpOnPlace = false;
    
    private State m_state = State.Idle;
    private Rigidbody2D m_rigidbody;

    private bool m_isActive = true;
    
    void Awake()
    {
        m_rigidbody = GetComponent<Rigidbody2D>();
        m_shooter = GetComponent<Shooter>();
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
            bool isRight = transform.lossyScale.x > 0.0f;
            m_bodyAnimator.SetBool("Right", isRight);

            bool findBehavior = false;
            foreach(GunBehavior gunBehavior in m_gunBehaviors)
            {
                gunBehavior.UpdateTargets(m_shooter.detectTargets.targets);
                if(gunBehavior.CanShoot())
                {
                    if(gunBehavior != m_shooter.gunBehavior)
                    {
                        EquippedGunBehavior(gunBehavior);
                    }
                    findBehavior = true;
                    break;
                }
            }
            ActivateShooter(findBehavior);

            /*if(!IsGrabbed() && isRight)
            {
                m_timeIdle += Time.deltaTime;
                if(m_timeIdle >= m_timeWaitingBeforeJumpOnPlace) // want attention
                {
                    if(!m_jumpOnPlace)
                    {
                        m_jumpOnPlace = true;
                        m_bodyAnimator.SetBool("JumpOnPlace", true);
                        StartCoroutine(JumpOnPlace());
                    }
                }
            }*/
        }
    }

    private bool IsGrabbed()
    {
        return m_state == State.Grabbed;
    }

    public void StartGrab()
    {
        
        m_rigidbody.isKinematic = true;
        m_rigidbody.velocity = Vector2.zero;
    }
    public void Grab()
    {
        SetState(State.Grabbed);
        m_rigidbody.isKinematic = true;
        m_rigidbody.velocity = Vector2.zero;
        m_bodyAnimator.SetBool("isGrab", true);
    }

    public void Drop(float _velocity, bool _dropOnPlace)
    {
        SetState(State.Idle);
        //m_timeIdle = 0.0f;
        m_rigidbody.isKinematic = false;
        m_rigidbody.velocity = _dropOnPlace? Vector2.zero : new Vector2(_velocity, m_upVelocityOnDrop);
        m_bodyAnimator.SetBool("isGrab", false);
    }

    private void SetState(State _newState)
    {
        m_state = _newState;
        m_shooter.Reset();
    }

    public void Speak()
    {
        Debug.Log("Eve dis : Je suis là");
    }

    public void setEmote()
    {
        Debug.Log("Eve sourie");
    }

    /*private IEnumerator JumpOnPlace()
    {
        yield return new WaitForSeconds(1f);
        m_bodyAnimator.SetBool("JumpOnPlace", false);
        m_jumpOnPlace = false;
        m_timeIdle = 0f;
    }*/
    
    public void SetGunBehaviors(List<GunBehavior> _newGunBehaviors)
    {
        m_gunBehaviors = _newGunBehaviors;
    }

    public void EquippedGunBehavior(GunBehavior _newGunBehavior)
    {
        m_shooter.SetEquippedGunBehavior(_newGunBehavior);
    }

    public void SetEquippedGunBehavior(GunBehavior _newGunBehavior)
    {
        m_gunBehaviors.Clear();
        m_gunBehaviors.Add(_newGunBehavior);
        m_shooter.SetEquippedGunBehavior(_newGunBehavior);
    }

    public void SetGunType(GunType _newGunType)
    {
        m_shooter.SetGunType(_newGunType);
    }

    public void Activated(bool _activated)
    {
        m_isActive = _activated;
        ActivateShooter(_activated);
    }

    private void ActivateShooter(bool _activated)
    {
        m_shooter.Activate(_activated);
    }

    public void SetDistanceDetect(float _distance)
    {
        m_shooter.SetDistanceDetect(_distance);
    }
}
