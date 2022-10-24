using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static System.TimeZoneInfo;

public class Player : MonoBehaviour
{
    CharacterController CHC;


    GameObject Manager;
    Controls controls;
    manager managerVariables;

    LevelLoader lvlloader;
    GameObject helpCanvas;

    Camera mainCamera;
    MousePosition3D mousePosition3D;

    bool SpaceAvaiable = true;

    float PlayerSpeed;
    float RotationSpeed = 300;
    float JumpCooldown = 0;
    
    public Vector3 JumpVelocity;

    private Animator anim;

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

        lvlloader = GameObject.Find("LevelLoader").GetComponent<LevelLoader>();
        helpCanvas = GameObject.Find("Help");
        helpCanvas.SetActive(false);

        anim = GetComponent<Animator>();
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

        if (Velocity == Vector3.zero) anim.SetBool("isRunning", false);
        else anim.SetBool("isRunning", true);

        if (Velocity != Vector3.zero)
        {
            float angle = Mathf.Atan2(Velocity[0], Velocity[2]) * Mathf.Rad2Deg;
            


            Quaternion toRotation = Quaternion.Euler(new Vector3(0, angle, 0));

            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, RotationSpeed * Time.deltaTime);
        }
        


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
                        JumpVelocity = new Vector3(MoveX, 0, MoveZ) * managerVariables.Player.Speed;
                        //jump
                        StartCoroutine(Dash());
                        anim.SetTrigger("isRolling");
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

        if(Input.GetKey(KeyCode.Mouse0)) anim.SetTrigger("isAttacking");

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
        yield return new WaitForSeconds(.1f);
        float startTime = Time.time;
        while (Time.time < startTime + managerVariables.Player.JumpTime)
        {
            
            
            CHC.Move(JumpVelocity * managerVariables.Player.JumpSpeed * Time.deltaTime);
            
            yield return null;
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.name == "Portal")
        {
            
            
            if (Input.GetKey(KeyCode.E))
            {
                lvlloader.SwitchScene();
            }
        }

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Portal")
        {
            helpCanvas.SetActive(true);


        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == "Portal")
        {
            helpCanvas.SetActive(false);


        }

        
    }

}
