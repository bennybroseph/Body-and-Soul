using UnityEngine;
using System.Collections;

public class MovingPlat : Platform {

    public string Function;
    bool change = false;
    int Wait;

    // Use this for initialization
    void Start ()
    {
        Wait = 0;
	}
	
	// Update is called once per frame
	void Update ()
    {
	switch(Function)
        {
            case "right":
                if (change == false)
                {
                    MoveForward();
                    Wait += 1;
                    if (Wait >= Spaces)
                    {
                        change = true;
                        Wait = 0;
                    }
                }
                if (change == true)
                {
                    MoveBack();
                    Wait += 1;
                    if (Wait >= Spaces)
                    {
                        change = false;
                        Wait = 0;
                    }
                }
                break;

            case "left":
                if (change == false)
                {
                    MoveBack();
                    Wait += 1;
                    if (Wait >= Spaces)
                    {
                        change = true;
                        Wait = 0;
                    }
                }
                if (change == true)
                {
                    MoveForward();
                    Wait += 1;
                    if (Wait >= Spaces)
                    {
                        change = false;
                        Wait = 0;
                    }
                }
                break;

            case "up":
                if (change == false)
                {
                    MoveUp();
                    Wait += 1;
                    if (Wait >= Spaces)
                    {
                        change = true;
                        Wait = 0;
                    }
                }
                if (change == true)
                {
                    MoveDown();
                    Wait += 1;
                    if (Wait >= Spaces)
                    {
                        change = false;
                        Wait = 0;
                    }
                }
                break;

            case "down":
                if (change == false)
                {
                    MoveDown();
                    Wait += 1;
                    if (Wait >= Spaces)
                    {
                        change = true;
                        Wait = 0;
                    }
                }
                if (change == true)
                {
                    MoveUp();
                    Wait += 1;
                    if (Wait >= Spaces)
                    {
                        change = false;
                        Wait = 0;
                    }
                }
                break;
        }
	}
}
