using UnityEngine;
using System.Collections;

public class Win_Box : MonoBehaviour {

    [SerializeField]
    private GameObject ThePlayer;

    [SerializeField]
    private Game_Controller TheGame;

    void OnTriggerEnter(Collider other)
    {
        if (other.name.IndexOf("Human") != -1)
        {
            StartCoroutine(TheGame.YouWin());
        }
    }
}
