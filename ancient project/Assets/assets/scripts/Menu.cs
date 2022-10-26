using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public void startGame()
    {
        SceneManager.LoadScene("Lobby");
    }

    public void options()
    {
        SceneManager.LoadScene("Options");
    }
    public void quit()
    {
        Application.Quit();
    }
}
