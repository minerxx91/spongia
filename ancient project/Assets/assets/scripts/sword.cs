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
                if (other.GetComponent<Poseidon>().Health > managerVariables.Player.Damage + managerVariables.Player.DamageIncrease)
                {
                    other.GetComponent<Poseidon>().Health -= managerVariables.Player.Damage + managerVariables.Player.DamageIncrease;

                }
                else
                {
                    other.GetComponent<Poseidon>().Health = 0;
                    Destroy(other.gameObject);
                }
            }
            else if (other.gameObject.name == "Minotaur")
            {
                if (other.GetComponent<Minotaur>().Health > managerVariables.Player.Damage + managerVariables.Player.DamageIncrease)
                {
                    other.GetComponent<Minotaur>().Health -= managerVariables.Player.Damage + managerVariables.Player.DamageIncrease;

                }
                else
                {
                    other.GetComponent<Minotaur>().Health = 0;
                    Destroy(other.gameObject);
                }
            }
            /*if ( managerVariables.Player.AttackInProcess)
            {
                
                
            }*/

        }
    }
}
