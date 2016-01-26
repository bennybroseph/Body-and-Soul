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
    protected float m_Distance;

    protected Vector3 m_Origin;

    virtual protected void Start()
    {
        m_Origin = transform.position;
    }

    protected void MoveForward()
    {
        transform.Translate(Vector3.right * m_Distance * (Time.deltaTime / m_Time));
        //transform.position = new Vector3(Mathf.Clamp(transform.position.x, m_Origin.x, m_Origin.x + m_Distance), transform.position.y, transform.position.z);
    }

    protected void MoveBack()
    {
        transform.Translate(Vector3.left * m_Distance * (Time.deltaTime / m_Time));
        //transform.position = new Vector3(Mathf.Clamp(transform.position.x, m_Origin.x - m_Distance, m_Origin.x), transform.position.y, transform.position.z);
    }

    protected void MoveUp()
    {
        transform.Translate(Vector3.up * m_Distance * (Time.deltaTime / m_Time));
    }

    protected void MoveDown()
    {
        transform.Translate(Vector3.down * m_Distance * (Time.deltaTime / m_Time));
    }

    protected void Snap()
    {

    }
}
