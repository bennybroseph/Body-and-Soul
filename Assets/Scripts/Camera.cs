using UnityEngine;
using System.Collections;

public class Camera : MonoBehaviour
{
    [SerializeField]
    protected GameObject m_Following;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = new Vector3(m_Following.transform.position.x, m_Following.transform.position.y, m_Following.transform.position.z - 15.0f);
    }
}
