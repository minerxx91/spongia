using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    GameObject Manager;
    manager managerVariables;
    Controls controls;

    Player player;

    int BasicInventorySlot = 125;
    int CircleInventorySlot = 165;
    [SerializeField] GameObject Help;

    [SerializeField] GameObject HealthMask;
    [SerializeField] GameObject StaminaMask;
    [SerializeField] GameObject JumpColor;
    [SerializeField] GameObject AttackColor;
    [SerializeField] GameObject ShieldColor;

    [SerializeField] GameObject MedusaColor;
    [SerializeField] GameObject MedusaQ;
    [SerializeField] GameObject MinotaurColor;
    [SerializeField] GameObject MinotaurQ;
    [SerializeField] GameObject PoseidonColor;
    [SerializeField] GameObject PoseidonQ;
    [SerializeField] GameObject ZeusColor;
    [SerializeField] GameObject ZeusQ;



    [SerializeField] GameObject BossBar;
    
    private void Start()
    {
        Manager = GameObject.Find("Manager");
        managerVariables = Manager.GetComponent<manager>();
        controls = managerVariables.GetComponent<Controls>();

        player = GameObject.Find("Player").GetComponent<Player>();
        





        Help.GetComponent<TextMeshProUGUI>().text = "Zmačkni  „" + controls.Interact.ToString() + "“  pre pokračovanie";


       

    }
private void Update()
    {
        float dielikH = CircleInventorySlot / managerVariables.Player.MaxHealth;
        HealthMask.transform.position = new Vector2(HealthMask.transform.position.x, (managerVariables.Player.Health * dielikH)-35);

        float dielikS = CircleInventorySlot / managerVariables.Player.MaxStamina;
        StaminaMask.transform.position = new Vector2(StaminaMask.transform.position.x, (managerVariables.Player.Stamina * dielikS) - 35);


        float dielikJ = BasicInventorySlot / managerVariables.Player.JumpCooldown;
        JumpColor.transform.position = new Vector2(JumpColor.transform.position.x, (player.JumpCooldown * dielikJ)- 12.5f);

        



        if (managerVariables.Player.JumpCooldown == player.JumpCooldown)
        {
            JumpColor.GetComponent<Image>().color = new Color32(255, 180, 0, 100);
        }
        else
        {
            JumpColor.GetComponent<Image>().color = new Color32(178, 128, 45, 100);

        }

        float dielikA = BasicInventorySlot / managerVariables.Player.AttackCooldown;
        AttackColor.transform.position = new Vector2(AttackColor.transform.position.x, (player.AttackCooldown * dielikA) - 12.5f);
        if (managerVariables.Player.AttackCooldown == player.AttackCooldown)
        {
            AttackColor.GetComponent<Image>().color = new Color32(255, 180, 0, 100);
        }
        else
        {
            AttackColor.GetComponent<Image>().color = new Color32(178, 128, 45, 100);

        }
        float dielikShield = BasicInventorySlot / managerVariables.Player.ShieldCooldown;
        ShieldColor.transform.position = new Vector2(ShieldColor.transform.position.x, (player.ShieldCooldown * dielikShield) - 12.5f);
        if (managerVariables.Player.ShieldCooldown == player.ShieldCooldown)
        {
            ShieldColor.GetComponent<Image>().color = new Color32(255, 180, 0, 100);
        }
        else
        {
            ShieldColor.GetComponent<Image>().color = new Color32(178, 128, 45, 100);

        }


        if (GameObject.FindGameObjectsWithTag("Boss").Length != 0)
        {
            if (GameObject.FindGameObjectsWithTag("Boss")[0].gameObject.name == "Poseidon")
            {
                float bossHealth = GameObject.FindGameObjectsWithTag("Boss")[0].GetComponent<Poseidon>().Health;
                float dielikBoss = 1800 / GameObject.FindGameObjectsWithTag("Boss")[0].GetComponent<Poseidon>().maxHealth;
                BossBar.transform.localScale = new Vector2((bossHealth * dielikBoss) / 1800, BossBar.transform.localScale.y);
            }
            else if (GameObject.FindGameObjectsWithTag("Boss")[0].gameObject.name == "Minotaur")
            {
                float bossHealth = GameObject.FindGameObjectsWithTag("Boss")[0].GetComponent<Minotaur>().Health;
                float dielikBoss = 1800 / GameObject.FindGameObjectsWithTag("Boss")[0].GetComponent<Minotaur>().maxHealth;
                BossBar.transform.localScale = new Vector2((bossHealth * dielikBoss) / 1800, BossBar.transform.localScale.y);
            }



        }
        if(managerVariables.Player.MeduzaUnlocked)
        {
            MedusaColor.GetComponent<Image>().color = new Color32(188, 171, 39, 155);
            MedusaQ.SetActive(false);
        }
        else
        {
            MedusaColor.GetComponent<Image>().color = new Color32(0, 0, 0, 155);
            MedusaQ.SetActive(true);

        }
        if (managerVariables.Player.MinotaurUnlocked)
        {
            MinotaurColor.GetComponent<Image>().color = new Color32(188, 171, 39, 155);
            MinotaurQ.SetActive(false);
        }
        else
        {
            MinotaurColor.GetComponent<Image>().color = new Color32(0, 0, 0, 155);
            MinotaurQ.SetActive(true);

        }
        if (managerVariables.Player.PoseidonUnlocked)
        {
            PoseidonColor.GetComponent<Image>().color = new Color32(188, 171, 39, 155);
            PoseidonQ.SetActive(false);
        }
        else
        {
            PoseidonColor.GetComponent<Image>().color = new Color32(0, 0, 0, 155);
            PoseidonQ.SetActive(true);

        }
        if (managerVariables.Player.ZeusUnlocked)
        {
            ZeusColor.GetComponent<Image>().color = new Color32(188, 171, 39, 155);
            ZeusQ.SetActive(false);
        }
        else
        {
            ZeusColor.GetComponent<Image>().color = new Color32(0, 0, 0, 155);
            ZeusQ.SetActive(true);

        }







    }
 

}
