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
    string Text1 = "Môeš sa hıbat W A S D";
    string Text2 = "Nepriatela si môeš oznaèi kliknutím pravého tlaèítka na myši";
    string Text3 = "Útoèi môeš kliknutím ¾avého tlaèítka na myši";
    string Text4 = "Útoky nepriate¾ov moeš blokova ¾avım shiftom aby si zmenšil damage, ktorı dostaneš";
    string Text5 = "Medzeníkom urobíš kotúl a môeš sa úplne vyhnú útoku nepriate¾a";
    string Text6 = "Dole vidíš ko¾ko máš ivota, energie a kedy môeš znova poui útok, blokavanie atï...";
    */
    string[] Texts = new string[] {"Môeš sa hıbat ",
                                    "Nepriatela si môeš oznaèi namierením naòho a kliknutím pravého tlaèítka na myši",
                                    "Útoèi môeš kliknutím ¾avého tlaèítka na myši",
                                    "Útoky nepriate¾ov moeš blokova ¾avım shiftom aby si zmenšil damage, ktorı dostaneš","",
                                    "Medzeníkom + Smer pohybu urobíš kotúl a môeš sa úplne vyhnú útoku nepriate¾a","",
                                    "Dole vidíš ko¾ko máš ivota, energie a kedy môeš znova poui útok, blokavanie atï... \n\n"+
                                    "Stlaè medzeník pre pokraèovanie",
                                    "Keï urobíš viacero útokov hneï po sebe vznikne combo, na konci ktorého je kritickı zásah, ktorı dáva ve¾ké poškodenie\n\n"+
                                    "Stlaè medzeník pre pokraèovanie",
                                    "Dávaj si ale pozor lebo poèas útoèenia sa nemozeš hıba\n\n"+
                                    "Stlaè medzeník pre pokraèovanie"};
    
    

    void Start()
    {
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        managerVariables = GameObject.Find("Manager").GetComponent<manager>();
        controls = GameObject.Find("Manager").GetComponent<Controls>();
        Text = GameObject.Find("TutorialText").GetComponent<TextMeshProUGUI>();
        plane = GameObject.Find("TutorialPlane");
        Invoke(nameof(startFreeze), 1.5f);
        Texts[0] = "Môeš sa hıbat " + controls.MoveUp + controls.MoveLeft + controls.MoveDown + controls.MoveRight;
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
