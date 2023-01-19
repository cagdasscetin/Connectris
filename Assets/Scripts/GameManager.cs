using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Text;

public class GameManager : MonoBehaviour
{
    public string debugStartMessage;

    public GameObject winCircle;

    public GameObject player1;
    public GameObject player2;

    //public GameObject player1Ghost;
    //public GameObject player2Ghost;

    public GameObject winScreen;
    public Text winText;
    public GameObject exitScreen;

    public int height = 6;
    public int length = 7;

    string[] inputFields = new string[] {"InputField0", "InputField1", "InputField2", "InputField3", "InputField4", "InputField5", "InputField6"};

    public GameObject destroyer;

    public GameObject board;

    GameObject fallingPiece;

    public GameObject[] spawnLoc;

    bool player1Turn = true;

    //bool gameOver = false;

    int[,] boardState; //0 = empty, 1 = player1, 2 = player2

    StringBuilder sb = new StringBuilder();

    //float smooth = 5.0f;
    //float tiltAngle = 180.0f;
    int turn = 0;

    public Turner tr;

    void Start()
    {
        //Debug.Log(debugStartMessage);
        //Debug.LogError("Error");
        //Debug.LogWarning("Warning");

        boardState = new int[length, height];
        InputField.round = 1;
        //player1Ghost.SetActive(false);
        //player2Ghost.SetActive(false);
    }
    /*
    public void HoverColumn(int column)
    {
        if (boardState[column, height - 1] == 0 && (fallingPiece == null || fallingPiece.GetComponent<Rigidbody>().velocity == Vector3.zero) && gameOver == false)
        {
            if (player1Turn)
            {
                player1Ghost.SetActive(true);
                player1Ghost.transform.position = spawnLoc[column].transform.position;
            }
            else
            {
                player2Ghost.SetActive(true);
                player2Ghost.transform.position = spawnLoc[column].transform.position;
            }
        }
    }
    */

    public void SelectColumn(int column)
    {
        if((fallingPiece == null || fallingPiece.GetComponent<Rigidbody>().velocity == Vector3.zero) && ((board.transform.position.x == 0 && board.transform.position.y == 0) || (board.transform.position.x == 6 && board.transform.position.y == 5)))
        {
            //Debug.Log("GameManager Column " + column);
            TakeTurn(column);
        }
    }

