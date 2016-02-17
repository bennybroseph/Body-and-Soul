using UnityEngine;
using System.Collections;

public class Fireball_Script : MonoBehaviour
{
    protected GameObject Player;
    protected Game_Controller TheGame;
    protected float count;

    void Start()
    {
        Player = FindObjectOfType<Human>().gameObject;
        TheGame = FindObjectOfType<Game_Controller>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.name == "Human_Prefab")
        {
            Player.GetComponent<Human>().HitPoints -= 0.5f;
            if (Player.GetComponent<Human>().HitPoints <= 0)
            {
                TheGame.GameOver();
            }
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
