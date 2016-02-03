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
    private Player m_Human;
    [SerializeField]
    private Player m_Spirit;
    [SerializeField]
    private MyCamera m_Camera;
    [SerializeField]
    private GameObject SpiritMode;
    private SceneManager SceneManagement;

    public enum PlayerState { HUMAN, SPIRIT };

    private PlayerState m_PlayerState = PlayerState.HUMAN;

    [SerializeField]
    private float m_StateTimer;

    void Start()
    {
        LevelComplete = false;
        gameOver = false;
        restart = false;
        check = true;
        gameOverText.text = "";
        restartText.text = "";
        m_Human.IsActive = true;
        m_Spirit.IsActive = false;
    }

    void FixedUpdate()
    {
        if (Input.GetAxisRaw("Switch Form") != 0 && m_StateTimer == 0)
        {
            m_Human.IsActive = !m_Human.IsActive;
            m_Spirit.IsActive = !m_Spirit.IsActive;

            if (m_Human.IsActive)
            {
                m_PlayerState = PlayerState.HUMAN;
                m_Camera.Following = m_Human.gameObject;
            }
            else
            {
                m_PlayerState = PlayerState.SPIRIT;
                m_Camera.Following = m_Spirit.gameObject;
            }
            m_Human.gameObject.GetComponent<SpriteRenderer>().enabled = !m_Human.gameObject.GetComponent<SpriteRenderer>().enabled;
            m_Spirit.gameObject.GetComponent<SpriteRenderer>().enabled = !m_Spirit.gameObject.GetComponent<SpriteRenderer>().enabled;

            ChangeSceneMode();

            m_StateTimer = 1;
        }
        else if (m_StateTimer > 0)
        {
            m_StateTimer -= Time.deltaTime;
            m_StateTimer = Mathf.Clamp(m_StateTimer, 0.0f, m_StateTimer);
        }
    }
    void Update()
    {
        if (check)
        {
            if (m_Human.transform.position.y <= -10)
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

    public void LateUpdate()
    {
        if (!gameOver)
            transform.position = m_Human.transform.position;
    }

    public void GameOver()
    {
        gameOverText.text = "Game Over!";
        gameOver = true;
        m_Human.GetComponent<Rigidbody>().freezeRotation = false;
        m_Human.GetComponent<Rigidbody>().AddTorque(100, 0, 500);
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
        GameObject[] objects = GameObject.FindGameObjectsWithTag("Platform");
        for (int i = 0; i < objects.Length; i++)
        {
            objects[i].GetComponent<SpriteRenderer>().enabled = !objects[i].GetComponent<SpriteRenderer>().enabled;
            objects[i].GetComponent<BoxCollider>().enabled = !objects[i].GetComponent<BoxCollider>().enabled;
        }
        SpiritMode.SetActive(!SpiritMode.active);
    }
}