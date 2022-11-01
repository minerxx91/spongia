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
        if (managerVariables.Player.shake)
        {
            managerVariables.Player.shake = false;
            CamShaker.ShakeOnce(managerVariables.Player.shake1, managerVariables.Player.shake2, managerVariables.Player.shake3, managerVariables.Player.shake4);
        }
    }
}
