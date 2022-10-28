using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class Scenario : MonoBehaviour
{
    [SerializeField]
    public TextMeshProUGUI subtitles;
    [SerializeField]
    GameObject subtitlesCanvas;

    public bool captions = false;



    private void Start()
    {
        
        subtitlesCanvas.SetActive(false);
        Scene currentScene = SceneManager.GetActiveScene();
        string sceneName = currentScene.name;
        if (captions)
        {
            subtitlesCanvas.SetActive(true);
            if (sceneName == "Lobby")
            {
                StartCoroutine(LobbySequence());
            }
            else if (sceneName == "LVL1")
            {
                StartCoroutine(Lvl1Sequence());
            }
        }


    }

    IEnumerator LobbySequence()
    {
        yield return new WaitForSeconds(5);
        subtitles.text = "Vitaj, dúfame že si našu hru užiješ!";
        yield return new WaitForSeconds(5);
        subtitles.text = "";

    }
    IEnumerator Lvl1Sequence()
    {
        yield return new WaitForSeconds(1);
        subtitles.text = "Pravý hrdina, doneste mu jedlo a dobré víno a odpracte tú medúzu odtiaľto.";
        yield return new WaitForSeconds(2);
        subtitles.text = "";
        yield return new WaitForSeconds(2);
        subtitles.text = "To je tvoja práca?";
        yield return new WaitForSeconds(2);
        subtitles.text = "Áno, myslím že áno";
        yield return new WaitForSeconds(2);
        subtitles.text = "Odvaha ti nechýba, ale to neznamená že nie si hlupák, presne niekoho takého potrebujem";
        yield return new WaitForSeconds(2);
        subtitles.text = "Smiem aspoň vedieť kto si a na čo by si ma potreboval než  to odkývam?";
        yield return new WaitForSeconds(2);
        subtitles.text = "Som Zeus, vládca gréckych bohov a teba som si vybral aby si zabil Minotaura, tá obluda už mi ide dlho na nervy, na tvoju odpoveď nie je čas, priprav sa na labyrint a veľa šťastia, budeš ho potrebovať ale veľa času ti nedávam a budeš lízať podlahu.";
        yield return new WaitForSeconds(2);
        subtitles.text = "";


    }




    
}
