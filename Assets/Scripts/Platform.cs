using UnityEngine;
using System.Collections;

public class Platform : MonoBehaviour {

    public bool humanPlat;
    public bool spiritPlat;
    public float speed;
    public float Spaces;

    protected void MoveForward()
    {
        transform.Translate(Vector3.right * Time.deltaTime * speed);
    }

    protected void MoveBack()
    {
        transform.Translate(-Vector3.right * Time.deltaTime * speed);
    }

    protected void MoveUp()
    {
        transform.Translate(Vector3.up * Time.deltaTime * speed);
    }

    protected void MoveDown()
    {
        transform.Translate(Vector3.down * Time.deltaTime * speed);
    }

    protected void Snap()
    {

    }
}
