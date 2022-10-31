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




    private bool captions = true;
    private bool callOnce = false;
    

    
    string[] Lvl2 = { "Asi som ťa podcenil, všetka česť.", "Prečo si sa o to nepostaral sám, si predsa najsilnejší z celého Olympu?",
    "Nemám čas sa zahadzovať s takýmito maličkosťami, na to mám takých hlupáčikov ako ty, ale to ako sa ti podarilo prežiť je naozaj obdivuhodné. Nejakým spôsobom sa ti podarilo použiť medúzinu schopnosť. Musíš byť poloboh s veľkým darom. Poseidon v poslednej dobe začal robiť problémy, mal by si sa o neho v mojom mene postarať kým si to niekto odskáče. Nie je čas navyše, priprav sa. Ó a inak skoro som zabudol, tu máš nejaké vybavenie nech po tebe nezostane len mastný fľak."};
    int[] Lvl2Field = { 1, 0, 1};

    string[] Lvl1 = { };
    string[] Lvl3 = { };
    string[] Lvl4 = { };
    string[] Lvl5 = { };

    int[] Lvl1Field = {};
    int[] Lvl3Field = {};
    int[] Lvl4Field = {};
    int[] Lvl5Field = {};






    int index = 0;








    private void Start()
    {

    }

    private void Update()
    {


        {
            Scene currentScene = SceneManager.GetActiveScene();
            string sceneName = currentScene.name;

            if (sceneName == "Lobby")
            {
                if (Input.GetMouseButtonDown(0))
                    LobbyScenario();
            }

            if (sceneName == "LVL1")
            {
                if (Input.GetMouseButtonDown(0))
                    Lvl1Scenario();
            }
            
            if (sceneName == "LVL2")
            {
                if (Input.GetMouseButtonDown(0))
                    Lvl2Scenario();
            }
            
            if (sceneName == "LVL3" /*&& boss.IsDestroyed()*/)
            {              
                if (Input.GetMouseButtonDown(0))               
                    Lvl3Scenario();             
            }

            if (sceneName == "LVL4")
            {
                if (Input.GetMouseButtonDown(0))
                    Lvl4Scenario();
            }

            if (sceneName == "LVL5")
            {
                if (Input.GetMouseButtonDown(0))
                    Lvl5Scenario();
            }



        }
    }



    private void FixedUpdate()
    {

    }





    private void LobbyScenario()
    {
        if (index == 0)
        {
            lobbyText.text = "Vitaj, dúfame že si našu hru užiješ!";
            index++;
        }           
        else
        {
            lobbyText.text = "";
        }
    }


    private void Lvl1Scenario()
    {
        playerText.text = "";
        othersText.text = "";
        //panel.SetActive(true);
        //image.SetActive(false);

        if (index <= Lvl1.Length - 1)
        {
            if (Lvl1Field[index] == 0)
            {
                playerText.text = Lvl1[index];
                //image.SetActive(true);
                index++;
            }
            else
            {
                othersText.text = Lvl1[index];
                index++;
            }
        }
        else
        {
            Debug.Log("konec");
            othersText.text = "";
        }
    }

    private void Lvl2Scenario()
    {
        playerText.text = "";
        othersText.text = "";
        //panel.SetActive(true);
        //image.SetActive(false);

        if (index <= Lvl2.Length - 1)
        {
            if (Lvl2Field[index] == 0)
            {
                playerText.text = Lvl2[index];
                //image.SetActive(true);
                index++;
            }
            else
            {
                othersText.text = Lvl2[index];
                index++;
            }
        }
        else
        {
            Debug.Log("konec");
            othersText.text = "";
        }
    }

    private void Lvl3Scenario()
    {
        playerText.text = "";
        othersText.text = "";
        //panel.SetActive(true);
        //image.SetActive(false);

        if (index <= Lvl3.Length - 1)
        {
            if (Lvl3Field[index] == 0)
            {
                playerText.text = Lvl3[index];
                //image.SetActive(true);
                index++;
            }
            else
            {
                othersText.text = Lvl3[index];
                index++;
            }
        }
        else
        {
            Debug.Log("konec");
            othersText.text = "";
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
