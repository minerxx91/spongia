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
            audioManager.PlayEnemyDamageIncome();
            if (other.gameObject.name == "Poseidon")
            {
                if (this.gameObject.name == "ability1")
                {
                    other.GetComponent<Poseidon>().Stun = true;
                    print("stuning");
                }
                else
                {
                    print("ani rit");
                    if (managerVariables.Poseidon.Health > managerVariables.Player.Damage + managerVariables.Player.DamageIncrease)
                    {
                        managerVariables.Poseidon.Health -= managerVariables.Player.Damage + managerVariables.Player.DamageIncrease;

                    }
                    else
                    {
                        managerVariables.Poseidon.Health = 0;
                        Destroy(other.gameObject);
                    }
                }
                
            }
            else if (other.gameObject.name == "Minotaur")
            {
                if (managerVariables.Minotaur.Health > managerVariables.Player.Damage + managerVariables.Player.DamageIncrease)
                {
                    managerVariables.Minotaur.Health -= managerVariables.Player.Damage + managerVariables.Player.DamageIncrease;

                }
                else
                {
                    managerVariables.Minotaur.Health = 0;
                    Destroy(other.gameObject);
                }
            }
            /*if ( managerVariables.Player.AttackInProcess)
            {
                
                
            }*/

        }
    }
}
