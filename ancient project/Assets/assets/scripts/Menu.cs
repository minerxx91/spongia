using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{

    [SerializeField] GameObject menu;
    [SerializeField] GameObject load;
    [SerializeField] GameObject settings;
    [SerializeField] GameObject grafic;
    [SerializeField] GameObject zvuk;
    [SerializeField] GameObject ovladanie;
    private void Start()
    {
        backtoMenu();
    }
    public void startGame()
    {
        SceneManager.LoadScene("Lobby");
    }
    public void loadGame()
    {
        menu.SetActive(false);
        load.SetActive(true);

    }

    public void options()
    {
        menu.SetActive(false);
        settings.SetActive(true);
    }
    public void grafika()
    {
        menu.SetActive(false);
        settings.SetActive(false);
        grafic.SetActive(true);
    }
    public void quit()
    {
        Application.Quit();
    }
    public void backtoMenu()
    {
        load.SetActive(false);
        settings.SetActive(false) ;
        grafic.SetActive(false);
        menu.SetActive(true);
    }
}
