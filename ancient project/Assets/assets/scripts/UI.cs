using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI : MonoBehaviour
{
    [SerializeField] GameObject manager;
    manager managerVariables;


    [SerializeField] GameObject HealthMask;
    private void Start()
    {
        managerVariables = manager.GetComponent<manager>();
    }
    private void Update()
    {
        float dielik = 165 / managerVariables.Player.MaxHealth;
        HealthMask.transform.position = new Vector2(HealthMask.transform.position.x, (managerVariables.Player.Health * dielik)-35);
    }
}
