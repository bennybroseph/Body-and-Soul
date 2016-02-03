using UnityEngine;
using System.Collections;

public class Spirit : Player
{
    [SerializeField]
    private bool m_CanHover;
    [SerializeField]
    private Vector3 m_HoverPos;

    // Use this for initialization
    protected override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }

    protected override void Jump()
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
        else if(m_Rigidbody.velocity.y <= 0.0f && !m_CanHover)
        {
            m_CanHover = true;
            m_HoverPos = transform.position;          
        }
        else if(m_CanHover)
        {
            m_Rigidbody.velocity = new Vector3(0.0f, 0.0f, 0.0f);
            transform.position = new Vector3(transform.position.x, m_HoverPos.y, transform.position.z);
        }
    }

    protected override void EndJump()
    {
        m_CanHover = false;
    }
}
