using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputField : MonoBehaviour
{
    public int column;

    public GameManager gm;

    Touch touch;

    public GameObject turnScreen;
    public Text turnText;

    /*
    void OnMouseDown()
    {
        //Debug.Log("Column number is " + column);
        gm.SelectColumn(column);
    }
    
    void OnMouseOver()
    {
        gm.HoverColumn(column);
    }
    */

    public static int round=1;

    void Update()
    {
        if (round % 2 == 1)
        {
            //Debug.LogWarning("Player 1's Turn");
            turnText.text = "Player 1's Turn";
            turnText.color = Color.white;
        }
        else
        {
            //Debug.LogWarning("Player 2's Turn");
            turnText.text = "Player 2's Turn";
            turnText.color = Color.white;
        }

        int tc = Input.touchCount;
        if (tc > 0)
        {
            touch = Input.GetTouch(0);
            //Debug.Log("touchCount" + tc);


            if (touch.phase == TouchPhase.Began)
            {
                Vector3 screenPos = new Vector3(touch.position.x, touch.position.y, Camera.main.transform.position.z);
                Vector3 touchPosition = Camera.main.ScreenToWorldPoint(screenPos);

                //Debug.Log(screenPos);
                //Debug.Log(touchPosition);
                //column = (int) ((touchPosition.x - 2.70f) * 10);

                //gm.SelectColumn(column);
                tc = 0;


                RaycastHit tap;
                var ray = Camera.main.ScreenPointToRay(screenPos);
                Physics.Raycast(ray, out tap, Mathf.Infinity);

                if (tap.collider != null)
                {
                    //Debug.Log(tap.point);
                    if (tap.point.x > -0.5f && tap.point.x < 0.5f)
                        gm.SelectColumn(0);

                    if (tap.point.x > 0.5f && tap.point.x < 1.5f)
                        gm.SelectColumn(1);

                    if (tap.point.x > 1.5f && tap.point.x < 2.5f)
                        gm.SelectColumn(2);

                    if (tap.point.x > 2.5f && tap.point.x < 3.5f)
                        gm.SelectColumn(3);

                    if (tap.point.x > 3.5f && tap.point.x < 4.5f)
                        gm.SelectColumn(4);

                    if (tap.point.x > 4.5f && tap.point.x < 5.5f)
                        gm.SelectColumn(5);

                    if (tap.point.x > 5.5f && tap.point.x < 6.5f)
                        gm.SelectColumn(6);

                }
            }
        }
    }
}
