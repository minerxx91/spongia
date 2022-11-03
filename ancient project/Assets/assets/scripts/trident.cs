using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trident : MonoBehaviour
{
    float time =0;
    float velocity = 36;
    public bool grounded = false;
    Vector3 freezeRotation = Vector3.zero;
    manager managerVariables;
    bool gravityFreeze = false;
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
            transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x + 90 - 10, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z );
            transform.localScale = new Vector3(0.05f,0.05f,0.05f);
        }
        
    }

    void Update()
    {
        if (!grounded)
        {
            transform.position += transform.up * Time.deltaTime * velocity;
            time += Time.deltaTime;
            if (time > 5)
            {


                Destroy(gameObject);


            }

        }
        else
        {
            if (!gravityFreeze)
            {
                gameObject.GetComponent<BoxCollider>().isTrigger = false;
                gameObject.GetComponent<Rigidbody>().useGravity = true;
            }
            else{
                gameObject.GetComponent<BoxCollider>().isTrigger = true;

            }



        }




    }
    private void OnTriggerEnter(Collider other)
    {
        
        if(other.gameObject.layer == 8)
        {
            grounded = true;
            this.gameObject.name = "tridentPickUp";
            if (other.gameObject.name == "Poseidon")
            {
                if (managerVariables.Poseidon.Health >= managerVariables.Player.Ability3Damage + managerVariables.Player.DamageIncrease)
                {
                    managerVariables.Poseidon.Health -= managerVariables.Player.Ability3Damage + managerVariables.Player.DamageIncrease;
                }
                else
                {
                    managerVariables.Poseidon.Health = 0;
                }
            }
            if (other.gameObject.name == "Minotaur")
            {
                if (managerVariables.Minotaur.Health >= managerVariables.Player.Ability3Damage + managerVariables.Player.DamageIncrease)
                {
                    managerVariables.Minotaur.Health -= managerVariables.Player.Ability3Damage + managerVariables.Player.DamageIncrease;
                }
                else
                {
                    managerVariables.Minotaur.Health = 0;
                }
            }
        }

    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == 7)
        {
            print("kolizia so zemou");
            gravityFreeze = true;
            gameObject.GetComponent<Rigidbody>().useGravity = false;



        }
    }

}

