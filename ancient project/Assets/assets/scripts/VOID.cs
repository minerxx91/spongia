using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VOID : MonoBehaviour
{
    manager managerVariables;
    private void Start()
    {
        managerVariables = GameObject.Find("Manager").GetComponent<manager>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.name == "Player")
            managerVariables.toLobby();
    }
}
