using UnityEngine;
using System.Collections.Generic;

public class Button : DynamicObject
{
    [SerializeField]
    protected bool m_IsPressed;

    [SerializeField]
    protected Collider m_PlayerCollider;
    [SerializeField]
    protected Collider m_Collider;
    [SerializeField]
    protected SpriteRenderer m_SpriteRenderer;

    [SerializeField]
    protected Sprite m_PressedSprite;
    [SerializeField]
    protected Sprite m_DepressedSprite;

    [SerializeField]
    protected List<DynamicObject> m_DynamicObjects;

    public bool IsPressed
    {
        get { return m_IsPressed; }
    }

    // Use this for initialization
    protected override void Start()
    {
        base.Start();

        if (GetComponent<Collider>() != null)
            m_Collider = GetComponent<Collider>();
        if (GetComponent<SpriteRenderer>() != null)
            m_SpriteRenderer = GetComponent<SpriteRenderer>();

        Physics.IgnoreCollision(m_PlayerCollider, m_Collider);
    }
    
    protected override void UpdateVelocity()
    {
        base.UpdateVelocity();

        foreach (DynamicObject dynamicObject in m_DynamicObjects)
            m_TotalVelocity += dynamicObject.TotalVelocity;
    }

    protected void LateUpdate()
    {
        if (m_IsPressed)
            m_SpriteRenderer.sprite = m_PressedSprite;
        else
            m_SpriteRenderer.sprite = m_DepressedSprite;
    }

    protected void OnCollisionEnter(Collision collision)
    {
        foreach (ContactPoint contact in collision.contacts)
            if (contact.normal == new Vector3(0.0f, 1.0f, 0.0f) && collision.gameObject.GetComponent<DynamicObject>() && !m_DynamicObjects.Exists(x => x == collision.gameObject.GetComponent<DynamicObject>()))
                m_DynamicObjects.Add(collision.gameObject.GetComponent<DynamicObject>());
    }
    protected void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.GetComponent<DynamicObject>())
            m_DynamicObjects.Remove(collision.gameObject.GetComponent<DynamicObject>());
    }

    protected void OnTriggerEnter(Collider other)
    {
        if (other == m_PlayerCollider)
            m_IsPressed = !m_IsPressed;
    }
}
