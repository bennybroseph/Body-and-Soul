using UnityEngine;
using System.Collections;

public class Enemy : Actor {

    [SerializeField]
    protected GameObject Fire;
    protected Object clone1;
    protected Object clone2;
    protected Object clone3;

    protected override void Start ()
    {
        base.Start();

	}
	
	protected override void Update ()
    {
        base.Update();
        clone1 = Instantiate(Fire, new Vector3(), Quaternion.identity);
        clone2 = Instantiate(Fire, new Vector3(), Quaternion.identity);
        clone3 = Instantiate(Fire, new Vector3(), Quaternion.identity);
    }
}
