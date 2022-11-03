using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialCollision : MonoBehaviour
{
    // Start is called before the first frame update
    Tutorial tutorial;
    void Start()
    {
        tutorial = GameObject.Find("Tutorial").GetComponent<Tutorial>();

    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (this.gameObject.name == "cube1")
            {
                tutorial.cube1 = true;
                print("tu c1");
            }
            if (this.gameObject.name == "cube2")
            {
                tutorial.cube2 = true;
                print("tu c2");
            }
        }
    }
}