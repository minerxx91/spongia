using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sword : MonoBehaviour
{

    manager managerVariables;
    private void Start()
    {
        managerVariables = GameObject.Find("Manager").GetComponent<manager>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy" || other.gameObject.tag == "Boss")
        {
            if (other.GetComponent<EnemyNavMesh>().Health > managerVariables.Player.Damage + managerVariables.Player.DamageIncrease)
            {
                other.GetComponent<EnemyNavMesh>().Health -= managerVariables.Player.Damage + managerVariables.Player.DamageIncrease;

            }
            else
            {
                other.GetComponent<EnemyNavMesh>().Health = 0;
                Destroy(other.gameObject);
            }
            /*if ( managerVariables.Player.AttackInProcess)
            {
                
                
            }*/
            
        }
    }
}
