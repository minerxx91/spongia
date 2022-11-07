using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class levelUp : MonoBehaviour
{

    bool played = false;
    manager managerVariables;
    private void Start()
    {
        managerVariables = GameObject.Find("Manager").GetComponent<manager>();
    }
    void Update()
    {
        if (managerVariables.ButtonAvaiable == 2)
        {
            this.gameObject.transform.position = GameObject.Find("Player").transform.position;
            this.gameObject.transform.position = new Vector3(transform.position.x,transform.position.y+ 0.5f, transform.position.z);
            if (GameObject.Find("ScenarioCanvas").transform.Find("Panel").gameObject.active == false)
            {
                if (!played)
                {
                    this.gameObject.GetComponent<VisualEffect>().Play();
                    played = true;

                }


            }
        }
    }
}
