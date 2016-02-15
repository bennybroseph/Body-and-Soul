﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;
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

        m_Human.IsActive = true;
        m_Human.GetComponent<SpriteRenderer>().enabled = true;

        m_Spirit.IsActive = false;
        m_Spirit.GetComponent<SpriteRenderer>().enabled = false;

        GameObject[] platform = GameObject.FindGameObjectsWithTag("Platform");
        for (int i = 0; i < platform.Length; i++)
        {
            if (platform[i].GetComponent<Platform>().IsHuman)
                platform[i].GetComponent<SpriteRenderer>().enabled = true;
            if (platform[i].GetComponent<Platform>().IsSpirit)
                platform[i].GetComponent<SpriteRenderer>().enabled = false;
        }
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
            if (m_Human.transform.position.y <= -10)
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
        m_GameOverText.text = "Game Over!";
        gameOver = true;
        m_Human.GetComponent<Rigidbody>().freezeRotation = false;
        m_Human.GetComponent<Rigidbody>().AddTorque(25, 0, 0);
    }

    public IEnumerator YouWin()
    {
        m_GameOverText.text = "You Win!";
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
        }
        SpiritMode.SetActive(!SpiritMode.active);
    }
}