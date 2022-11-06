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

    PlayerTutorial playerT;
    Player player;
    bool playerTexist = false;

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
    [SerializeField] TextMeshProUGUI BossName; 
    
    private void Start()
    {
        Manager = GameObject.Find("Manager");
        managerVariables = Manager.GetComponent<manager>();
        controls = managerVariables.GetComponent<Controls>();

        

        if (GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerTutorial>() == null)
        {
            player = GameObject.Find("Player").GetComponent<Player>();
        }
        else
        {
            playerT = GameObject.Find("Player").GetComponent<PlayerTutorial>();
            playerTexist = true;
        }




        Help.GetComponent<TextMeshProUGUI>().text = "Zmačkni  „" + controls.Interact.ToString() + "“  pre pokračovanie";


       

    }
private void Update()
    {


        float dielikH = CircleInventorySlot / managerVariables.Player.MaxHealth;
        HealthMask.transform.position = new Vector2(HealthMask.transform.position.x, (managerVariables.Player.Health * dielikH) - 35);

        float dielikS = CircleInventorySlot / managerVariables.Player.MaxStamina;
        StaminaMask.transform.position = new Vector2(StaminaMask.transform.position.x, (managerVariables.Player.Stamina * dielikS) - 35);


        

        if (!playerTexist)
        {
            float dielikJ = BasicInventorySlot / managerVariables.Player.JumpCooldown;
            JumpColor.transform.position = new Vector2(JumpColor.transform.position.x, (player.JumpCooldown * dielikJ) - 12.5f);

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
            float dielikMeduza = BasicInventorySlot / managerVariables.Player.Ability1Cooldown;
            MedusaColor.transform.position = new Vector2(MedusaColor.transform.position.x, (player.Ability1Cooldown * dielikMeduza) - 12.5f);
            if (managerVariables.Player.Ability1Cooldown == player.Ability1Cooldown)
            {
                MedusaColor.GetComponent<Image>().color = new Color32(188, 170, 38, 100);
            }
            else
            {
                MedusaColor.GetComponent<Image>().color = new Color32(158, 150, 18, 100);

            }
            float dielikMinotaur = BasicInventorySlot / managerVariables.Player.Ability2Cooldown;
            MinotaurColor.transform.position = new Vector2(MinotaurColor.transform.position.x, (player.Ability2Cooldown * dielikMinotaur) - 12.5f);
            if (managerVariables.Player.Ability2Cooldown == player.Ability2Cooldown)
            {
                MinotaurColor.GetComponent<Image>().color = new Color32(188, 170, 38, 100);
            }
            else
            {
                MinotaurColor.GetComponent<Image>().color = new Color32(158, 150, 18, 100);

            }
            float dielikPoseidon = BasicInventorySlot / managerVariables.Player.Ability3Cooldown;
            PoseidonColor.transform.position = new Vector2(PoseidonColor.transform.position.x, (player.Ability3Cooldown * dielikPoseidon) - 12.5f);
            if (managerVariables.Player.Ability3Cooldown == player.Ability3Cooldown)
            {
                PoseidonColor.GetComponent<Image>().color = new Color32(188, 170, 38, 100);
            }
            else
            {
                PoseidonColor.GetComponent<Image>().color = new Color32(158, 150, 18, 100);

            }
        }





        else
        {
            float dielikJ = BasicInventorySlot / managerVariables.Player.JumpCooldown;
            JumpColor.transform.position = new Vector2(JumpColor.transform.position.x, (playerT.JumpCooldown * dielikJ) - 12.5f);

            if (managerVariables.Player.JumpCooldown == playerT.JumpCooldown)
            {
                JumpColor.GetComponent<Image>().color = new Color32(255, 180, 0, 100);
            }
            else
            {
                JumpColor.GetComponent<Image>().color = new Color32(178, 128, 45, 100);

            }

            float dielikA = BasicInventorySlot / managerVariables.Player.AttackCooldown;
            AttackColor.transform.position = new Vector2(AttackColor.transform.position.x, (playerT.AttackCooldown * dielikA) - 12.5f);
            if (managerVariables.Player.AttackCooldown == playerT.AttackCooldown)
            {
                AttackColor.GetComponent<Image>().color = new Color32(255, 180, 0, 100);
            }
            else
            {
                AttackColor.GetComponent<Image>().color = new Color32(178, 128, 45, 100);

            }
            float dielikShield = BasicInventorySlot / managerVariables.Player.ShieldCooldown;
            ShieldColor.transform.position = new Vector2(ShieldColor.transform.position.x, (playerT.ShieldCooldown * dielikShield) - 12.5f);
            if (managerVariables.Player.ShieldCooldown == playerT.ShieldCooldown)
            {
                ShieldColor.GetComponent<Image>().color = new Color32(255, 180, 0, 100);
            }
            else
            {
                ShieldColor.GetComponent<Image>().color = new Color32(178, 128, 45, 100);

            }
            float dielikMeduza = BasicInventorySlot / managerVariables.Player.Ability1Cooldown;
            MedusaColor.transform.position = new Vector2(MedusaColor.transform.position.x, (playerT.Ability1Cooldown * dielikMeduza) - 12.5f);
            if (managerVariables.Player.Ability1Cooldown == playerT.Ability1Cooldown)
            {
                MedusaColor.GetComponent<Image>().color = new Color32(188, 170, 38, 100);
            }
            else
            {
                MedusaColor.GetComponent<Image>().color = new Color32(158, 150, 18, 100);

            }

            float dielikMinotaur = BasicInventorySlot / managerVariables.Player.Ability2Cooldown;
            MinotaurColor.transform.position = new Vector2(MinotaurColor.transform.position.x, (playerT.Ability2Cooldown * dielikMinotaur) - 12.5f);
            if (managerVariables.Player.Ability2Cooldown == playerT.Ability2Cooldown)
            {
                MinotaurColor.GetComponent<Image>().color = new Color32(188, 170, 38, 100);
            }
            else
            {
                MinotaurColor.GetComponent<Image>().color = new Color32(158, 150, 18, 100);

            }
            float dielikPoseidon = BasicInventorySlot / managerVariables.Player.Ability3Cooldown;
            PoseidonColor.transform.position = new Vector2(PoseidonColor.transform.position.x, (playerT.Ability3Cooldown * dielikPoseidon) - 12.5f);
            if (managerVariables.Player.Ability3Cooldown == playerT.Ability3Cooldown)
            {
                PoseidonColor.GetComponent<Image>().color = new Color32(188, 170, 38, 100);
            }
            else
            {
                PoseidonColor.GetComponent<Image>().color = new Color32(158, 150, 18, 100);

            }
        }


        







        if(!(this.gameObject.scene.name == "Lobby"))
        {

            BossBar.SetActive(true);
            BossName.gameObject.SetActive(true);

            if (GameObject.FindGameObjectsWithTag("Boss").Length != 0)
            {
                if (GameObject.FindGameObjectsWithTag("Boss")[0].gameObject.name == "Poseidon")
                {
                    float bossHealth = managerVariables.Poseidon.Health;
                    float dielikBoss = 1800 / managerVariables.Poseidon.maxHealth;
                    BossBar.transform.localScale = new Vector2((bossHealth * dielikBoss) / 1800, BossBar.transform.localScale.y);
                    BossName.text = "Poseidon";
                }
                else if (GameObject.FindGameObjectsWithTag("Boss")[0].gameObject.name == "Minotaur")
                {
                    float bossHealth = managerVariables.Minotaur.Health;
                    float dielikBoss = 900 / managerVariables.Minotaur.maxHealth;
                    BossBar.transform.localScale = new Vector2((bossHealth * dielikBoss) / 900, BossBar.transform.localScale.y);
                    BossName.text = "Minotaur";
                }
                else if (GameObject.FindGameObjectsWithTag("Boss")[0].gameObject.name == "Hades")
                {
                    float bossHealth = managerVariables.Hades.Health;
                    float dielikBoss = 900 / managerVariables.Hades.maxHealth;
                    BossBar.transform.localScale = new Vector2((bossHealth * dielikBoss) / 900, BossBar.transform.localScale.y);
                    BossName.text = "Hades";
                }


            }
        }
        else
        {
            BossBar.SetActive(false);
            BossName.gameObject.SetActive(false);
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
