using Unity.Mathematics;
using UnityEngine;

public class Character : MonoBehaviour
{
    public static readonly float EPSILON = 1.401298E-45f;
    
    [SerializeField] private float m_maxSpeed = 10.0f;
    [SerializeField] private float m_verticalSpeedGrabModifier = 0.5f;
    [SerializeField] private float m_horizontalSpeedGrabModifier = 0.5f;
    // Serialize
    [SerializeField] private Transform m_body;
    [SerializeField] private Transform m_robotGrabPos;
    
    [SerializeField] private DetectRobot m_detectRobotRef;

    // Private
    private Rigidbody2D m_rigidbody;
    private DetectPhysics m_detectPhysics;
    private Animator m_fsm;
    private CharacterFSM.VirtualState m_currentState;
    private Robot m_robotRef;
    
#if UNITY_EDITOR
    private bool m_drawDebug = false;
#endif
    
    // Getter
    public float maxSpeed => m_maxSpeed;
    public Transform body => m_body;
    public Transform robotGrabPos => m_robotGrabPos;
    public DetectPhysics detectPhysics => m_detectPhysics;
    public DetectRobot detectRobot => m_detectRobotRef;
    
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
        m_fsm.SetBool("isOnGround", detectPhysics.isOnGround);
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
    public void GrabRobot(Robot _robot)
    {
        m_fsm.SetBool("grabRobot", true);
        _robot.Grab();
        _robot.transform.parent = robotGrabPos;
        _robot.transform.localPosition = Vector3.zero;
        _robot.transform.localScale= Vector3.one;
        m_robotRef = _robot;
    }
    public void DropRobot()
    {
        m_fsm.SetBool("grabRobot", false);
        m_robotRef.Drop();
        m_robotRef.transform.parent = null;
        m_robotRef = null;
    }

#region Physics
    public Vector2 GetCurrentVelocity()
    {
        return m_rigidbody.velocity;
    }
    public void SetDesiredVelocity(Vector2 _velocity, bool _ignoreVertical = true)
    {
        bool grab = m_fsm.GetBool("grabRobot");
        m_rigidbody.velocity = Vector2.right * (_velocity.x * (grab? m_horizontalSpeedGrabModifier : 1.0f))
                               + Vector2.up * (_ignoreVertical ? m_rigidbody.velocity.y : _velocity.y * (grab? m_verticalSpeedGrabModifier : 1.0f));
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
        if (m_fsm.GetBool("grabRobot"))
        {
            DropRobot();
        }
        else m_fsm.SetTrigger("Interact");
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
