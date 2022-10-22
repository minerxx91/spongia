using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    CharacterController CHC;

    [SerializeField]
    GameObject Manager;

    Controls controls;
    manager managerVariables;
  
    float PlayerSpeed;


    // Start is called before the first frame update
    void Start()
    {
        CHC = GetComponent<CharacterController>();

        //Manager GameObject
        controls = Manager.GetComponent<Controls>();
        managerVariables = Manager.GetComponent<manager>();
        //---------

        PlayerSpeed = managerVariables.Speed / 1000;
    }

    // Update is called once per frame
    void Update()
    {
        float MoveX = 0;
        float MoveZ = 0;
        //Input.GetKeyDown(MoveUp)
        if (Input.GetKey(controls.MoveUp)) 
        {
            MoveZ++;
        }
        else if (Input.GetKey(controls.MoveDown))
        {
            MoveZ--;
        }
        if (Input.GetKey(controls.MoveRight))
        {
            MoveX++;
        }
        else if (Input.GetKey(controls.MoveLeft))
        {
            MoveX--;
        }

        CHC.Move(new Vector3(MoveX,0, MoveZ)*PlayerSpeed);
    }
}
