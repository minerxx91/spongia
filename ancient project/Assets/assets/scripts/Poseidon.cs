using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Poseidon : MonoBehaviour
{
    manager managerVariables;
    private void Start()
    {
        managerVariables = GameObject.Find("Manager").GetComponent<manager>();
    }

    
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (managerVariables.Player.Health > managerVariables.Poseidon.Damage + managerVariables.Poseidon.DamageIncrease)
            {
                managerVariables.Player.Health -= managerVariables.Poseidon.Damage + managerVariables.Poseidon.DamageIncrease;

            }
            else
            {
                managerVariables.Player.Health = 0;
            }
            /*if ( managerVariables.Player.AttackInProcess)
            {
                
                
            }*/

        }
    }
}
