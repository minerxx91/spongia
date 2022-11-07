using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
using UnityEngine.SceneManagement;


public class PauseManager : MonoBehaviour
{
    [SerializeField] GameObject pauseCanvas;
    public bool paused;
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;
        pauseCanvas.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (paused)
            {
                PauseGame();
            }
            else
            {
                ResumeGame();
            }
        }
    }
    private void PauseGame()
    {

        Time.timeScale = 0;
        paused = false;
        pauseCanvas.SetActive(true);
    }

    public void ResumeGame()
    {

        Time.timeScale = 1;
        paused = true;
        pauseCanvas.SetActive(false);

    }

    public void ToMenu()
    {
        SceneManager.LoadScene("Menu");
        Time.timeScale = 1;
    }
}
