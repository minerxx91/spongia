using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HadesAttack : MonoBehaviour
{
    manager managerVariables;
    AudioManager audioManager;
    private void Start()
    {
        managerVariables = GameObject.Find("Manager").GetComponent<manager>();
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();

    }




    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if( this.gameObject.name == "melee1")
            {
                if (managerVariables.Player.Health > managerVariables.Hades.Damage + managerVariables.Hades.DamageIncrease)
                {
                    managerVariables.Player.Health -= (managerVariables.Hades.Damage + managerVariables.Hades.DamageIncrease) * (100 - managerVariables.Player.Resistence) / 100;
                    if (managerVariables.Player.Resistence > 0)
                    {
                        audioManager.PlayPlayerShield();
                        if (GameObject.Find("Player").GetComponent<PlayerTutorial>() == null) Invoke(nameof(ShieldDown), .3f);
                        else ShieldDownTutorial();
                        if (GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerTutorial>() == null) Invoke(nameof(ShieldDown), .3f);
                        else Invoke(nameof(ShieldDownTutorial), .3f);
                        managerVariables.Player.absorb2 = true;
                    }
                    else managerVariables.Player.absorb = true;
                }
                else
                {
                    //managerVariables.Player.absorb = true;
                    managerVariables.Player.Health = 0;
                    GameObject.Find("Player").GetComponent<Player>().died = true;
                }
            }
            
            /*if ( managerVariables.Player.AttackInProcess)
            {
                
                
            }*/

        }
    }
    private void OnTriggerStay(Collider other)
    {
        if(this.gameObject.name == "Beamhitbox")
        {
            if (other.gameObject.name == "Player")
            {
                if (managerVariables.Player.Resistence > 0)
                {
                    audioManager.PlayPlayerShield();
                    managerVariables.Player.absorb2 = true;

                    if (managerVariables.Player.Health > (10 * Time.deltaTime))
                    {
                        managerVariables.Player.Health -= 10 * Time.deltaTime;
                    }
                    else
                    {
                        managerVariables.Player.Health = 0;
                    }
                }
                else
                {
                     managerVariables.Player.absorb = true;

                }



            }
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
