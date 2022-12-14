using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sword : MonoBehaviour
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
        if (other.gameObject.tag == "Enemy" || other.gameObject.tag == "Boss")
        {
            
            if (other.gameObject.name == "Poseidon")
            {
                if (this.gameObject.name == "ability1")
                {
                    other.GetComponent<Poseidon>().Stun = true;
                    audioManager.PlayZkamenenie();
                }
                else
                {
                    if (managerVariables.Poseidon.Health > managerVariables.Player.Damage + managerVariables.Player.DamageIncrease)
                    {
                        managerVariables.Poseidon.Health -= managerVariables.Player.Damage + managerVariables.Player.DamageIncrease;

                    }
                    else
                    {
                        managerVariables.Poseidon.Health = 0;
                    }
                }
                
            }
            else if (other.gameObject.name == "Minotaur")
            {
                if (this.gameObject.name == "ability1")
                {
                    other.GetComponent<Minotaur>().Stun = true;
                    audioManager.PlayZkamenenie();
                    managerVariables.Minotaur.Health = 0;
                }
                else
                {
                    audioManager.PlayMinotaurHit();
                    if (managerVariables.Minotaur.Health > managerVariables.Player.Damage + managerVariables.Player.DamageIncrease)
                    {
                        managerVariables.Minotaur.Health -= managerVariables.Player.Damage + managerVariables.Player.DamageIncrease;

                    }
                    else
                    {
                        managerVariables.Minotaur.Health = 0;
                    }
                }
               
                
            }
            if (other.gameObject.name == "Hades")
            {
                if (this.gameObject.name == "ability1")
                {
                    other.GetComponent<Hades>().Stun = true;
                    audioManager.PlayZkamenenie();
                }
                else
                {
                    if (managerVariables.Hades.Health > managerVariables.Player.Damage + managerVariables.Player.DamageIncrease)
                    {
                        managerVariables.Hades.Health -= managerVariables.Player.Damage + managerVariables.Player.DamageIncrease;

                    }
                    else
                    {
                        managerVariables.Hades.Health = 0;
                    }
                }

            }
            if (other.gameObject.name == "Hades2")
            {
                if (this.gameObject.name == "ability1")
                {
                    other.GetComponent<Hades>().Stun = true;
                    audioManager.PlayZkamenenie();
                }
                else
                {
                    if (managerVariables.Hades2.Health > managerVariables.Player.Damage + managerVariables.Player.DamageIncrease)
                    {
                        managerVariables.Hades2.Health -= managerVariables.Player.Damage + managerVariables.Player.DamageIncrease;

                    }
                    else
                    {
                        managerVariables.Hades2.Health = 0;
                    }
                }

            }
            else if (other.gameObject.tag == "Enemy")
            {
                if (other.gameObject.GetComponent<EnemyPleb>().Health > managerVariables.Player.Damage + managerVariables.Player.DamageIncrease)
                {
                    other.gameObject.GetComponent<EnemyPleb>().Health -= managerVariables.Player.Damage + managerVariables.Player.DamageIncrease;

                }
                else
                {
                    other.gameObject.GetComponent<EnemyPleb>().Health = 0;
                    Destroy(other.gameObject);
                }
            }
        }
    }
}
