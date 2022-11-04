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

    string[] Texts = new string[] {"Môžeš sa hýbať ",
                                    "Nepriatela si môžeš označiť namierením naňho a kliknutím pravého tlačidtka myši",
                                    "útočiť môžeš pomocou ľavého tlačitka myši",
                                    "útoky nepriateľov môžeš blokovať ľavím shiftom aby si zmenšil poškodenie, ktoré dostaneš","",
                                    "Medzeníkom a pohybom urobíš kotúl a môžeš sa úplne vyhnúť útoku nepriateľa","",
                                    "Dole môžeš vidieť koľko máš života, energie a kedy môžeš znova použiť útok, blokavanie atď... \n\n"+
                                    "Stlač medzeník pre pokračovanie",
                                    "Keď urobíš viacero útokov hneď po sebe vznikne combo, na konci ktorého je kritický zásah, ktorý dáva väčšie poškodenie\n\n"+
                                    "Stlač medzeník pre pokračovanie",
                                    "Dávaj si ale pozor lebo počas útočenia sa nemozeš hýbať\n\n"+
                                    "Stlač medzeník pre pokračovanie"};



    void Start()
    {
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        managerVariables = GameObject.Find("Manager").GetComponent<manager>();
        controls = GameObject.Find("Manager").GetComponent<Controls>();
        Text = GameObject.Find("TutorialText").GetComponent<TextMeshProUGUI>();
        plane = GameObject.Find("TutorialPlane");
        Invoke(nameof(startFreeze), 1.5f);
        Texts[0] = "Môžeš sa hýbať " + controls.MoveUp + controls.MoveLeft + controls.MoveDown + controls.MoveRight;
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
                    print(postup);
                }
            }
            else if (postup == 1)
            {
                if (cube1)
                {
                    showText = true;
                    
                    print(cube1);
                    Text.text = Texts[postup];
                    plane.gameObject.SetActive(true);
                    Time.timeScale = 0;
                }
                if (Input.GetKey(controls.LockTarget))
                {
                    managerVariables.Player.target = GameObject.Find("Enemy1");
                    postup++;
                    showText = false;
                    Time.timeScale = 1;
                    print(postup);
                }
            }
            else if (postup == 2)
            {
                if (showText == false)
                {
                    managerVariables.Player.target = GameObject.Find("Enemy1");
                    if (GameObject.Find("Enemy1").GetComponent<NavMeshAgent>().remainingDistance < 1.7f && GameObject.Find("Enemy1").GetComponent<NavMeshAgent>().remainingDistance != 0)
                    {
                        showText = true;
                        Time.timeScale = 0;
                    }
                }
                else if (Input.GetKey(controls.Attack))
                {
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
                    GameObject.Find("Enemy2").GetComponent<EnemyPleb>().walkPoint = new Vector3(-47, 0.48f, -59);
                    GameObject.Find("Enemy2").GetComponent<EnemyPleb>().DontAttack = true;
                }
            }
            else if (postup == 7 && cube2)
            {
                Time.timeScale = 0;
                showText = true;
                if (Input.GetKeyUp(KeyCode.Space))
                {
                    postup++;
                    //Time.timeScale = 1;
                }
            }

            else if (postup == 8)
            {
                Time.timeScale = 0;
                showText = true;
                if (Input.GetKeyUp(KeyCode.Space))
                {
                    postup++;
                    //Time.timeScale = 1;
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
