using UnityEngine;
using System.Collections;

public class Final_Boss : MonoBehaviour {

    void OnTriggerEnter(Collider other)
    {
        other.gameObject.GetComponent<Rigidbody>().isKinematic = false;
        other.gameObject.GetComponent<Rigidbody>().useGravity = true;
    }

}
