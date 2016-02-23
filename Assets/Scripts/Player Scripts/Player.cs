using UnityEngine;
using System.Collections.Generic;

public class Player : Actor
{
    [ReadOnly, SerializeField]
    protected List<GameObject> m_CurrentlyTouching;
    [ReadOnly, SerializeField]
    protected List<DynamicObject> m_DynamicObjects;

    [SerializeField]
    protected Game_Controller TheGame;

    // Use this for initialization
    protected override void Start()
    {
        //Debug.Log("Start");

        base.Start();

        m_DynamicObjects = new List<DynamicObject>();
        m_CanJump = false;

        m_CurrentlyTouching = new List<GameObject>();

        TheGame = FindObjectOfType<Game_Controller>();
        if (TheGame == null)
            Debug.Log("You forgot to add a Game Controller...");
    }

    protected override void CalculateVelocity()
    {
        base.CalculateVelocity();

        //if (m_CurrentlyTouching.Count == 0)
        //    m_CanJump = false;
        //else if (Input.GetAxisRaw("Jump") == 0)
        //{
        //    //m_MaxJumpTime = 0;
        //    m_CanJump = true;
        //    //m_CanAffectJump = true;
        //}

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

            //////////////////////////////////////
            //          Jump Code               //
            //////////////////////////////////////

            if (Input.GetAxisRaw("Jump") != 0)
            {
                Jump();

                if (m_JumpTimer <= 0)
                    m_CanJump = false;
            }
            else if (m_MovementState == MovementStates.JUMPING && m_CanJump)
                m_CanJump = false;
            else
                EndJump();

            //////////////////////////////////////
            //          /Jump Code              //
            //////////////////////////////////////

            if (Input.GetAxisRaw("Vertical") < 0 && m_MovementState == MovementStates.IDLE)
                m_MovementState = MovementStates.DUCK;
            else if (Input.GetAxisRaw("Vertical") == 0 && m_MovementState == MovementStates.DUCK)
                m_MovementState = MovementStates.IDLE;
        }

        if (Input.GetAxisRaw("Horizontal") == 0 && m_MovementState != MovementStates.JUMPING && m_Velocity.x != 0)
        {
            if (m_Velocity.x > 0)
                m_Velocity = new Vector3(Mathf.Clamp(m_Velocity.x - (m_Decay * Time.deltaTime), 0, m_MaxSpeed), m_Velocity.y, m_Velocity.z);
            if (m_Velocity.x < 0)
                m_Velocity = new Vector3(Mathf.Clamp(m_Velocity.x + (m_Decay * Time.deltaTime), -m_MaxSpeed, 0), m_Velocity.y, m_Velocity.z);
        }
        if (Mathf.Abs(m_Velocity.x) <= 0.001)
        {
            if (Input.GetAxisRaw("Horizontal") == 0 && (m_MovementState == MovementStates.LANDING || m_MovementState == MovementStates.WALKING))
                m_MovementState = MovementStates.IDLE;

            m_Velocity = new Vector3(0, m_Velocity.y, m_Velocity.z);
        }
    }

    protected virtual void Jump()
    {
        if (m_CanJump)
        {
            if (m_MovementState == MovementStates.JUMPING && m_JumpTimer > 0)
            {
                m_Rigidbody.AddForce(new Vector3(0.0f, m_IncrementalJumpForce, 0.0f));

                m_JumpTimer -= Time.deltaTime;
            }
            else if (m_MovementState != MovementStates.JUMPING)
            {
                m_MovementState = MovementStates.JUMPING;
                m_Rigidbody.AddForce(new Vector3(0.0f, m_InitialJumpForce, 0.0f));

                m_JumpTimer = m_MaxJumpTime;
            }
        }
    }
    protected virtual void EndJump()
    {

    }

    protected override void UpdateVelocity()
    {
        base.UpdateVelocity();

        foreach (DynamicObject dynamicObject in m_DynamicObjects)
            m_TotalVelocity += dynamicObject.TotalVelocity;
    }

    protected virtual void LateUpdate()
    {
        //print("LateUpdate");

        if (m_DamageTimer >= 0)
            m_DamageTimer -= Time.deltaTime;
        else
            m_IsHurting = false;

        if (m_MovementState != MovementStates.LANDING)
            m_Animator.SetInteger("Player_State", (int)m_MovementState);
        if (m_MovementState == MovementStates.WALKING)
            m_Animator.speed = Mathf.Abs(m_Velocity.x) * 10 + 0.8f;
    }

    protected virtual void OnCollisionStay(Collision collision)
    {
        //print("OnCollisionStay");

        foreach (ContactPoint contact in collision.contacts)
        {
            if (contact.normal == new Vector3(0.0f, 1.0f, 0.0f))
            {
                if (Input.GetAxisRaw("Jump") == 0)
                {
                    m_CanJump = true;
                    //m_CanAffectJump = true;
                }

                if (m_MovementState == MovementStates.JUMPING)
                    m_MovementState = MovementStates.LANDING;
            }
            if ((contact.normal == new Vector3(-1.0f, 0.0f, 0.0f) && m_Velocity.x > 0) || (contact.normal == new Vector3(1.0f, 0.0f, 0.0f) && m_Velocity.x < 0))
            {
                m_Rigidbody.position -= m_Velocity;
                m_Velocity = new Vector3(0, m_Velocity.y, 0);
            }
        }
    }
    protected virtual void OnCollisionEnter(Collision collision)
    {
        //print("OnCollisionEnter");
        foreach (ContactPoint contact in collision.contacts)
        {
            if (contact.normal == new Vector3(0.0f, 1.0f, 0.0f) && collision.gameObject.GetComponent<DynamicObject>() && !m_DynamicObjects.Exists(x => x == collision.gameObject.GetComponent<DynamicObject>()))
            {
                m_DynamicObjects.Add(collision.gameObject.GetComponent<DynamicObject>());

                if (!m_IsHurting && collision.gameObject.GetComponent<Spikes>() != null)
                {
                    m_IsHurting = true;
                    m_DamageTimer = m_MaxDamageTimer;
                    m_HitPoints -= 1;
                }
            }
            if (contact.normal == new Vector3(0.0f, 1.0f, 0.0f) && !m_CurrentlyTouching.Exists(x => x == collision.gameObject))
            {
                if (Input.GetAxisRaw("Jump") == 0)
                {
                    m_CanJump = true;
                    //m_CanAffectJump = true;
                }

                if (m_MovementState == MovementStates.JUMPING)
                    m_MovementState = MovementStates.LANDING;
                m_CurrentlyTouching.Add(collision.gameObject);
            }
        }
    }
    protected virtual void OnCollisionExit(Collision collision)
    {
        //print("OnCollisionExit");

        if (collision.gameObject.GetComponent<DynamicObject>())
            m_DynamicObjects.Remove(collision.gameObject.GetComponent<DynamicObject>());

        if (m_CurrentlyTouching.Exists(x => x == collision.gameObject))
            m_CurrentlyTouching.Remove(collision.gameObject);
    }

    protected override void OnValidate()
    {
        base.OnValidate();

        if (m_Decay < 0)
            m_Decay = 0;
    }
}
