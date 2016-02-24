using UnityEngine;
using System.Collections.Generic;

public class Spikes : DynamicObject
{
    [SerializeField]
    protected List<DynamicObject> m_DynamicObjects;
    // Use this for initialization
    protected override void Start()
    {
        base.Start();
    }

    protected override void UpdateVelocity()
    {
        base.UpdateVelocity();

        foreach (DynamicObject dynamicObject in m_DynamicObjects)
            m_TotalVelocity += dynamicObject.TotalVelocity;
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
}
