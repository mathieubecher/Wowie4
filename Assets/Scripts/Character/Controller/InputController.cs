using System;
using System.Collections.Generic;
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
    public void ReadDownAction(InputAction.CallbackContext _context)
    {
        if (_context.performed || _context.canceled)
        {
            foreach (var gameObject in FindGameObjectsWithLayer(LayerMask.NameToLayer("Platform")))
            {
                gameObject.GetComponent<Collider2D>().enabled = !_context.performed;
            }
        }
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
    
    private static List<GameObject> FindGameObjectsWithLayer (int _layer) {
        var goArray = FindObjectsOfType<GameObject>();
        List<GameObject> goList = new List<GameObject>();
        for (var i = 0; i < goArray.Length; i++) {
            if (goArray[i].layer == _layer) {
                goList.Add(goArray[i]);
            }
        }
        if (goList.Count == 0) {
            return null;
        }
        return goList;
    }
    
    
}
