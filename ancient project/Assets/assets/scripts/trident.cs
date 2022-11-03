using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trident : MonoBehaviour
{
    float time =0;
    float velocity = 36;

    manager managerVariables;
    private void Start()
    {
        managerVariables = GameObject.Find("Manager").GetComponent<manager>();


        if(this.gameObject.name == "EnemyTrident")
        {
            transform.LookAt(GameObject.Find("Player").transform.position + new Vector3(0, .5f, 0));
            transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x + 90, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);
        }
        else
        {
            gameObject.transform.rotation =   GameObject.Find("Player").GetComponent<Transform>().rotation ;
            transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x + 90, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);
        }
        
    }

    void Update()
    {
        transform.position += transform.up * Time.deltaTime * velocity;


        time += Time.deltaTime;
        if(time > 5)
        {
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        print(other.name);
        if(other.gameObject.layer == 8)
        {
           
            if (other.gameObject.name == "Poseidon")
            {
                print("daj mu damage");
                if (managerVariables.Poseidon.Health >= managerVariables.Player.Ability3Damage + managerVariables.Player.DamageIncrease)
                {
                    managerVariables.Poseidon.Health -= managerVariables.Player.Ability3Damage + managerVariables.Player.DamageIncrease;
                }
                else
                {
                    managerVariables.Poseidon.Health = 0;
                }
            }
        }
    }
}
