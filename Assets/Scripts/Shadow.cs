using UnityEngine;
using System.Collections;

public class Shadow : MonoBehaviour
{
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
        transform.position = new Vector3(m_GameObject.transform.position.x + 0.5f, m_GameObject.transform.position.y - 0.5f, m_GameObject.transform.position.z);
        m_SpriteRenderer.sprite = m_GameObject.GetComponent<SpriteRenderer>().sprite;
    }
}
