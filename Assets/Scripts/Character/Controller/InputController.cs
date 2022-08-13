using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputController : MonoBehaviour
{
    private static InputController instance;

    public static InputController GetInstance()
    {
        if (!instance) instance = FindObjectOfType<InputController>();
        return instance;
    }
    
    private Inputs m_playerInput;
    
    public delegate void Interact();
    public static event Interact OnInteract;
    
    public delegate void Jump();
    public static event Jump OnJump;
    public static event Jump OnReleaseJump;
    
    public delegate void Dash();
    public static event Dash OnDash;
    
#if UNITY_EDITOR
    public delegate void DrawDebug();
    public static event DrawDebug OnDrawDebug;
#endif

    private void Awake()
    {
        instance = this;
    }

    public float moveDirection { get; set; }

    public void ReadMoveDirection(InputAction.CallbackContext _context)
    {
        moveDirection = _context.ReadValue<float>();
    }
    
    public void ReadInteractAction(InputAction.CallbackContext _context)
    {
        if (_context.performed)
            OnInteract?.Invoke();
    }
    
    public void ReadDashAction(InputAction.CallbackContext _context)
    {
        if (_context.performed)
            OnDash?.Invoke();
    }
    
    public void ReadJumpAction(InputAction.CallbackContext _context)
    {
        if (_context.performed)
            OnJump?.Invoke();
        if (_context.canceled)
            OnReleaseJump?.Invoke();
    }
    public void ReadDrawDebugAction(InputAction.CallbackContext _context)
    {
#if UNITY_EDITOR
        if (_context.performed)
            OnDrawDebug?.Invoke();
#endif
    }
    
}
