using UnityEngine;
using System.Collections;

public class MovingPlat : Platform
{

    public string Function;
    [SerializeField]
    bool change = true;

    // Update is called once per frame
    void Update()
    {
        switch (Function)
        {
            case "right":
                if (change == false)
                {
                    MoveForward();
                    if (transform.position.x + m_Distance >= m_Origin.x)
                    {
                        change = true;
                    }
                }
                if (change == true)
                {
                    MoveBack();
                    if (transform.position.x + m_Distance <= m_Origin.x)
                    {
                        change = false;
                    }
                }
                break;

            case "left":
                if (change == false)
                {
                    MoveBack();
                    if (transform.position.x + m_Distance <= m_Origin.x)
                    {
                        change = true;
                    }
                }
                if (change == true)
                {
                    MoveForward();
                    if (transform.position.x + m_Distance >= m_Origin.x)
                    {
                        change = false;
                    }
                }
                break;

            case "up":
                if (change == false)
                {
                    MoveUp();
                    if (transform.position.y + m_Distance >= m_Origin.y)
                    {
                        change = true;
                    }
                }
                if (change == true)
                {
                    MoveDown();
                    if (transform.position.y + m_Distance <= m_Origin.y)
                    {
                        change = false;
                    }
                }
                break;

            case "down":
                if (change == false)
                {
                    MoveDown();
                    if (transform.position.y + m_Distance <= m_Origin.y)
                    {
                        change = true;
                    }
                }
                if (change == true)
                {
                    MoveUp();
                    if (transform.position.y + m_Distance >= m_Origin.y)
                    {
                        change = false;
                    }
                }
                break;
        }
    }
}
