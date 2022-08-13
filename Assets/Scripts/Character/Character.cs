using Unity.Mathematics;
using UnityEngine;

public class Character : MonoBehaviour
{
    public static readonly float EPSILON = 1.401298E-45f;
    
    // Serialize
    [SerializeField] private Transform m_body;
    [SerializeField] private float m_maxSpeed = 10.0f;
    
    [SerializeField] private DetectRobot m_detectRobotRef;
    public float maxSpeed => m_maxSpeed;
    public Transform body => m_body;

    // Private
    private Rigidbody2D m_rigidbody;
    private DetectPhysics m_detectPhysics;
    public DetectPhysics detectPhysics => m_detectPhysics;
    private Animator m_fsm;
    private CharacterFSM.VirtualState m_currentState;
    
#if UNITY_EDITOR
    private bool m_drawDebug = false;
#endif
    
    public void SetState(CharacterFSM.VirtualState _state)
    {
        m_currentState = _state;
    }
    void Awake()
    {
        m_rigidbody = GetComponent<Rigidbody2D>();
        m_fsm = GetComponent<Animator>();
        m_detectPhysics = GetComponent<DetectPhysics>();
    }

    private void OnEnable()
    {
        InputController.OnDash += Dash;
        InputController.OnJump += Jump;
        InputController.OnInteract += Interact;
        
#if UNITY_EDITOR
        InputController.OnDrawDebug += DrawDebug;
#endif
    }

    private void OnDisable()
    {
        InputController.OnDash -= Dash;
        InputController.OnJump -= Jump;
        InputController.OnInteract -= Interact;
#if UNITY_EDITOR
        InputController.OnDrawDebug -= DrawDebug;
#endif
    }

    void Update()
    {
        float moveInput = GetMoveInput();
        m_body.localScale = new Vector3(math.abs(moveInput) > EPSILON? moveInput : m_body.localScale.x,1.0f,1.0f);
        
        m_fsm.SetBool("isOnGround", detectPhysics.isOnGround);
        Debug.Log(m_detectRobotRef.detectRobot);
        m_fsm.SetBool("detectRobot", m_detectRobotRef.detectRobot);
    }

    private void FixedUpdate()
    {
        if(m_currentState) m_currentState.OnFixedUpdate();
        
#if UNITY_EDITOR
        if (m_drawDebug)
        {
            Debug.DrawLine(transform.position, transform.position + (Vector3)m_rigidbody.velocity * 0.1f, Color.white, 10.0f);
        }
#endif
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
        moveInput = (math.abs(moveInput) > EPSILON) ? math.sign(moveInput) : 0f;
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
#if UNITY_EDITOR
    private void DrawDebug()
    {
        m_drawDebug = !m_drawDebug;
    }
#endif
#endregion
}
