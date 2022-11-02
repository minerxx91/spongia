using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour
{
    manager managerVariables;
    TextMeshProUGUI Text;
    GameObject plane;
    Controls controls;
    AudioManager audioManager;

    public int postup = 0;
    public bool showText = true;
    public bool startFreezed = false;

    public bool cube1 = false;
    public bool cube2 = false;
    /*
    string Text1 = "M��e� sa h�bat W A S D";
    string Text2 = "Nepriatela si m��e� ozna�i� kliknut�m prav�ho tla��tka na my�i";
    string Text3 = "�to�i� m��e� kliknut�m �av�ho tla��tka na my�i";
    string Text4 = "�toky nepriate�ov mo�e� blokova� �av�m shiftom aby si zmen�il damage, ktor� dostane�";
    string Text5 = "Medzen�kom urob� kot�l a m��e� sa �plne vyhn�� �toku nepriate�a";
    string Text6 = "Dole vid� ko�ko m� �ivota, energie a kedy m��e� znova pou�i� �tok, blokavanie at�...";
    */
    string[] Texts = new string[] {"M��e� sa h�bat ",
                                    "Nepriatela si m��e� ozna�i� namieren�m na�ho a kliknut�m prav�ho tla��tka na my�i",
                                    "�to�i� m��e� kliknut�m �av�ho tla��tka na my�i",
                                    "�toky nepriate�ov mo�e� blokova� �av�m shiftom aby si zmen�il damage, ktor� dostane�","",
                                    "Medzen�kom + Smer pohybu urob� kot�l a m��e� sa �plne vyhn�� �toku nepriate�a","",
                                    "Dole vid� ko�ko m� �ivota, energie a kedy m��e� znova pou�i� �tok, blokavanie at�... \n\n"+
                                    "Stla� medzen�k pre pokra�ovanie",
                                    "Ke� urob� viacero �tokov hne� po sebe vznikne combo, na konci ktor�ho je kritick� z�sah, ktor� d�va ve�k� po�kodenie\n\n"+
                                    "Stla� medzen�k pre pokra�ovanie",
                                    "D�vaj si ale pozor lebo po�as �to�enia sa nemoze� h�ba�\n\n"+
                                    "Stla� medzen�k pre pokra�ovanie"};
    
    

    void Start()
    {
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        managerVariables = GameObject.Find("Manager").GetComponent<manager>();
        controls = GameObject.Find("Manager").GetComponent<Controls>();
        Text = GameObject.Find("TutorialText").GetComponent<TextMeshProUGUI>();
        plane = GameObject.Find("TutorialPlane");
        Invoke(nameof(startFreeze), 1.5f);
        Texts[0] = "M��e� sa h�bat " + controls.MoveUp + controls.MoveLeft + controls.MoveDown + controls.MoveRight;
    }

    void startFreeze()
    {
        Time.timeScale = 0;
        startFreezed = true;

    }

    void Update()
    {
        if (showText)
        {
            Text.text = Texts[postup];
            plane.gameObject.SetActive(true);
            audioManager.StopRun();
        }
        else 
        { 
            Text.text = "";
            plane.gameObject.SetActive(false);
        }

        if (startFreezed)
        {
            if (postup == 0)
            {
                if (Input.GetKey(controls.MoveUp) || Input.GetKey(controls.MoveDown) || Input.GetKey(controls.MoveLeft) || Input.GetKey(controls.MoveRight))
                {
                    postup++;
                    showText = false;
                    Time.timeScale = 1;
                }
            }
            else if (postup == 1)
            {
                if (cube1)
                {
                    showText = true;
                    Time.timeScale = 0;
                }
                if (Input.GetKey(controls.LockTarget))
                {
                    managerVariables.Player.target = GameObject.Find("Enemy1");
                    postup++;
                    showText = false;
                    Time.timeScale = 1;
                }
            }
            else if (postup == 2)
            {
                if(showText == false)
                {
                    managerVariables.Player.target = GameObject.Find("Enemy1");
                    if (GameObject.Find("Enemy1").GetComponent<NavMeshAgent>().remainingDistance < 1.7f && GameObject.Find("Enemy1").GetComponent<NavMeshAgent>().remainingDistance != 0)
                    {
                        showText = true;
                        Time.timeScale = 0;
                    }
                }
                else if(Input.GetKey(controls.Attack)){
                    postup++;
                    showText = false;
                    Time.timeScale = 1;
                }
                
            }
            else if (postup == 3)
            {
                managerVariables.Player.target = GameObject.Find("Enemy2");
                if (GameObject.Find("Enemy2").GetComponent<NavMeshAgent>().remainingDistance < 1.7f && GameObject.Find("Enemy2").GetComponent<NavMeshAgent>().remainingDistance != 0)
                {
                    showText = true;
                    Time.timeScale = 0;
                }
                if (showText && Input.GetKey(controls.Block))
                {
                    postup++;
                    Invoke(nameof(DelayPostup), 1f);
                    showText = false;
                    Time.timeScale = 1;
                }
            }
            else if (postup == 5)
            {
                if (GameObject.Find("Enemy2").GetComponent<NavMeshAgent>().remainingDistance < 2)
                {
                    managerVariables.Player.target = null;
                    showText = true;
                    Time.timeScale = 0;
                }
                if (showText && Input.GetKey(controls.Jump) && (Input.GetKey(controls.MoveUp) || Input.GetKey(controls.MoveDown) || Input.GetKey(controls.MoveLeft) || Input.GetKey(controls.MoveRight)))
                {
                    postup++;
                    Invoke(nameof(DelayPostup), 1f);
                    showText = false;
                    Time.timeScale = 1;
                }
            }
            else if (postup == 7)
            {
                Time.timeScale = 0;
                showText = true;
                if (Input.GetKeyUp(KeyCode.Space))
                {
                    postup++;
                    Time.timeScale = 1;
                }
            }

            else if (postup == 8)
            {
                Time.timeScale = 0;
                showText = true;
                if (Input.GetKeyUp(KeyCode.Space))
                {
                    postup++;
                    Time.timeScale = 1;
                }
            }

            else if (postup == 9)
            {
                Time.timeScale = 0;
                showText = true;
                if (Input.GetKeyUp(KeyCode.Space))
                {
                    postup++;
                    showText = false;
                    Time.timeScale = 1;
                }
            }
        }
    }

    void DelayPostup()
    {
        postup++;
    }
}
