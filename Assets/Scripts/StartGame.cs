using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Text;

public class StartGame : MonoBehaviour
{
    public GameObject exitScreen;
    public void PlayGame()
    {
        SceneManager.LoadScene(1);
    }
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
}
