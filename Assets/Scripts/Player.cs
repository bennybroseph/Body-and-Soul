using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{
    [SerializeField]
    bool m_IsActive;
    [SerializeField]
    float m_HitPoints;

    [SerializeField]
    Vector3 m_Velocity;
    [SerializeField]
    Vector3 m_Speed;
    [SerializeField]
    Vector3 m_MaxSpeed;

    Vector3 m_PrevPos;

    [SerializeField]
    bool m_CanJump = true;

    enum PlayerState { IDLE, WALKING, RUNNING, JUMPING, LANDING };

    [SerializeField]
    PlayerState m_State = PlayerState.IDLE;
    [SerializeField]
    Animator m_Animator;

    [SerializeField]
    Rigidbody m_Rigidbody;

    protected virtual void Awake()
    {
        Debug.Log("Awake");
    }

    // Use this for initialization
    protected virtual void Start()
    {
        Debug.Log("Start");
        m_PrevPos = transform.position;
    }

    protected virtual void FixedUpdate()
    {
        print("FixedUpdate");

        if (Input.GetAxisRaw("Horizontal") != 0)
        {
            if (m_State != PlayerState.JUMPING)
            {
                m_State = PlayerState.WALKING;
                m_Animator.SetTrigger("Player_Walking");
            }

            if (Input.GetAxisRaw("Horizontal") == 1)
                m_Velocity = new Vector3(Mathf.Clamp(m_Velocity.x + (m_Speed.x * Time.deltaTime), -m_MaxSpeed.x, m_MaxSpeed.x), m_Velocity.y, m_Velocity.z);
            if (Input.GetAxisRaw("Horizontal") == -1)
                m_Velocity = new Vector3(Mathf.Clamp(m_Velocity.x - (m_Speed.x * Time.deltaTime), -m_MaxSpeed.x, m_MaxSpeed.x), m_Velocity.y, m_Velocity.z);
        }
        else if (m_State != PlayerState.JUMPING && m_Velocity.x != 0)
        {
            if (m_Velocity.x > 0)
                m_Velocity = new Vector3(Mathf.Clamp(m_Velocity.x - (m_Speed.x * Time.deltaTime), 0, m_MaxSpeed.x), m_Velocity.y, m_Velocity.z);
            if (m_Velocity.x < 0)
                m_Velocity = new Vector3(Mathf.Clamp(m_Velocity.x + (m_Speed.x * Time.deltaTime), -m_MaxSpeed.x, 0), m_Velocity.y, m_Velocity.z);
        }

        if (Input.GetAxisRaw("Jump") != 0 && m_CanJump)
        {
            m_State = PlayerState.JUMPING;
            m_Animator.SetTrigger("Player_Jumping");

            m_CanJump = false;
            m_Velocity = new Vector3(m_Velocity.x, m_MaxSpeed.y, m_Velocity.z);
            print(m_Velocity.y);
        }

        m_Velocity = new Vector3(m_Velocity.x, Mathf.Clamp(m_Velocity.y - (m_Speed.y * Time.deltaTime), -m_MaxSpeed.y, m_MaxSpeed.y), m_Velocity.z);

        Move();

        if (m_PrevPos == transform.position)
        {
            print("Yes");

            m_State = PlayerState.IDLE;
            m_Animator.SetTrigger("Player_Idle");

            m_Velocity = new Vector3(0, 0, 0);
        }

        m_PrevPos = transform.position;
        m_Rigidbody.velocity = new Vector3(0, 0, 0);
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        print("Update");            
    }

    protected virtual void LateUpdate()
    {
        print("LateUpdate");
    }

    protected virtual void Move()
    {
        transform.position += m_Velocity;
    }

    protected virtual void OnCollisionStay(Collision collision)
    {
        print("OnCollisionStay");
        //m_CanJump = false;
        foreach (ContactPoint contact in collision.contacts)
        {
            if (contact.normal == new Vector3(0.0f, 1.0f, 0.0f))
            {
                m_CanJump = true;

                if (m_State == PlayerState.JUMPING)
                {
                    m_State = PlayerState.LANDING;
                    m_Animator.SetTrigger("Player_Landing");
                }
                else
                    m_Velocity = new Vector3(m_Velocity.x, 0, m_Velocity.z);
            }
            else if (contact.normal == new Vector3(-1.0f, 0.0f, 0.0f) && Input.GetAxisRaw("Horizontal") == 1)
            {
                if (m_State != PlayerState.JUMPING)
                {
                    //m_State = PlayerState.IDLE;
                    m_Velocity = new Vector3(0, m_Velocity.y, 0);
                }
            }
        }
    }
}
