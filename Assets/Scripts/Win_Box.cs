using UnityEngine;
using System.Collections;

public class Win_Box : MonoBehaviour {

    [SerializeField]
    private GameObject ThePlayer;

    [SerializeField]
    private Game_Controller TheGame;

	void Start ()
    {
	
	}
	
	void Update ()
    {

	}

    void OnTriggerEnter(Collider other)
    {
        TheGame.YouWin();
    }
}
