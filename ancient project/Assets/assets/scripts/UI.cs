using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

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

    [SerializeField] GameObject BossBar;
    private void Start()
    {
        Manager = GameObject.Find("Manager");
        managerVariables = Manager.GetComponent<manager>();
        controls = managerVariables.GetComponent<Controls>();

        player = GameObject.Find("Player").GetComponent<Player>();






        Help.GetComponent<TextMeshProUGUI>().text = "Zmackni  -" + controls.Interact.ToString() + "-  pre pokracovanie";


       

    }
private void Update()
    {
        float dielikH = CircleInventorySlot / managerVariables.Player.MaxHealth;
        HealthMask.transform.position = new Vector2(HealthMask.transform.position.x, (managerVariables.Player.Health * dielikH)-35);

        float dielikS = CircleInventorySlot / managerVariables.Player.MaxStamina;
        StaminaMask.transform.position = new Vector2(StaminaMask.transform.position.x, (managerVariables.Player.Stamina * dielikS) - 35);

        float dielikJ = BasicInventorySlot / managerVariables.Player.JumpCooldown;
        JumpColor.transform.position = new Vector2(JumpColor.transform.position.x, (player.JumpCooldown * dielikJ)- 12.5f);

        float dielikA = BasicInventorySlot / managerVariables.Player.AttackCooldown;
        AttackColor.transform.position = new Vector2(AttackColor.transform.position.x, (player.AttackCooldown * dielikA) - 12.5f);

        float bossHealth = GameObject.FindGameObjectsWithTag("Boss")[0].GetComponent<EnemyNavMesh>().Health;
        float dielikBoss = 1800 / bossHealth;
        print("bossdielik" + dielikBoss);
        BossBar.transform.localScale = new Vector2(1800/(bossHealth * dielikBoss), BossBar.transform.localScale.y);



    }
}
