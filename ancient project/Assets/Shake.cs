using EZCameraShake;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Shake : MonoBehaviour
{
    GameObject Manager;
    manager managerVariables;

    CameraShaker CamShaker;
    void Start()
    {
        Manager = GameObject.Find("Manager");
        managerVariables = Manager.GetComponent<manager>();
        CamShaker = GetComponent<CameraShaker>();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
