using UnityEngine;
using System.Collections;

public class Platform : MonoBehaviour {

    [SerializeField]
    protected bool m_IsHuman;
    [SerializeField]
    protected bool m_IsSpirit;
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

    virtual protected void Start()
    {
        m_Origin = transform.position;
    }

    protected void MoveForward()
    {
        Velocity = new Vector3(m_Distance.x * (Time.deltaTime / m_Time), Velocity.y, Velocity.z);

        transform.Translate(Velocity);
    }

    protected void MoveBack()
    {
        Velocity = new Vector3(-m_Distance.x * (Time.deltaTime / m_Time), Velocity.y, Velocity.z);

        transform.Translate(Velocity);
    }

    protected void MoveUp()
    {
        Velocity = new Vector3(Velocity.x, m_Distance.y * (Time.deltaTime / m_Time), Velocity.z);

        transform.Translate(Velocity);
    }

    protected void MoveDown()
    {
        Velocity = new Vector3(Velocity.x, -m_Distance.y * (Time.deltaTime / m_Time), Velocity.z);

        transform.Translate(Velocity);
    }

    protected void Snap()
    {

    }
}