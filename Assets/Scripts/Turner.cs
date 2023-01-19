using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turner : MonoBehaviour
{
    float timer = 0.0f;

    int spin=1;

    public bool timerRunning;

    public void Turn(int turn, bool running)
    {
        spin = turn;
        timerRunning = running;
    }

    void Update()
    {
        if (spin % 7 == 0)
        {
            if (timerRunning)
            {
                timer += Time.deltaTime % 0.1f;
                if (timer <= 4f)
                {
                    transform.RotateAround(new Vector3(3.0f, 2.5f, 0.0f), new Vector3(0, 0, 1), 45 * Time.deltaTime);
                }
                else
                {
                    if (spin % 14 == 0)
                    {
                        transform.position = new Vector3(0, 0, 0);
                        transform.localRotation = Quaternion.Euler(-90, -90, 0);
                    }
                    else
                    {
                        transform.position = new Vector3(6, 5, 0);
                        transform.localRotation = Quaternion.Euler(90, -90, 0);
                    }
                    timerRunning = false;
                    timer = 0.0f;
                }
            }
        }
    }
}
