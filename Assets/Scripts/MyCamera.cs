using UnityEngine;
using System.Collections;

public class MyCamera : MonoBehaviour
{
    [SerializeField]
    protected GameObject m_Following;
    [SerializeField]
    protected Vector3 m_Offset;

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
        transform.position = m_Following.transform.position + m_Offset;
    }
}
