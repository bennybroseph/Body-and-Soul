using UnityEngine;
using System.Collections;

// This class is used as a base for anything that reacts to stimuli such as the player character or enemies
public class Actor : DynamicObject
{
    public enum MovementStates { IDLE, DUCK, WALKING, RUNNING, JUMPING, LANDING, DEAD };

    [ReadOnly, SerializeField]
    protected MovementStates m_MovementState;
    [ReadOnly, SerializeField, Tooltip("Whether the actor can jump or not")]
    protected bool m_CanJump;
    [ReadOnly, SerializeField, Tooltip("Whether the jump height can still be affected by stimuli")]
    protected bool m_CanAffectJump;
    [ReadOnly, SerializeField]
    protected float m_JumpTimer;

    [SerializeField, Tooltip("How much speed is lost when sliding on the ground")]
    protected float m_Decay;
    [SerializeField, Tooltip("How much speed is gained while holding a Horizontal key")]
    protected float m_Speed;
    [SerializeField, Tooltip("The maximum horizontal speed")]
    protected float m_MaxSpeed;

    [SerializeField, Tooltip("Amount of force to add while holding the jump key")]
    protected float m_IncrementalJumpForce;
    [SerializeField, Tooltip("Amount of force to add the first time the jump key is pressed")]
    protected float m_InitialJumpForce;
    [SerializeField, Tooltip("Amount of time to allow the control of jump height")]
    protected float m_MaxJumpTime;

    [SerializeField]
    protected float m_HitPoints;

    [SerializeField]
    protected Animator m_Animator;
    [SerializeField]
    protected Rigidbody m_Rigidbody;

    public float HitPoints { get { return m_HitPoints; } set { m_HitPoints = value; } }
    public MovementStates MovementState { get { return m_MovementState; } set { m_MovementState = value; } }

    protected override void Start()
    {
        base.Start();

        m_MovementState = MovementStates.IDLE;
        m_CanJump = false;
        m_JumpTimer = 0;
        
        if (m_Animator == null)
            m_Animator = GetComponent<Animator>();
        if (m_Rigidbody == null)
            m_Rigidbody = GetComponent<Rigidbody>();
    }

    protected virtual void OnValidate()
    {
        if (m_HitPoints < 0)
            m_HitPoints = 0;
    }
}
