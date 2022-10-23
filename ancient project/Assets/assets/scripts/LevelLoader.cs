using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    public Animator transition;

    public float transitionTime = 1f;
    manager managerVariables;

    public static GameObject instance;

    private void Start()
    {

        managerVariables = GameObject.Find("Manager").GetComponent<manager>();
        
        
        

        
    }

    void Update()
    {

        if (Input.GetKey(KeyCode.E))
        {
            LoadNextLevel();
        }
    }

    public void LoadNextLevel()
    {
        if(SceneManager.GetActiveScene().buildIndex != 0) StartCoroutine(LoadLevel(0));
        else StartCoroutine(LoadLevel(1));
    }

    IEnumerator LoadLevel(int levelIndex)
    {
        transition.SetTrigger("Start");

        yield return new WaitForSeconds(transitionTime);



        managerVariables.SwapScene();







        //SceneManager.LoadScene(levelIndex);


    }
}
