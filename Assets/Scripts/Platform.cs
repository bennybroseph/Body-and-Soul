using UnityEngine;
using System.Collections;

public class Platform : MonoBehaviour {

    [SerializeField]
    private bool m_IsHuman;
    [SerializeField]
    private bool isSpirit;
    [SerializeField]
    protected Vector3 m_Velocity;
    [SerializeField]
    protected float m_Time;
    [SerializeField]
    protected Vector3 m_Distance;

    protected Vector3 m_Origin;

    public Vector3 Velocity
    {
        get
        {
            return m_Velocity;
        }

        set
        {
            m_Velocity = value;
        }
    }

    public bool IsHuman
    {
        get
        {
            return m_IsHuman;
        }

        set
        {
            m_IsHuman = value;
        }
    }
    public bool IsSpirit
    {
        get
        {
            return isSpirit;
        }

        set
        {
            isSpirit = value;
        }
    }

    virtual protected void Start()
    {
        m_Origin = transform.position;
    }

    protected void MoveForward()
    {
        m_Velocity = new Vector3(m_Distance.x * (Time.deltaTime / m_Time), m_Velocity.y, m_Velocity.z);

        transform.Translate(m_Velocity);
    }

    protected void MoveBack()
    {
        m_Velocity = new Vector3(-m_Distance.x * (Time.deltaTime / m_Time), m_Velocity.y, m_Velocity.z);

        transform.Translate(m_Velocity);
    }

    protected void MoveUp()
    {
        m_Velocity = new Vector3(m_Velocity.x, m_Distance.y * (Time.deltaTime / m_Time), m_Velocity.z);

        transform.Translate(m_Velocity);
    }

    protected void MoveDown()
    {
        m_Velocity = new Vector3(m_Velocity.x, -m_Distance.y * (Time.deltaTime / m_Time), m_Velocity.z);

        transform.Translate(m_Velocity);
    }

    protected void Snap()
    {

    }
}