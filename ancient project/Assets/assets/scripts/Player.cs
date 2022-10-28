using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using static System.TimeZoneInfo;
using static UnityEngine.GraphicsBuffer;

public class Player : MonoBehaviour
{
    CharacterController CHC;


    GameObject Manager;
    Controls controls;
    manager managerVariables;

    AudioManager audioManager;

    LevelLoader lvlloader;
    GameObject helpCanvas;

    Camera mainCamera;
    MousePosition3D mousePosition3D;

    bool SpaceAvaiable = true;

    float PlayerSpeed;
    float RotationSpeed = 300;
    public float JumpCooldown = 0;
    float attackprocess = 0;
    
    public Vector3 JumpVelocity;
    Vector3 Velocity;
    private Animator anim;

    [SerializeField] ParticleSystem swing;
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

        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();

        lvlloader = GameObject.Find("LevelLoader").GetComponent<LevelLoader>();
        helpCanvas = GameObject.Find("Help");
        helpCanvas.SetActive(false);

        anim = GetComponent<Animator>();
    }

    void animationSelect(string select)
    {
        if (select == "forward")
        {
            anim.SetBool("forward", true);
            anim.SetBool("left", false);
            anim.SetBool("right", false);
            anim.SetBool("back", false);
        }
        else if (select == "back")
        {
            anim.SetBool("forward", false);
            anim.SetBool("left", false);
            anim.SetBool("right", false);
            anim.SetBool("back", true);
        }
        else if (select == "left")
        {
            anim.SetBool("forward", false);
            anim.SetBool("left", true);
            anim.SetBool("right", false);
            anim.SetBool("back", false);
        }
        else if (select == "right")
        {
            anim.SetBool("forward", false);
            anim.SetBool("left", false);
            anim.SetBool("right", true);
            anim.SetBool("back", false);
        }
        else
        {
            anim.SetBool("forward", false);
            anim.SetBool("left", false);
            anim.SetBool("right", false);
            anim.SetBool("back", false);
        }
    }

    void ResetAttack()
    {
        managerVariables.Player.AttackReady = true;
    }
    // Update is called once per frame
    void Update()
    {


        //cooldowns
        if (JumpCooldown < managerVariables.Player.JumpCooldown)
            JumpCooldown += Time.deltaTime;
        else
            JumpCooldown = managerVariables.Player.JumpCooldown;

        if(managerVariables.Player.AttackInProcess)
        {
            
            attackprocess += Time.deltaTime;
            if (attackprocess >= 0.5f)
            {
                managerVariables.Player.AttackInProcess = false;
                attackprocess = 0;
            }
        }
      

        //gravity

        if (!CHC.isGrounded)
        {
            managerVariables.Player.gravityIncrease += managerVariables.GravityForce * Time.deltaTime;

        }
        else
        {
            managerVariables.Player.gravityIncrease = 0;
        }




        //---------
        float MoveX = 0;
        float MoveZ = 0;
        //Input.GetKeyDown(MoveUp)
        if (Input.GetKey(controls.MoveUp))
        { MoveZ++; }
        if (Input.GetKey(controls.MoveDown))
        { MoveZ--; }
        if (Input.GetKey(controls.MoveRight))
        { MoveX++; }
        if (Input.GetKey(controls.MoveLeft))
        { MoveX--; }

        PlayerSpeed = managerVariables.Player.Speed * Time.deltaTime;

        if (!managerVariables.Player.Jumping)
        {
            Velocity = new Vector3(MoveX * PlayerSpeed, -managerVariables.Player.gravityIncrease, MoveZ * PlayerSpeed);
        }
        else
        {
            Velocity = new Vector3(0, -managerVariables.Player.gravityIncrease, 0);

        }
        //Velocity.Normalize();
        CHC.Move(Velocity);
        if (managerVariables.Player.target == null)
        {
            animationSelect(" ");
            if (Velocity[0] == 0 && Velocity[2] == 0)
            {
                anim.SetBool("isRunning", false);
                if (CHC.isGrounded)
                {


                }
            }
            else
            {
                anim.SetBool("isRunning", true);

            }
        }
        else
        {
            anim.SetBool("isRunning", false);
            if (Velocity[0] == 0 && Velocity[2] == 0)
            {
                anim.SetBool("forward", false);
                anim.SetBool("left", false);
                anim.SetBool("right", false);
                anim.SetBool("back", false);
            }
            else
            {
 
                if (gameObject.transform.rotation.eulerAngles.y < 45)
                {
                    if (Velocity[2] < 0) animationSelect("back");
                    else if (Velocity[2] > 0) animationSelect("forward");
                    else if (Velocity[0] > 0) animationSelect("right");
                    else if (Velocity[0] < 0) animationSelect("left");
                    

                }
                else if (gameObject.transform.rotation.eulerAngles.y > 315)
                {
                    if (Velocity[2] < 0) animationSelect("back");
                    else if (Velocity[2] > 0) animationSelect("forward");
                    else if (Velocity[0] > 0) animationSelect("right");
                    else if (Velocity[0] < 0) animationSelect("left");
                    

                }
                else if (gameObject.transform.rotation.eulerAngles.y > 45 && gameObject.transform.rotation.eulerAngles.y < 135)
                {
                    if (Velocity[0] < 0) animationSelect("back");
                    else if (Velocity[0] > 0) animationSelect("forward");
                    else if (Velocity[2] < 0) animationSelect("right");
                    else if (Velocity[2] > 0) animationSelect("left");
                    

                }
                else if (gameObject.transform.rotation.eulerAngles.y > 135 && gameObject.transform.rotation.eulerAngles.y < 225)
                {
                    if (Velocity[2] > 0) animationSelect("back");
                    else if (Velocity[2] < 0) animationSelect("forward");
                    else if (Velocity[0] < 0) animationSelect("right");
                    else if (Velocity[0] > 0) animationSelect("left");
                    

                }
                else if (gameObject.transform.rotation.eulerAngles.y > 225 && gameObject.transform.rotation.eulerAngles.y < 315)
                {
                    if (Velocity[0] > 0) animationSelect("back");
                    else if (Velocity[0] < 0) animationSelect("forward");
                    else if (Velocity[2] > 0) animationSelect("right");
                    else if (Velocity[2] < 0) animationSelect("left");
                    

                }
            }
        }

        if (Velocity[0] != 0 || Velocity[2] != 0)
        {
            if(managerVariables.Player.target == null)
            {
                if (!managerVariables.Player.Jumping)
                {
                    float angle = Mathf.Atan2(Velocity[0], Velocity[2]) * Mathf.Rad2Deg;



                    Quaternion toRotation = Quaternion.Euler(new Vector3(0, angle, 0));

                    transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, RotationSpeed * Time.deltaTime);
                }
            }
            else
            {
                Vector3 targetDirection = managerVariables.Player.target.transform.position - transform.position;
                float singleStep = 6f * Time.deltaTime;

                Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDirection, singleStep, 0.0f);
                transform.rotation = Quaternion.LookRotation(newDirection);
                //transform.LookAt(managerVariables.Player.target.transform);
                transform.rotation = Quaternion.Euler(new Vector3(0, transform.rotation.eulerAngles.y, 0));
            }
            
            
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

        if (Input.GetKey(KeyCode.Space) && !(Velocity[0] == 0 && Velocity[2] == 0) && !anim.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
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
                        anim.SetBool("rolling", true);
                        animationSelect(" ");
                        anim.SetTrigger("isRolling");
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

        if (Input.GetKey(KeyCode.Mouse0) && managerVariables.Player.AttackReady)
        {
            anim.SetTrigger("isAttacking");
            swing.Play();
            managerVariables.Player.AttackReady = false;
            Invoke(nameof(ResetAttack), managerVariables.Player.AttackCooldown);
            managerVariables.Player.AttackInProcess = true;
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
        yield return new WaitForSeconds(.1f);
        anim.SetBool("rolling", false);
        float angle = Mathf.Atan2(JumpVelocity[0], JumpVelocity[2]) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, angle, 0));

        float startTime = Time.time;
        while (Time.time < startTime + managerVariables.Player.JumpTime)
        {
            managerVariables.Player.Jumping = true;            
            CHC.Move(JumpVelocity * managerVariables.Player.JumpSpeed * Time.deltaTime);
            
            yield return null;
        }
        managerVariables.Player.Jumping = false;
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
