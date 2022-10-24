using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI : MonoBehaviour
{
    GameObject Manager;
    manager managerVariables;

    Player player;

    int BasicInventorySlot = 125;
    int CircleInventorySlot = 165;

    [SerializeField] GameObject HealthMask;
    [SerializeField] GameObject StaminaMask;
    [SerializeField] GameObject JumpColor;
    private void Start()
    {
        Manager = GameObject.Find("Manager");
        managerVariables = Manager.GetComponent<manager>();

        player = GameObject.Find("Player").GetComponent<Player>();
    }
    private void Update()
    {
        float dielikH = CircleInventorySlot / managerVariables.Player.MaxHealth;
        HealthMask.transform.position = new Vector2(HealthMask.transform.position.x, (managerVariables.Player.Health * dielikH)-35);

        float dielikS = CircleInventorySlot / managerVariables.Player.MaxStamina;
        StaminaMask.transform.position = new Vector2(StaminaMask.transform.position.x, (managerVariables.Player.Stamina * dielikS) - 35);

        float dielikJ = BasicInventorySlot / managerVariables.Player.JumpCooldown;
        JumpColor.transform.position = new Vector2(JumpColor.transform.position.x, (player.JumpCooldown * dielikJ)- 12.5f);
    }
}
