using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public const float EPSILON = 1.401298E-45f;
    
    // Serialize
    [SerializeField] private Transform m_body;
    [SerializeField] private float m_maxSpeed = 10.0f;
    public float maxSpeed{ get => m_maxSpeed; }
    
    // Private
    private Rigidbody2D m_rigidbody;
    private Animator m_fsm;
    private CharacterFSM.VirtualState m_currentState;

    public void SetState(CharacterFSM.VirtualState _state)
    {
        m_currentState = _state;
    }
    void Awake()
    {
        m_rigidbody = GetComponent<Rigidbody2D>();
        m_fsm = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        InputController.OnDash += Dash;
        InputController.OnJump += Jump;
        InputController.OnInteract += Interact;
    }

    private void OnDisable()
    {
        InputController.OnDash -= Dash;
        InputController.OnJump -= Jump;
        InputController.OnInteract -= Interact;
    }

    void Update()
    {
        float moveInput = GetMoveInput();
        m_body.localScale = new Vector3(Mathf.Abs(moveInput) > EPSILON? moveInput : m_body.localScale.x,1.0f,1.0f);

    }

    private void FixedUpdate()
    {
        if(m_currentState) m_currentState.OnFixedUpdate();
        Debug.DrawLine(transform.position, transform.position + (Vector3)m_rigidbody.velocity * 0.1f, Color.white, 10.0f);
    }

    #region Physics
    public Vector2 GetCurrentVelocity()
    {
        return m_rigidbody.velocity;
    }
    public void SetDesiredVelocity(Vector2 _velocity, bool _ignoreVertical = true)
    {
        m_rigidbody.velocity = Vector2.right * _velocity.x + Vector2.up * (_ignoreVertical ? m_rigidbody.velocity.y : _velocity.y);
    }
    public float GetGravityScaler(){ return m_rigidbody.gravityScale; }
    public void SetGravityScaler(float _gravityScaler){ m_rigidbody.gravityScale = _gravityScaler; }

    public float GetMoveInput() 
    {
        float moveInput = InputController.GetInstance().moveDirection;
        moveInput = (Mathf.Abs(moveInput) > EPSILON) ? Mathf.Sign(moveInput) : 0f;
        return moveInput;
    }
    #endregion
    
    #region Inputs
    private void Jump()
    {
        m_fsm.SetTrigger("Jump");
    }
    private void Interact()
    {
        Debug.Log("Interact");
    }
    private void Dash()
    {
        m_fsm.SetTrigger("Dash");
    }
    #endregion
}
