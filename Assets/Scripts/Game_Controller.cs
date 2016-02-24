using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class Game_Controller : MonoBehaviour
{
    private bool gameOver;
    private bool restart;
    private bool check;
    private bool LevelComplete;
    [SerializeField]
    private Text m_GameOverText;
    [SerializeField]
    private Text m_RestartText;
    [SerializeField]
    private Player m_Human;
    [SerializeField]
    private Player m_Spirit;
    [SerializeField]
    private MyCamera m_Camera;
    [SerializeField]
    private GameObject SpiritMode;
    [SerializeField]
    private AudioClip m_Song;
    [SerializeField]
    private AudioClip m_GameOverSong;
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
        m_GameOverText.text = "";
        m_RestartText.text = "";
        gameObject.GetComponent<AudioSource>().clip = m_Song;

        m_Human.IsActive = true;
        m_Human.GetComponent<SpriteRenderer>().enabled = true;

        m_Spirit.IsActive = false;
        m_Spirit.GetComponent<SpriteRenderer>().enabled = false;

        List<GameObject> SpiritObjectList = new List<GameObject>();
        SpiritObjectList.AddRange(GameObject.FindGameObjectsWithTag("Spirit Object"));

        List<GameObject> HumanObjectList = new List<GameObject>();
        HumanObjectList.AddRange(GameObject.FindGameObjectsWithTag("Human Object"));

        foreach (GameObject Object in SpiritObjectList)
            Object.GetComponent<SpriteRenderer>().enabled = false;
        foreach (GameObject Object in HumanObjectList)
            Object.GetComponent<SpriteRenderer>().enabled = true;
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
            m_Human.GetComponent<SpriteRenderer>().enabled = !m_Human.GetComponent<SpriteRenderer>().enabled;
            m_Spirit.GetComponent<SpriteRenderer>().enabled = !m_Spirit.GetComponent<SpriteRenderer>().enabled;

            ChangeSceneMode();

            m_StateTimer = 0.5f;
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
            if (m_Human.transform.position.y <= -1.5)
            {
                GameOver();
            }

            if (gameOver)
            {
                m_Human.MovementState = Actor.MovementStates.DEAD;
                m_RestartText.text = "Press 'R' for Restart";
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
        m_GameOverText.text = "Get Shreked!";
        gameObject.GetComponent<AudioSource>().clip = m_GameOverSong;
        gameObject.GetComponent<AudioSource>().Play();
        gameOver = true;
        m_Human.GetComponent<Rigidbody>().freezeRotation = false;
        m_Human.GetComponent<BoxCollider>().enabled = false;

        m_Human.IsActive = false;
        m_Human.Velocity = new Vector3(0.0f, 0.0f, 0.0f);
        m_Human.GetComponent<Rigidbody>().velocity = new Vector3(0.0f, 10.0f, 0.0f);
        m_Human.GetComponent<Rigidbody>().AddTorque(25, 0, 0);
    }

    public IEnumerator YouWin()
    {
        m_GameOverText.text = "You Win!";
        restart = true;
        gameOver = true;
        yield return new WaitForSeconds(3f);
        if (SceneManager.GetActiveScene().name == "AustinScene")
        {
            SceneManager.LoadScene("NewScene");
        }
        else if (SceneManager.GetActiveScene().name == "NewScene")
        {
            SceneManager.LoadScene("Final_Scene");
        }
        else if (SceneManager.GetActiveScene().name == "BenScene")
        {
            SceneManager.LoadScene("Final_Scene");
        }
        else if (SceneManager.GetActiveScene().name == "Final_Scene")
        {
            SceneManager.LoadScene("Endgame");
        }
    }

    public void ChangeSceneMode()
    {
        List<GameObject> ObjectList = new List<GameObject>();
        ObjectList.AddRange(GameObject.FindGameObjectsWithTag("Spirit Object"));
        ObjectList.AddRange(GameObject.FindGameObjectsWithTag("Human Object"));

        foreach (GameObject Object in ObjectList)
            Object.GetComponent<SpriteRenderer>().enabled = !Object.GetComponent<SpriteRenderer>().enabled;

        SpiritMode.SetActive(!SpiritMode.activeInHierarchy);
    }
}