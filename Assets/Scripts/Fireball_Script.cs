using UnityEngine;
using System.Collections;

public class Fireball_Script : MonoBehaviour
{
    protected GameObject Player;
    
    protected float count;

    void Start()
    {
        Player = FindObjectOfType<Human>().gameObject;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.name == "Human_Prefab")
        {
            Player.GetComponent<Human>().HitPoints -= 0.5f;
            
            Destroy(gameObject);
        }
    }

    void Update()
    {
        transform.Rotate(0, 0, 20); 
        count += Time.deltaTime;
        if (count >= 4)
        {
            Destroy(gameObject);
        }
    }
}
