using UnityEngine;
using System.Collections;

public class MovingPlat : Platform
{

    [SerializeField]
    protected string m_Direction;
    [SerializeField]
    protected bool m_Reverse = false;

    // Update is called once per frame
    void FixedUpdate()
    {
        m_Velocity = new Vector3(0, 0, 0);
        switch (m_Direction)
        {
            case "right":
                if (!m_Reverse)
                {
                    MoveForward();
                    if (m_Origin.x + m_Distance.x <= transform.position.x)
                    {
                        m_Reverse = true;
                    }
                }
                if (m_Reverse)
                {
                    MoveBack();
                    if (m_Origin.x >= transform.position.x)
                    {
                        m_Reverse = false;
                    }
                }
                break;

            case "left":
                if (!m_Reverse)
                {
                    MoveBack();
                    if (m_Origin.x - m_Distance.x >= transform.position.x)
                    {
                        m_Reverse = true;
                    }
                }
                if (m_Reverse)
                {
                    MoveForward();
                    if (m_Origin.x <= transform.position.x)
                    {
                        m_Reverse = false;
                    }
                }
                break;

            case "up":
                if (!m_Reverse)
                {
                    MoveUp();
                    if (m_Origin.y + m_Distance.y <= transform.position.y)
                    {
                        m_Reverse = true;
                    }
                }
                if (m_Reverse)
                {
                    MoveDown();
                    if (m_Origin.y >= transform.position.y)
                    {
                        m_Reverse = false;
                    }
                }
                break;

            case "down":
                if (!m_Reverse)
                {
                    MoveDown();
                    if (m_Origin.y - m_Distance.y >= transform.position.y)
                    {
                        m_Reverse = true;
                    }
                }
                if (m_Reverse)
                {
                    MoveUp();
                    if (m_Origin.y <= transform.position.y)
                    {
                        m_Reverse = false;
                    }
                }
                break;
        }
    }
}
