using UnityEngine;
using System.Collections;

// This class is used as a base for all objects in the game which have a script i.e. not a static object
public class DynamicObject : MonoBehaviour
{
    [SerializeField, Tooltip("Whether an object is active or not")]
    protected bool m_IsActive = false;  // By default an object will be inactive to ensure explicit activation

    [SerializeField, Tooltip("The object's personal velocity without outside forces")]
    protected Vector3 m_Velocity;
    [SerializeField, Tooltip("The total velocity of an object after adding outside forces")]
    protected Vector3 m_TotalVelocity;

    public virtual bool IsActive
    {
        get
        {
            return m_IsActive;
        }

        set
        {
            m_IsActive = value;
        }
    }           // Get or set an object's active state

    public virtual Vector3 Velocity
    {
        get
        {
            return m_Velocity;
        }
        set
        {
            m_Velocity = value;
        }
    }        // Get or set an object's velocity
    public virtual Vector3 TotalVelocity
    {
        get
        {
            return m_TotalVelocity;
        }
        set
        {
            m_TotalVelocity = value;
        }
    }   // Get or set an object's total velocity

    // Use this for initialization
    protected virtual void Start()
    {
        m_Velocity = new Vector3(0.0f, 0.0f, 0.0f);

        m_TotalVelocity = m_Velocity;        
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        CalculateVelocity();
        UpdateVelocity();

        Move();
    }

    protected virtual void CalculateVelocity()
    {
        m_TotalVelocity = m_Velocity;
    }
    protected virtual void UpdateVelocity()
    {

    }

    // Used to move objects
    protected virtual void Move()
    {
        transform.position += m_TotalVelocity;
    }
}