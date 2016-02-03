using UnityEngine;
using System.Collections.Generic;

public class Player : Actor
{
    [SerializeField]
    protected float m_HitPoints;

    protected List<Platform> m_Platforms;
    protected List<Player> m_Players;

    // Use this for initialization
    protected override void Start()
    {
        //Debug.Log("Start");

        base.Start();

        m_Platforms = new List<Platform>();
        m_Players = new List<Player>();
        m_CanJump = false;
    }

    protected virtual void FixedUpdate()
    {
        //print("FixedUpdate");
        if (m_IsActive)
        {
            if (Input.GetAxisRaw("Horizontal") != 0)
            {
                if (m_MovementState != MovementStates.JUMPING)
                    m_MovementState = MovementStates.WALKING;

                if (Input.GetAxisRaw("Horizontal") == 1)
                {
                    transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
                    m_Velocity = new Vector3(Mathf.Clamp(m_Velocity.x + (m_Speed * Time.deltaTime), -m_MaxSpeed, m_MaxSpeed), m_Velocity.y, m_Velocity.z);
                }
                if (Input.GetAxisRaw("Horizontal") == -1)
                {
                    transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
                    m_Velocity = new Vector3(Mathf.Clamp(m_Velocity.x - (m_Speed * Time.deltaTime), -m_MaxSpeed, m_MaxSpeed), m_Velocity.y, m_Velocity.z);
                }
            }
            else if (m_MovementState != MovementStates.JUMPING && m_Velocity.x != 0)
            {
                if (m_Velocity.x > 0)
                    m_Velocity = new Vector3(Mathf.Clamp(m_Velocity.x - (m_Decay * Time.deltaTime), 0, m_MaxSpeed), m_Velocity.y, m_Velocity.z);
                if (m_Velocity.x < 0)
                    m_Velocity = new Vector3(Mathf.Clamp(m_Velocity.x + (m_Decay * Time.deltaTime), -m_MaxSpeed, 0), m_Velocity.y, m_Velocity.z);
            }

            if (Input.GetAxisRaw("Jump") != 0 && m_CanJump)
            {
                if (m_MovementState == MovementStates.JUMPING && m_JumpTimer > 0)
                {
                    m_JumpTimer -= Time.deltaTime;
                    m_Rigidbody.AddForce(new Vector3(0.0f, m_IncrementalJumpForce, 0.0f));
                }
                else if (m_MovementState != MovementStates.JUMPING)
                {
                    m_MovementState = MovementStates.JUMPING;

                    m_JumpTimer = m_MaxJumpTime;
                    m_Rigidbody.AddForce(new Vector3(0.0f, m_InitialJumpForce, 0.0f));
                }

                if (m_JumpTimer <= 0)
                    m_CanJump = false;
            }
            else if (m_MovementState == MovementStates.JUMPING)
                m_CanJump = false;

            if (Input.GetAxisRaw("Vertical") < 0 && m_MovementState == MovementStates.IDLE)
                m_MovementState = MovementStates.DUCK;
            else if (Input.GetAxisRaw("Vertical") == 0 && m_MovementState == MovementStates.DUCK)
                m_MovementState = MovementStates.IDLE;

        }        

        if (Mathf.Abs(m_Velocity.x) <= 0.001)
        {
            if (Input.GetAxisRaw("Horizontal") == 0 && (m_MovementState == MovementStates.LANDING || m_MovementState == MovementStates.WALKING))
                m_MovementState = MovementStates.IDLE;

            m_Velocity = new Vector3(0, m_Velocity.y, m_Velocity.z);
        }       
    }

    protected override void CalculateVelocity()
    {
        base.CalculateVelocity();

        foreach (Platform platform in m_Platforms)
            m_TotalVelocity += platform.Velocity;
        foreach (Player player in m_Players)
            m_TotalVelocity += player.TotalVelocity;
    }

    protected virtual void LateUpdate()
    {
        //print("LateUpdate");

        m_Animator.SetInteger("Player_State", (int)m_MovementState);
    }

    protected virtual void OnCollisionStay(Collision collision)
    {
        //print("OnCollisionStay");

        foreach (ContactPoint contact in collision.contacts)
        {
            if (contact.normal == new Vector3(0.0f, 1.0f, 0.0f))
            {
                m_CanJump = true;

                if (m_MovementState == MovementStates.JUMPING)
                    m_MovementState = MovementStates.LANDING;
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
        if (collision.gameObject.GetComponent<Player>())
        {
            foreach (ContactPoint contact in collision.contacts)
            {
                if (contact.normal == new Vector3(0.0f, 1.0f, 0.0f) && !m_Players.Exists(x => x == collision.gameObject.GetComponent<Player>()))
                {
                    m_Players.Add(collision.gameObject.GetComponent<Player>());
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
        if (collision.gameObject.GetComponent<Player>())
        {
            m_Players.Remove(collision.gameObject.GetComponent<Player>());
        }
    }
}
