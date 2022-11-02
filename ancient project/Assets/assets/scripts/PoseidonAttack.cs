using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoseidonAttack : MonoBehaviour
{
    manager managerVariables;
    AudioManager audioManager;
    private void Start()
    {
        managerVariables = GameObject.Find("Manager").GetComponent<manager>();
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();

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
                managerVariables.Player.Health -= (managerVariables.Poseidon.Damage + managerVariables.Poseidon.DamageIncrease)*(100- managerVariables.Player.Resistence) /100;
                if(managerVariables.Player.Resistence > 0)
                {
                    if (managerVariables.Player.ShieldStaminaCost <= managerVariables.Player.Stamina)
                    {
                        audioManager.PlayPlayerShield();
                        Invoke(nameof(ShieldDown), .3f);
                        managerVariables.Player.absorb2 = true;
                        managerVariables.Player.Stamina -= managerVariables.Player.ShieldStaminaCost;
                    }
                       
                }
                else managerVariables.Player.absorb = true;
            }
            else
            {
                managerVariables.Player.absorb = true;
                managerVariables.Player.Health = 0;
            }
            /*if ( managerVariables.Player.AttackInProcess)
            {
                
                
            }*/

        }
    }

    private void ShieldDown()
    {
        GameObject.Find("Player").GetComponent<Player>().ShieldCooldown = 0;
    }
}
