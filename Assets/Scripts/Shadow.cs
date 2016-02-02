using UnityEngine;
using System.Collections;

public class Shadow : MonoBehaviour
{
    [SerializeField]
    protected Vector3 m_Offset;
    [SerializeField]
    protected GameObject m_GameObject;
    [SerializeField]
    protected SpriteRenderer m_SpriteRenderer;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = m_GameObject.transform.position + m_Offset;
        m_SpriteRenderer.sprite = m_GameObject.GetComponent<SpriteRenderer>().sprite;
    }
}