    void TakeTurn(int column)
    {
        turn++;
        //InputField.turn = turn;

        if (UpdateBoard(column))
        {
            //player1Ghost.SetActive(false);
            //player2Ghost.SetActive(false);

            if (player1Turn)
            {
                fallingPiece = Instantiate(player1, spawnLoc[column].transform.position, Quaternion.identity);
                fallingPiece.GetComponent<Rigidbody>().velocity = new Vector3 (0, 0.1f, 0);
                fallingPiece.GetComponent<Rigidbody>().AddForce(0, -7.0f, 0, ForceMode.Impulse);
                fallingPiece.transform.parent = board.transform;
                InputField.round++;
                player1Turn = false;

                if (boardState[0, 0] != 0 && boardState[1, 0] != 0 && boardState[2, 0] != 0 && boardState[3, 0] != 0 && boardState[4, 0] != 0 && boardState[5, 0] != 0 && boardState[6, 0] != 0)
                {
                    GameObject des0 = Instantiate(destroyer, new Vector3(0, 0, 0), Quaternion.identity);
                    des0.transform.parent = board.transform;
                    GameObject des1 = Instantiate(destroyer, new Vector3(1, 0, 0), Quaternion.identity);
                    des1.transform.parent = board.transform;
                    GameObject des2 = Instantiate(destroyer, new Vector3(2, 0, 0), Quaternion.identity);
                    des2.transform.parent = board.transform;
                    GameObject des3 = Instantiate(destroyer, new Vector3(3, 0, 0), Quaternion.identity);
                    des3.transform.parent = board.transform;
                    GameObject des4 = Instantiate(destroyer, new Vector3(4, 0, 0), Quaternion.identity);
                    des4.transform.parent = board.transform;
                    GameObject des5 = Instantiate(destroyer, new Vector3(5, 0, 0), Quaternion.identity);
                    des5.transform.parent = board.transform;
                    GameObject des6 = Instantiate(destroyer, new Vector3(6, 0, 0), Quaternion.identity);
                    des6.transform.parent = board.transform;

                    for (int x = 0; x < length; x++)
                    {
                        for (int y = 0; y < height - 1; y++)
                        {
                            boardState[x, y] = boardState[x, y + 1];
                        }
                        boardState[x, 5] = 0;
                    }
                    Debug.LogWarning("Row Deleted!");

                    for (int y = 0; y < height; y++)
                    {
                        for (int x = 0; x < length; x++)
                        {
                            sb.Append(boardState[x, y]);
                            sb.Append(' ');
                        }
                        sb.AppendLine();
                    }
                    Debug.Log(sb.ToString());
                    sb.Clear();

                }

                Seven();
                tr.Turn(turn, true);

                if (DidWin(1))
                {
                    winScreen.SetActive(true);
                    Debug.LogWarning("Player 1 Won!");
                    winText.text = "Player 1 Won!";
                    winText.color = Color.red;
                    foreach (string name in inputFields)
                    {
                        if (GameObject.Find(name))
                            Destroy(GameObject.Find(name));
                    }
                }

                else if (DidWin(2))
                {
                    winScreen.SetActive(true);
                    Debug.LogWarning("Player 2 Won!");
                    winText.text = "Player 2 Won!";
                    winText.color = Color.yellow;
                    foreach (string name in inputFields)
                    {
                        if (GameObject.Find(name))
                            Destroy(GameObject.Find(name));
                    }

                }
            }
            else
            {
                fallingPiece = Instantiate(player2, spawnLoc[column].transform.position, Quaternion.identity);
                fallingPiece.GetComponent<Rigidbody>().velocity = new Vector3(0, 0.1f, 0);
                fallingPiece.GetComponent<Rigidbody>().AddForce(0, -7.0f, 0, ForceMode.Impulse);
                fallingPiece.transform.parent = board.transform;
                InputField.round++;
                player1Turn = true;

                if (boardState[0, 0] != 0 && boardState[1, 0] != 0 && boardState[2, 0] != 0 && boardState[3, 0] != 0 && boardState[4, 0] != 0 && boardState[5, 0] != 0 && boardState[6, 0] != 0)
                {
                    GameObject des0 = Instantiate(destroyer, new Vector3(0, 0, 0), Quaternion.identity);
                    des0.transform.parent = board.transform;
                    GameObject des1 = Instantiate(destroyer, new Vector3(1, 0, 0), Quaternion.identity);
                    des1.transform.parent = board.transform;
                    GameObject des2 = Instantiate(destroyer, new Vector3(2, 0, 0), Quaternion.identity);
                    des2.transform.parent = board.transform;
                    GameObject des3 = Instantiate(destroyer, new Vector3(3, 0, 0), Quaternion.identity);
                    des3.transform.parent = board.transform;
                    GameObject des4 = Instantiate(destroyer, new Vector3(4, 0, 0), Quaternion.identity);
                    des4.transform.parent = board.transform;
                    GameObject des5 = Instantiate(destroyer, new Vector3(5, 0, 0), Quaternion.identity);
                    des5.transform.parent = board.transform;
                    GameObject des6 = Instantiate(destroyer, new Vector3(6, 0, 0), Quaternion.identity);
                    des6.transform.parent = board.transform;

                    for (int x = 0; x < length; x++)
                    {
                        for (int y = 0; y < height - 1; y++)
                        {
                            boardState[x, y] = boardState[x, y + 1];
                        }
                        boardState[x, 5] = 0;
                    }
                    Debug.LogWarning("Row Deleted!");

                    for (int y = 0; y < height; y++)
                    {
                        for (int x = 0; x < length; x++)
                        {
                            sb.Append(boardState[x, y]);
                            sb.Append(' ');
                        }
                        sb.AppendLine();
                    }
                    Debug.Log(sb.ToString());
                    sb.Clear();

                }

                Seven();
                tr.Turn(turn, true);

                if (DidWin(2))
                {
                    winScreen.SetActive(true);
                    Debug.LogWarning("Player 2 Won!");
                    winText.text = "Player 2 Won!";
                    winText.color = Color.yellow;
                    foreach (string name in inputFields)
                    {
                        if (GameObject.Find(name))
                            Destroy(GameObject.Find(name));
                    }
                }
                
                else if (DidWin(1))
                {
                    winScreen.SetActive(true);
                    Debug.LogWarning("Player 1 Won!");
                    winText.text = "Player 1 Won!";
                    winText.color = Color.red;
                    foreach (string name in inputFields)
                    {
                        if (GameObject.Find(name))
                            Destroy(GameObject.Find(name));
                    }
                }
            }

            if (DidDraw())
            {
                winScreen.SetActive(true);
                Debug.LogWarning("Draw!");
                winText.text = "Draw!";
                winText.color= Color.blue;
                foreach (string name in inputFields)
                {
                    if (GameObject.Find(name))
                        Destroy(GameObject.Find(name));
                }
            }
        }
    }

