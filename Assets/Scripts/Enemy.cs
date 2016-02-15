using UnityEngine;
using System.Collections;

public class Enemy : Actor {

    [SerializeField]
    protected GameObject Fire;
    [SerializeField]
    protected GameObject Player;
    [SerializeField]
    protected Vector3 ProjectileSpeed;
    protected Object clone1;

    protected override void Start ()
    {
        base.Start();

	}
	
	protected override void Update ()
    {
        base.Update();
        Transform clone1 = (Transform)Instantiate(Fire, new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z), Quaternion.identity);
        clone1.position += (Player.transform.position);
    }
}
