using UnityEngine;
using System.Collections;

public class Shadow : MonoBehaviour
{
    [SerializeField]
    private Vector3 m_Offset;
    //[SerializeField]
    private GameObject m_ParentObject;
    [SerializeField]
    private SpriteRenderer m_SpriteRenderer;

    // Use this for initialization
    void Start()
    {
        if (m_SpriteRenderer == null)
            m_SpriteRenderer = GetComponent<SpriteRenderer>();
        if (m_ParentObject == null)
            m_ParentObject = gameObject.transform.parent.gameObject;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = m_ParentObject.transform.position + m_Offset;

        if(m_ParentObject.GetComponent<SpriteRenderer>().sprite != null)
            m_SpriteRenderer.sprite = m_ParentObject.GetComponent<SpriteRenderer>().sprite;
    }
}