    bool UpdateBoard(int column)
    {
        for (int row = 0; row < height; row++)
        {

            if(boardState[column, row] == 0) //found empty spot
            {
                if (player1Turn)
                {
                    boardState[column, row] = 1;
                }
                else
                {
                    boardState[column, row] = 2;
                }

                
                for (int y = 0; y < height; y++)
                {
                    for (int x = 0; x < length; x++)
                    {
                        sb.Append(boardState[x, y]);
                        sb.Append(' ');
                    }
                    sb.AppendLine();
                }
                Debug.Log(sb.ToString());
                sb.Clear();
                
                return true;
            }
            
        }

        Debug.LogWarning("Column " + column + " is full");
        return false;
    }

    void Seven()
    {
        if (turn % 7 == 0)
        {
            for (int x = 0; x < length; x++)
            {
                for (int y = 0; y < height / 2; y++)
                {
                    (boardState[x, y], boardState[(length - 1) - x, (height - 1) - y]) = (boardState[(length - 1) - x, (height - 1) - y], boardState[x, y]);
                }
            }

            for (int i = 1; i <= 5; i++)
            {
                for (int x = 0; x < length; x++)
                {
                    for (int y = 0; y < height - 1; y++)
                    {
                        if (boardState[x, y] == 0)
                        {
                            (boardState[x, y], boardState[x, y + 1]) = (boardState[x, y + 1], boardState[x, y]);
                        }
                    }
                }
            }

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < length; x++)
                {
                    sb.Append(boardState[x, y]);
                    sb.Append(' ');
                }
                sb.AppendLine();
            }
            Debug.Log(sb.ToString());
            sb.Clear();
        }
    }

    bool DidWin(int playerNum)
    {
        // Horizontal
        for (int x = 0; x < length-3; x++)
        {
            for (int y = 0; y < height; y++)
            {
                if (boardState[x, y] == playerNum && boardState[x + 1, y] == playerNum && boardState[x + 2, y] == playerNum && boardState[x + 3, y] == playerNum)
                {
                    if (turn % 7 == 0)
                    {
                        GameObject circ1 = Instantiate(winCircle, new Vector3((length - 1) - x, (height - 1) - y, 0), Quaternion.identity);
                        circ1.transform.parent = board.transform;
                        GameObject circ2 = Instantiate(winCircle, new Vector3((length - 1) - (x + 1), (height - 1) - y, 0), Quaternion.identity);
                        circ2.transform.parent = board.transform;
                        GameObject circ3 = Instantiate(winCircle, new Vector3((length - 1) - (x + 2), (height - 1) - y, 0), Quaternion.identity);
                        circ3.transform.parent = board.transform;
                        GameObject circ4 = Instantiate(winCircle, new Vector3((length - 1) - (x + 3), (height - 1) - y, 0), Quaternion.identity);
                        circ4.transform.parent = board.transform;
                    }
                    else
                    {
                        GameObject circ1 = Instantiate(winCircle, new Vector3(x, y, 0), Quaternion.identity);
                        circ1.transform.parent = board.transform;
                        GameObject circ2 = Instantiate(winCircle, new Vector3(x + 1, y, 0), Quaternion.identity);
                        circ2.transform.parent = board.transform;
                        GameObject circ3 = Instantiate(winCircle, new Vector3(x + 2, y, 0), Quaternion.identity);
                        circ3.transform.parent = board.transform;
                        GameObject circ4 = Instantiate(winCircle, new Vector3(x + 3, y, 0), Quaternion.identity);
                        circ4.transform.parent = board.transform;
                    }

                    Debug.Log(x);
                    Debug.Log(y);
                    return true;
                }
            }
        }

        // Vertical
        for (int x = 0; x < length; x++)
        {
            for (int y = 0; y < height-3; y++)
            {
                if (boardState[x, y] == playerNum && boardState[x, y + 1] == playerNum && boardState[x, y + 2] == playerNum && boardState[x, y + 3] == playerNum)
                {
                    if (turn % 7 == 0)
                    {
                        GameObject circ1 = Instantiate(winCircle, new Vector3((length - 1) - x, (height - 1) - y, 0), Quaternion.identity);
                        circ1.transform.parent = board.transform;
                        GameObject circ2 = Instantiate(winCircle, new Vector3((length - 1) - x, (height - 1) - (y + 1), 0), Quaternion.identity);
                        circ2.transform.parent = board.transform;
                        GameObject circ3 = Instantiate(winCircle, new Vector3((length - 1) - x, (height - 1) - (y + 2), 0), Quaternion.identity);
                        circ3.transform.parent = board.transform;
                        GameObject circ4 = Instantiate(winCircle, new Vector3((length - 1) - x, (height - 1) - (y + 3), 0), Quaternion.identity);
                        circ4.transform.parent = board.transform;
                    }
                    else
                    {
                        GameObject circ1 = Instantiate(winCircle, new Vector3(x, y, 0), Quaternion.identity);
                        circ1.transform.parent = board.transform;
                        GameObject circ2 = Instantiate(winCircle, new Vector3(x, y + 1, 0), Quaternion.identity);
                        circ2.transform.parent = board.transform;
                        GameObject circ3 = Instantiate(winCircle, new Vector3(x, y + 2, 0), Quaternion.identity);
                        circ3.transform.parent = board.transform;
                        GameObject circ4 = Instantiate(winCircle, new Vector3(x, y + 3, 0), Quaternion.identity);
                        circ4.transform.parent = board.transform;

                    }

                    return true;
                }
            }
        }

        // y = x
        for (int x = 0; x < length-3; x++)
        {
            for (int y = 0; y < height - 3; y++)
            {
                if (boardState[x, y] == playerNum && boardState[x + 1, y + 1] == playerNum && boardState[x + 2, y + 2] == playerNum && boardState[x + 3, y + 3] == playerNum)
                {
                    if (turn % 7 == 0)
                    {
                        GameObject circ1 = Instantiate(winCircle, new Vector3((length - 1) - x, (height - 1) - y, 0), Quaternion.identity);
                        circ1.transform.parent = board.transform;
                        GameObject circ2 = Instantiate(winCircle, new Vector3((length - 1) - (x + 1), (height - 1) - (y + 1), 0), Quaternion.identity);
                        circ2.transform.parent = board.transform;
                        GameObject circ3 = Instantiate(winCircle, new Vector3((length - 1) - (x + 2), (height - 1) - (y + 2), 0), Quaternion.identity);
                        circ3.transform.parent = board.transform;
                        GameObject circ4 = Instantiate(winCircle, new Vector3((length - 1) - (x + 3), (height - 1) - (y + 3), 0), Quaternion.identity);
                        circ4.transform.parent = board.transform;
                    }
                    else
                    {
                        GameObject circ1 = Instantiate(winCircle, new Vector3(x, y, 0), Quaternion.identity);
                        circ1.transform.parent = board.transform;
                        GameObject circ2 = Instantiate(winCircle, new Vector3(x + 1, y + 1, 0), Quaternion.identity);
                        circ2.transform.parent = board.transform;
                        GameObject circ3 = Instantiate(winCircle, new Vector3(x + 2, y + 2, 0), Quaternion.identity);
                        circ3.transform.parent = board.transform;
                        GameObject circ4 = Instantiate(winCircle, new Vector3(x + 3, y + 3, 0), Quaternion.identity);
                        circ4.transform.parent = board.transform;
                    }

                    return true;
                }
            }
        }

        //  y = -x 
        for (int x = 0; x < length-3; x++)
        {
            for (int y = 0; y < height - 3; y++)
            {
                if (boardState[x, y + 3] == playerNum && boardState[x + 1, y + 2] == playerNum && boardState[x + 2, y + 1] == playerNum && boardState[x + 3, y] == playerNum)
                {
                    if (turn % 7 == 0)
                    {
                        GameObject circ1 = Instantiate(winCircle, new Vector3((length - 1) - x, (height - 1) - (y + 3), 0), Quaternion.identity);
                        circ1.transform.parent = board.transform;
                        GameObject circ2 = Instantiate(winCircle, new Vector3((length - 1) - (x + 1), (height - 1) - (y + 2), 0), Quaternion.identity);
                        circ2.transform.parent = board.transform;
                        GameObject circ3 = Instantiate(winCircle, new Vector3((length - 1) - (x + 2), (height - 1) - (y + 1), 0), Quaternion.identity);
                        circ3.transform.parent = board.transform;
                        GameObject circ4 = Instantiate(winCircle, new Vector3((length - 1) - (x + 3), (height - 1) - y, 0), Quaternion.identity);
                        circ4.transform.parent = board.transform;
                    }
                    else
                    {
                        GameObject circ1 = Instantiate(winCircle, new Vector3(x, y + 3, 0), Quaternion.identity);
                        circ1.transform.parent = board.transform;
                        GameObject circ2 = Instantiate(winCircle, new Vector3(x + 1, y + 2, 0), Quaternion.identity);
                        circ2.transform.parent = board.transform;
                        GameObject circ3 = Instantiate(winCircle, new Vector3(x + 2, y + 1, 0), Quaternion.identity);
                        circ3.transform.parent = board.transform;
                        GameObject circ4 = Instantiate(winCircle, new Vector3(x + 3, y, 0), Quaternion.identity);
                        circ4.transform.parent = board.transform;
                    }

                    return true;
                }
            }
        }
        return false;
    }

    bool DidDraw()
    {
        for (int x = 0; x < length; x++)
        {
            if(boardState[x, height - 1] == 0)
            {
                return false;
            }
        }
        return true;
    }

    // 0 0 0 0 0 0 0
    // 0 0 0 0 0 0 0
    // 0 0 0 0 0 0 0
    // 0 0 0 0 0 0 0
    // 0 0 0 0 0 0 0
    // 0 0 0 0 0 0 0

    public void QuitGame()
    {
        exitScreen.SetActive(true);
    }
    public void ExitYes()
    {
        Application.Quit();
        Debug.LogWarning("Quitting Game!");
    }
    public void ExitNo()
    {
        exitScreen.SetActive(false);
    }
    public void RestartGame()
    {
        SceneManager.LoadScene(1);
    }
    public void MainScreen()
    {
        SceneManager.LoadScene(0);
    }
}
