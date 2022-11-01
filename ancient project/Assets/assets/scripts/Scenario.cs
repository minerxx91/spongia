using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using Unity.VisualScripting;

public class Scenario : MonoBehaviour
{
    public TextMeshProUGUI playerText;
    public TextMeshProUGUI othersText;
    public TextMeshProUGUI lobbyText;
    [SerializeField]
    GameObject subtitlesCanvas;
    [SerializeField]
    GameObject boss;
    [SerializeField]
    GameObject image;
    [SerializeField]
    GameObject imageOther;
    [SerializeField]
    GameObject panel;
  
    string[] Lvl1 = { "Pravý hrdina, presne niekoho ako ty potrebujem.","???", "Keďže si preukázal silu a odvahu ako málokto, postaráš sa o problém " +
        "ktorý ma trápi už roky.","???", "Zabiješ pre mňa Minotaura, ak to dokážeš získaš bohatstvo na celý život, ak nie zomrieš so cťou. " +
        "Odmietnuť samozrejme nemôžeš, hah." };
    string[] Lvl2 = {"a","b","a","b" };
    string[] Lvl3 = { "Asi som ťa podcenil, všetka česť.", "Prečo si sa o to nepostaral sám, si predsa najsilnejší z celého Olympu?",
        "Nemám čas sa zahadzovať s takýmito maličkosťami, na to mám takých hlupáčikov ako ty, ale to ako sa ti podarilo prežiť je naozaj obdivuhodné. " +
        "Nejakým spôsobom sa ti podarilo použiť medúzinu schopnosť. Musíš byť poloboh s veľkým darom. Poseidon v poslednej dobe začal robiť problémy, " +
        "mal by si sa o neho v mojom mene postarať kým si to niekto odskáče. Nie je čas navyše, priprav sa. Ó a inak skoro som zabudol, " +
        "tu máš nejaké vybavenie nech po tebe nezostane len mastný fľak."};
    string[] Lvl4 = { };
    string[] Lvl5 = { };

    int[] Lvl1Field = {1,0,1,0,1};
    int[] Lvl2Field = {0,1,0,1};
    int[] Lvl3Field = { 1, 0, 1 };
    int[] Lvl4Field = {};
    int[] Lvl5Field = {};

    int index = 0;
    int sceneNumber = 0;

    private void Start()
    {       
        image.SetActive(false);
        imageOther.SetActive(false);
        panel.SetActive(false);        
    }

    private void Update()
    {   
        Scene currentScene = SceneManager.GetActiveScene();
        string sceneName = currentScene.name;

        if (Input.GetMouseButtonDown(0))
        {
            if (sceneName == "Lobby")
            {
                LobbyScenario();
            }

            if (sceneName == "LVL1" && boss.IsDestroyed())
            {
                sceneNumber = 1;
                Lvl1Scenario();
            }

            if (sceneName == "LVL2" && boss.IsDestroyed())
            {
                Lvl2Scenario();
            }

            if (sceneName == "LVL3" && boss.IsDestroyed())
            {
                Lvl3Scenario();
            }

            if (sceneName == "LVL4" && boss.IsDestroyed())
            {
                Lvl4Scenario();
            }

            if (sceneName == "LVL5" && boss.IsDestroyed())
            {
                Lvl5Scenario();
            }
        }       
    }

    private void LobbyScenario()
    {
        if (index == 0)
        {
            panel.SetActive(true);
            lobbyText.text = "Rok 453 pred n. l., na rozkaz kráľa Kréty Androgeosa sa kráľovská armáda vydáva na výpravu za zneškodnením mýtickej " +
                "bytosti známej ako Medúza po sérii útokov po ktorých z napadnutých nezostalo nič viac ako kamenné sochy s vydeseným výrazom v tvári.";
            index++;
        }           
        else
        {
            lobbyText.text = "";
            panel.SetActive(false);
        }
    }

    private void Lvl1Scenario()
    {
        playerText.text = "";
        othersText.text = "";
        panel.SetActive(true);
        image.SetActive(false);
        imageOther.SetActive(false);

        if (index <= Lvl1.Length - 1)
        {
            if (Lvl1Field[index] == 0)
            {
                playerText.text = Lvl1[index];
                image.SetActive(true);
                index++;
            }
            else
            {
                othersText.text = Lvl1[index];
                imageOther.SetActive(true);
                index++;
            }
        }
        else
        {
            Debug.Log("konec");
            panel.SetActive(false);
        }
    }

    private void Lvl2Scenario()
    {
        playerText.text = "";
        othersText.text = "";
        panel.SetActive(true);
        image.SetActive(false);
        imageOther.SetActive(false);

        if (index <= Lvl2.Length - 1)
        {
            if (Lvl2Field[index] == 0)
            {
                playerText.text = Lvl2[index];
                image.SetActive(true);
                index++;
            }
            else
            {
                othersText.text = Lvl2[index];
                imageOther.SetActive(true);
                index++;
            }
        }
        else
        {
            Debug.Log("konec");
            panel.SetActive(false);
        }
    }

    private void Lvl3Scenario()
    {
        playerText.text = "";
        othersText.text = "";
        panel.SetActive(true);
        image.SetActive(false);
        imageOther.SetActive(false);

        if (index <= Lvl3.Length - 1)
        {
            if (Lvl3Field[index] == 0)
            {
                playerText.text = Lvl3[index];
                image.SetActive(true);
                index++;
            }
            else
            {
                othersText.text = Lvl3[index];
                imageOther.SetActive(true);
                index++;
            }
        }
        else
        {
            Debug.Log("konec");
            panel.SetActive(false);
        }
    }

    private void Lvl4Scenario()
    {
        playerText.text = "";
        othersText.text = "";
        //panel.SetActive(true);
        //image.SetActive(false);

        if (index <= Lvl4.Length - 1)
        {
            if (Lvl4Field[index] == 0)
            {
                playerText.text = Lvl4[index];
                //image.SetActive(true);
                index++;
            }
            else
            {
                othersText.text = Lvl4[index];
                index++;
            }
        }
        else
        {
            Debug.Log("konec");
            othersText.text = "";
        }
    }

    private void Lvl5Scenario()
    {
        playerText.text = "";
        othersText.text = "";
        //panel.SetActive(true);
        //image.SetActive(false);

        if (index <= Lvl5.Length - 1)
        {
            if (Lvl5Field[index] == 0)
            {
                playerText.text = Lvl5[index];
                //image.SetActive(true);
                index++;
            }
            else
            {
                othersText.text = Lvl5[index];
                index++;
            }
        }
        else
        {
            Debug.Log("konec");
            othersText.text = "";
        }
    }












    
}
