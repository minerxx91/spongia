using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    CharacterController CHC;


    GameObject Manager;
    Controls controls;
    manager managerVariables;

    
    Camera mainCamera;
    MousePosition3D mousePosition3D;

    bool SpaceAvaiable = true;

    float PlayerSpeed;
    float JumpCooldown = 0;
    
    public Vector3 DashVelocity;

    // Start is called before the first frame update
    void Start()
    {
        CHC = GetComponent<CharacterController>();
        mainCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
        mousePosition3D = mainCamera.GetComponent<MousePosition3D>();


        //Manager GameObject
        Manager = GameObject.Find("Manager");
        controls = Manager.GetComponent<Controls>();
        managerVariables = Manager.GetComponent<manager>();
        //---------

        
    }

    // Update is called once per frame
    void Update()
    {
        //cooldowns
        if (JumpCooldown <= managerVariables.Player.JumpCooldown)
            JumpCooldown += Time.deltaTime;
        else
            JumpCooldown = managerVariables.Player.JumpCooldown;


        //---------
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

        Vector3 Velocity = new Vector3(MoveX, 0, MoveZ) * PlayerSpeed;
        //Velocity.Normalize();
        CHC.Move(Velocity);


        
        mousePosition3D.MousePosition.y = transform.position.y;
        transform.LookAt(mousePosition3D.MousePosition);
        // healtzh regen
        if (managerVariables.Player.Health + managerVariables.Player.HealthRegen < managerVariables.Player.MaxHealth)
        {
            managerVariables.Player.Health += managerVariables.Player.HealthRegen * Time.deltaTime;
        }
        else
        {
            managerVariables.Player.Health = managerVariables.Player.MaxHealth;
        }

        if (Input.GetKey(KeyCode.Space))
        {
           if (SpaceAvaiable)
           {
                if (JumpCooldown == managerVariables.Player.JumpCooldown)
                {
                    if (managerVariables.Player.Stamina >= managerVariables.Player.JumpCost)
                    {
                        managerVariables.Player.Stamina -= managerVariables.Player.JumpCost;
                        JumpCooldown = 0;
                        SpaceAvaiable = false;
                        DashVelocity = new Vector3(MoveX, 0, MoveZ) * managerVariables.Player.Speed;
                        //jump
                        StartCoroutine(Dash());

                        if (managerVariables.Player.Stamina > 10)
                        {
                            managerVariables.Player.Stamina -= 10;
                        }
                        else
                        {
                            managerVariables.Player.Stamina = 0;
                        }
                    }
                    
                }
                
           }
            
        }
        else
        {
            SpaceAvaiable = true;
        }

        // stamina regen
        if (managerVariables.Player.Stamina + managerVariables.Player.StaminaRegen < managerVariables.Player.MaxStamina)
        {
            managerVariables.Player.Stamina += managerVariables.Player.StaminaRegen * Time.deltaTime;
        }
        else
        {
            managerVariables.Player.Stamina = managerVariables.Player.MaxStamina;
        }



    }
    IEnumerator Dash()
    {
        float startTime = Time.time;
        while (Time.time < startTime + managerVariables.Player.JumpTime)
        {
            
            
            this.GetComponent<Renderer>().material.color = new Color(1,0.2f,.02f,1);
            CHC.Move(DashVelocity * managerVariables.Player.JumpSpeed * Time.deltaTime);
            
            yield return null;
        }
        this.GetComponent<Renderer>().material.color = new Color(1, 1, 1, 1);
    }
}
