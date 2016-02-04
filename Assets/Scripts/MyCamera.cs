using UnityEngine;
using System.Collections;

public class MyCamera : MonoBehaviour
{
    [SerializeField]
    protected GameObject m_Following;
    [SerializeField]
    protected Vector3 m_Offset;

    [SerializeField, Tooltip("How close the camera should get before it decides that it should stop trying to be more accurate")]
    protected float m_CloseEnough;

    public GameObject Following
    {
        get
        {
            return m_Following;
        }
        set
        {
            m_Following = value;
        }
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = new Vector3(m_Following.transform.position.x + m_Offset.x, m_Following.transform.position.y + m_Offset.y, transform.position.z);

        if (transform.position.z < m_Following.transform.position.z + m_Offset.z - m_CloseEnough)
        {
            transform.position += new Vector3(0.0f, 0.0f, (((m_Following.transform.position.z + m_Offset.z) - transform.position.z) * 3) * Time.deltaTime);
            transform.position = new Vector3(transform.position.x, transform.position.y, Mathf.Clamp(transform.position.z, transform.position.z, m_Following.transform.position.z + m_Offset.z - m_CloseEnough));
        }
        else if (transform.position.z > (m_Following.transform.position.z + m_Offset.z) + m_CloseEnough)
        {
            transform.position += new Vector3(0.0f, 0.0f, (((m_Following.transform.position.z + m_Offset.z) - transform.position.z) * 3) * Time.deltaTime);
            transform.position = new Vector3(transform.position.x, transform.position.y, Mathf.Clamp(transform.position.z, m_Following.transform.position.z + m_Offset.z + m_CloseEnough, transform.position.z));
        }
        else
            transform.position = m_Following.transform.position + m_Offset;
    }
}
