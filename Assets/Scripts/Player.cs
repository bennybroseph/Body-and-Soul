﻿using UnityEngine;
using System.Collections.Generic;

public class Player : MonoBehaviour
{
    [SerializeField]
    bool m_IsActive;
    [SerializeField]
    float m_HitPoints;

    [SerializeField]
    Vector3 m_Velocity;
    [SerializeField]
    Vector3 m_Decay;
    [SerializeField]
    Vector3 m_Speed;
    [SerializeField]
    Vector3 m_MaxSpeed;

    Vector3 m_PrevPos;

    [SerializeField]
    bool m_CanJump;

    enum PlayerState { IDLE, WALKING, RUNNING, JUMPING, LANDING };

    [SerializeField]
    PlayerState m_State = PlayerState.IDLE;
    [SerializeField]
    Animator m_Animator;

    [SerializeField]
    Rigidbody m_Rigidbody;    

    List<Platform> m_Platforms;

    protected virtual void Awake()
    {
        //Debug.Log("Awake");

        m_Platforms = new List<Platform>();
        m_CanJump = false;
    }

    // Use this for initialization
    protected virtual void Start()
    {
        //Debug.Log("Start");

        m_PrevPos = transform.position;        
    }

    protected virtual void FixedUpdate()
    {
        //print("FixedUpdate");

        if (Input.GetAxisRaw("Horizontal") != 0)
        {
            if (m_State != PlayerState.JUMPING)
                m_State = PlayerState.WALKING;

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
        else if (m_State != PlayerState.JUMPING && m_Velocity.x != 0)
        {
            if (m_Velocity.x > 0)
                m_Velocity = new Vector3(Mathf.Clamp(m_Velocity.x - (m_Decay.x * Time.deltaTime), 0, m_MaxSpeed.x), m_Velocity.y, m_Velocity.z);
            if (m_Velocity.x < 0)
                m_Velocity = new Vector3(Mathf.Clamp(m_Velocity.x + (m_Decay.x * Time.deltaTime), -m_MaxSpeed.x, 0), m_Velocity.y, m_Velocity.z);
        }

        if (Input.GetAxisRaw("Jump") != 0 && m_CanJump)
        {
            m_State = PlayerState.JUMPING;

            m_CanJump = false;
            m_Rigidbody.AddForce(new Vector3(0.0f, m_Speed.y, 0.0f));
        }

        Move();

        if (Mathf.Abs(m_Velocity.x) <= 0.001)
        {
            if (Input.GetAxisRaw("Horizontal") == 0 && (m_State == PlayerState.LANDING || m_State == PlayerState.WALKING))
                m_State = PlayerState.IDLE;

            m_Velocity = new Vector3(0, m_Velocity.y, m_Velocity.z);
        }

        m_PrevPos = transform.position;
        //m_Rigidbody.velocity = new Vector3(0, m_Rigidbody.velocity.y, 0);
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        //print("Update");            
    }

    protected virtual void LateUpdate()
    {
        //print("LateUpdate");

        m_Animator.SetInteger("Player_State", (int)m_State);
    }

    protected virtual void Move()
    {
        transform.position += m_Velocity;

        //print("Number of Items: " + m_Platforms.Count.ToString());
        foreach (Platform platform in m_Platforms)
            transform.position += platform.Velocity;
    }

    protected virtual void OnCollisionStay(Collision collision)
    {
        print("OnCollisionStay");
        
        foreach (ContactPoint contact in collision.contacts)
        {
            if (contact.normal == new Vector3(0.0f, 1.0f, 0.0f))
            {
                m_CanJump = true;

                if (m_State == PlayerState.JUMPING)
                    m_State = PlayerState.LANDING;
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
