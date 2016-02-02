using UnityEngine;
using System.Collections.Generic;

public class Player : Actor
{
    [SerializeField]
    private bool m_IsActive;
    [SerializeField]
    private float m_HitPoints;

    [SerializeField]
    private Vector3 m_Velocity;
    [SerializeField]
    private Vector3 m_Decay;
    [SerializeField]
    private Vector3 m_Speed;
    [SerializeField]
    private Vector3 m_MaxSpeed;

    //private Vector3 m_PrevPos;

    [SerializeField]
    private bool m_CanJump;

    enum MovementState { IDLE, WALKING, RUNNING, JUMPING, LANDING };

    enum PlayerState { HUMAN, SPIRIT };

    [SerializeField]
    private MovementState m_MovementState = MovementState.IDLE;
    [SerializeField]
    private PlayerState m_PlayerState = PlayerState.HUMAN;
    [SerializeField]
    private Animator m_Animator;

    [SerializeField]
    private Rigidbody m_Rigidbody;

    private List<Platform> m_Platforms;

    [SerializeField]
    private float m_JumpTimer;

    protected virtual void Awake()
    {
        //Debug.Log("Awake");

        m_Platforms = new List<Platform>();
        m_CanJump = false;
    }

    // Use this for initialization
    protected override void Start()
    {
        //Debug.Log("Start");

        base.Start();

        //m_PrevPos = transform.position;
    }

    protected virtual void FixedUpdate()
    {
        //print("FixedUpdate");

        if (Input.GetAxisRaw("Horizontal") != 0)
        {
            if (m_MovementState != MovementState.JUMPING)
                m_MovementState = MovementState.WALKING;

            if (Input.GetAxisRaw("Horizontal") == 1)
            {
                transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
                m_Velocity = new Vector3(Mathf.Clamp(m_Velocity.x + (m_Speed.x * Time.deltaTime), -m_MaxSpeed.x, m_MaxSpeed.x), m_Velocity.y, m_Velocity.z);
            }
            if (Input.GetAxisRaw("Horizontal") == -1)
            {
                transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
                m_Velocity = new Vector3(Mathf.Clamp(m_Velocity.x - (m_Speed.x * Time.deltaTime), -m_MaxSpeed.x, m_MaxSpeed.x), m_Velocity.y, m_Velocity.z);
            }
        }
        else if (m_MovementState != MovementState.JUMPING && m_Velocity.x != 0)
        {
            if (m_Velocity.x > 0)
                m_Velocity = new Vector3(Mathf.Clamp(m_Velocity.x - (m_Decay.x * Time.deltaTime), 0, m_MaxSpeed.x), m_Velocity.y, m_Velocity.z);
            if (m_Velocity.x < 0)
                m_Velocity = new Vector3(Mathf.Clamp(m_Velocity.x + (m_Decay.x * Time.deltaTime), -m_MaxSpeed.x, 0), m_Velocity.y, m_Velocity.z);
        }

        if (Input.GetAxisRaw("Jump") != 0 && m_CanJump)
        {
            if (m_MovementState == MovementState.JUMPING && m_JumpTimer > 0)
            {
                m_JumpTimer -= Time.deltaTime;
                m_Rigidbody.AddForce(new Vector3(0.0f, m_Speed.y, 0.0f));
            }
            else if (m_MovementState != MovementState.JUMPING)
            {
                m_MovementState = MovementState.JUMPING;

                m_JumpTimer = 0.25f;
                m_Rigidbody.AddForce(new Vector3(0.0f, m_Decay.y, 0.0f));
            }

            if (m_JumpTimer <= 0)
                m_CanJump = false;
        }
        else if(m_MovementState == MovementState.JUMPING)
            m_CanJump = false;

        Move();

        if (Mathf.Abs(m_Velocity.x) <= 0.001)
        {
            if (Input.GetAxisRaw("Horizontal") == 0 && (m_MovementState == MovementState.LANDING || m_MovementState == MovementState.WALKING))
                m_MovementState = MovementState.IDLE;

            m_Velocity = new Vector3(0, m_Velocity.y, m_Velocity.z);
        }

        //m_PrevPos = transform.position;
        //m_Rigidbody.velocity = new Vector3(0, m_Rigidbody.velocity.y, 0);
    }

    protected virtual void LateUpdate()
    {
        //print("LateUpdate");

        m_Animator.SetInteger("Player_State", (int)m_MovementState);
    }

    protected override void Move()
    {
        transform.position += m_Velocity;

        //print("Number of Items: " + m_Platforms.Count.ToString());
        foreach (Platform platform in m_Platforms)
            transform.position += platform.Velocity;
    }

    protected virtual void OnCollisionStay(Collision collision)
    {
        //print("OnCollisionStay");

        foreach (ContactPoint contact in collision.contacts)
        {
            if (contact.normal == new Vector3(0.0f, 1.0f, 0.0f))
            {
                m_CanJump = true;

                if (m_MovementState == MovementState.JUMPING)
                    m_MovementState = MovementState.LANDING;
            }
            else if ((contact.normal == new Vector3(-1.0f, 0.0f, 0.0f) && m_Velocity.x > 0) || (contact.normal == new Vector3(1.0f, 0.0f, 0.0f) && m_Velocity.x < 0))
                m_Velocity = new Vector3(0, m_Velocity.y, 0);

        }
    }
    protected virtual void OnCollisionEnter(Collision collision)
    {
        //print("OnCollisionEnter");

        if (collision.gameObject.GetComponent<Platform>())
        {
            foreach (ContactPoint contact in collision.contacts)
            {
                if (contact.normal == new Vector3(0.0f, 1.0f, 0.0f) && !m_Platforms.Exists(x => x == collision.gameObject.GetComponent<Platform>()))
                {
                    m_Platforms.Add(collision.gameObject.GetComponent<Platform>());
                }
            }
        }
    }
    protected virtual void OnCollisionExit(Collision collision)
    {
        //print("OnCollisionExit");

        if (collision.gameObject.GetComponent<Platform>())
        {
            m_Platforms.Remove(collision.gameObject.GetComponent<Platform>());
        }
    }
}
