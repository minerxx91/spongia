using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI : MonoBehaviour
{
    GameObject Manager;
    manager managerVariables;


    [SerializeField] GameObject HealthMask;
    [SerializeField] GameObject StaminaMask;
    private void Start()
    {
        Manager = GameObject.Find("Manager");
        managerVariables = Manager.GetComponent<manager>();
    }
    private void Update()
    {
        float dielikH = 165 / managerVariables.Player.MaxHealth;
        HealthMask.transform.position = new Vector2(HealthMask.transform.position.x, (managerVariables.Player.Health * dielikH)-35);

        float dielikS = 165 / managerVariables.Player.MaxStamina;
        StaminaMask.transform.position = new Vector2(StaminaMask.transform.position.x, (managerVariables.Player.Stamina * dielikS) - 35);
    }
}
