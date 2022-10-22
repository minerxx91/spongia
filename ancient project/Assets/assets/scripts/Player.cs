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

    [SerializeField]
    Camera mainCamera;
    MousePosition3D mousePosition3D;
  


    float PlayerSpeed;


    // Start is called before the first frame update
    void Start()
    {
        CHC = GetComponent<CharacterController>();
        mousePosition3D = mainCamera.GetComponent<MousePosition3D>();
        

        //Manager GameObject
        controls = Manager.GetComponent<Controls>();
        managerVariables = Manager.GetComponent<manager>();
        //---------

        
    }

    // Update is called once per frame
    void Update()
    {
        float MoveX = 0;
        float MoveZ = 0;
        //Input.GetKeyDown(MoveUp)
        if (Input.GetKey(controls.MoveUp)) 
        {MoveZ++;}
        else if (Input.GetKey(controls.MoveDown))
        {MoveZ--;}
        if (Input.GetKey(controls.MoveRight))
        {MoveX++;}
        else if (Input.GetKey(controls.MoveLeft))
        {MoveX--;}


        PlayerSpeed = managerVariables.Player.Speed * Time.deltaTime;
        print(managerVariables.Player.Health);

        Vector3 Velocity = new Vector3(MoveX, 0, MoveZ) * PlayerSpeed;
        //Velocity.Normalize();
        CHC.Move(Velocity);


        
        mousePosition3D.MousePosition.y = transform.position.y;
        transform.LookAt(mousePosition3D.MousePosition);

        if (managerVariables.Player.Health + managerVariables.Player.HealthRegen < managerVariables.Player.MaxHealth)
        {
            managerVariables.Player.Health += managerVariables.Player.HealthRegen * Time.deltaTime;
        }
        else
        {
            managerVariables.Player.Health = managerVariables.Player.MaxHealth;
        }
        
        
    }
}
