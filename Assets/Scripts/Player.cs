using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{
    [SerializeField]
    bool IsActive;
    [SerializeField]
    float HitPoints;

    [SerializeField]
    private Rigidbody2D MyRigidbody;

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
            if((fJump = Input.GetAxis("Jump")) > 0)
            {
              //  ShouldJump = false;
            }
       // }

        Vector2 v2Movement = new Vector2(fMoveHorizontal * 1000 * Time.deltaTime, fJump * 8000 * Time.deltaTime);

        Mathf.Clamp(v2Movement.x, 10, 100);
        MyRigidbody.AddForce(v2Movement);
    }

    //// Update is called once per frame
    //protected virtual void Update()
    //{
        
    //}

    // Used to interact with 
}
