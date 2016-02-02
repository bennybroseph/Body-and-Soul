using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Game_Controller : MonoBehaviour
{

    private bool gameOver;
    private bool restart;
    private bool check;
    private bool LevelComplete;
    [SerializeField]
    private GUIText gameOverText;
    [SerializeField]
    private GUIText restartText;
    [SerializeField]
    private GameObject ThePlayer;
    private SceneManager SceneManagement;

    void Start()
    {
        LevelComplete = false;
        gameOver = false;
        restart = false;
        check = true;
        gameOverText.text = "";
        restartText.text = "";
    }

    void Update()
    {
        if (check)
        {
            if (ThePlayer.transform.position.y <= -10)
        {
            GameOver();
        }

            if (gameOver)
        {
            restartText.text = "Press 'R' for Restart";
            restart = true;
            check = false;
        }
        }
        if (restart)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }
    }

    public void GameOver()
    {
        gameOverText.text = "Game Over!";
        gameOver = true;
        ThePlayer.GetComponent<Rigidbody>().freezeRotation = false;
        ThePlayer.GetComponent<Rigidbody>().AddTorque(100, 0, 500);
    }

    public IEnumerator YouWin()
    {
        gameOverText.text = "You Win!";
        restart = true;
        gameOver = true;
        yield return new WaitForSeconds(3f);
        if (SceneManager.GetActiveScene().name != "NewScene")
        {
            SceneManager.LoadScene("NewScene");
        }
        else
        {
            SceneManager.LoadScene("BenScene");
        }   
    }

    public void ChangeSceneMode()
    {
        GameObject[] objects = GameObject.FindGameObjectsWithTag("Platforms");
        for(int i = 0; i > objects.Length; i++)
        {
            //if()
            //{

            //}

            //if()
            //{

            //}
        }
    }
}