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
    private Camera m_Camera;
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
    }

    void FixedUpdate()
    {
        if (Input.GetAxisRaw("Switch Form") != 0 && m_StateTimer == 0)
        {
            m_Human.IsActive = !m_Human.IsActive;
            m_Spirit.IsActive = !m_Spirit.IsActive;

            if(m_Human.IsActive)
                m_Camera.Following = m_Human.gameObject;
            else
                m_Camera.Following = m_Spirit.gameObject;
            //m_Human.gameObject.SetActive(m_Human.IsActive);
            //m_Spirit.gameObject.SetActive(m_Spirit.IsActive);

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
        GameObject[] objects = GameObject.FindGameObjectsWithTag("Platforms");
        for(int i = 0; i > objects.Length; i++)
        {
            if (objects[i].GetComponent<MovingPlat>().IsHuman)
            {
                objects[i].GetComponent<MovingPlat>().IsHuman = false;
                objects[i].GetComponent<MovingPlat>().IsSpirit = true;
                objects[i].SetActive(false);
                SpiritMode.SetActive(true);
            }
            else
            {
                objects[i].GetComponent<MovingPlat>().IsHuman = true;
                objects[i].GetComponent<MovingPlat>().IsSpirit = false;
                objects[i].SetActive(true);
                SpiritMode.SetActive(false);
            }
        }
    }
}