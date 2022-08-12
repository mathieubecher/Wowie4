using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public const float EPSILON = 1.401298E-45f;
    
    // Serialize
    [SerializeField] private Transform m_body;
    
    // Private
    private Rigidbody2D m_rigidbody;
    private Animator m_fsm;
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

    public Vector2 GetCurrentVelocity()
    {
        return m_rigidbody.velocity;
    }
    public void SetDesiredVelocity(Vector2 _velocity, bool _ignoreVertical = true)
    {
        m_rigidbody.velocity = Vector2.right * _velocity.x + Vector2.up * (_ignoreVertical ? m_rigidbody.velocity.y : _velocity.y);
    }

    public float GetMoveInput() 
    {
        float moveInput = InputController.GetInstance().moveDirection;
        moveInput = (Mathf.Abs(moveInput) > EPSILON) ? Mathf.Sign(moveInput) : 0f;
        return moveInput;
    }
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
}
