using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    private Rigidbody2D m_rigidbody;
    void Awake()
    {
        m_rigidbody = GetComponent<Rigidbody2D>();
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
        //Debug.Log(GetMoveInput());
    }


    private float GetMoveInput()
    {
        return InputController.GetInstance().moveDirection;
    }
    private void Jump()
    {
        Debug.Log("Jump");
    }
    private void Interact()
    {
        Debug.Log("Interact");
    }
    private void Dash()
    {
        Debug.Log("Dash");
    }
}
