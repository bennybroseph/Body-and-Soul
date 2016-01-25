using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{
    [SerializeField]
    bool m_IsActive;
    [SerializeField]
    float m_HitPoints;

    [SerializeField]
    private Rigidbody m_Rigidbody;

    bool ShouldJump = true;

    protected virtual void Awake()
    {
        Debug.Log("Awake");
    }

    // Use this for initialization
    protected virtual void Start()
    {
        Debug.Log("Start");
    }

    protected virtual void FixedUpdate()
    {
        float fMoveHorizontal = Input.GetAxis("Horizontal");
        float fJump = 0;

        //if (ShouldJump)
        //{
        if ((fJump = Input.GetAxis("Jump")) > 0)
        {
            //  ShouldJump = false;
        }
        // }

        Vector3 v3Movement = new Vector3(fMoveHorizontal * 1000 * Time.deltaTime, fJump * 800 * Time.deltaTime, 0);

        Mathf.Clamp(v3Movement.x, 10, 100);
        m_Rigidbody.AddForce(v3Movement);
    }

    // Update is called once per frame
    protected virtual void Update()
    {

    }

    protected virtual void OnCollisionEnter(Collision other)
    {
        print("Points colliding: " + other.contacts.Length);
        print("First point that collided: " + other.contacts[0].point);
    }
}
