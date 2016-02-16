using UnityEngine;
using System.Collections;

public class Enemy : Actor {

    [SerializeField]
    protected GameObject Fire;
    [SerializeField]
    protected GameObject Player;
    [SerializeField]
    protected float WaitForFire;
    protected float count;
    protected bool DoIt;
    [SerializeField]
    protected float Speed;
    protected GameObject TempFireball;

    protected override void Start ()
    {
        base.Start();
        DoIt = false;
	}
	
	protected override void Update ()
    {
        base.Update();
        if (DoIt)
        {
            TempFireball = Instantiate(Fire, gameObject.transform.position, Quaternion.identity) as GameObject;
            float ATang = Mathf.Atan((transform.position.y - Player.transform.position.y) / (transform.position.x - Player.transform.position.x));

            // Correct the angle if we are in quadrant 1
            if (transform.position.x > Player.transform.position.x && transform.position.y > Player.transform.position.y)
                ATang += Mathf.PI;
            // Correct the angle if we are in quadrant 4
            if (transform.position.x > Player.transform.position.x && transform.position.y < Player.transform.position.y)
                ATang -= Mathf.PI;
            
            TempFireball.GetComponent<Rigidbody>().velocity = new Vector3(Speed * Mathf.Cos(ATang), Speed * Mathf.Sin(ATang), 0);
            DoIt = false;
        }
        else
        {
            count += Time.deltaTime;
            if (count >= WaitForFire)
            {
                DoIt = true;
                count = 0;
            }
        }
    }
}
