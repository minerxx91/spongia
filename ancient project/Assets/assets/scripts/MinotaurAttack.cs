using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinotaurAttack : MonoBehaviour
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
            if (managerVariables.Player.Health > managerVariables.Minotaur.Damage + managerVariables.Minotaur.DamageIncrease)
            {
                managerVariables.Player.Health -= (managerVariables.Minotaur.Damage + managerVariables.Minotaur.DamageIncrease)*(100- managerVariables.Player.Resistence) /100;
                if (managerVariables.Player.Resistence > 0)
                {
                    audioManager.PlayPlayerShield();
                    if(GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerTutorial>() == null) Invoke(nameof(ShieldDown), .3f);
                    else Invoke(nameof(ShieldDownTutorial), .3f);
                    managerVariables.Player.absorb2 = true;
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

    private void ShieldDownTutorial()
    {
        GameObject.Find("Player").GetComponent<PlayerTutorial>().ShieldCooldown = 0;
    }
}
