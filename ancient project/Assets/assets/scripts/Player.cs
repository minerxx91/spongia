using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
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
    Volume postprocesing;
    ColorParameter vignetterColor;

    LevelLoader lvlloader;
    GameObject helpCanvas;

    Camera mainCamera;
    MousePosition3D mousePosition3D;

    bool SpaceAvaiable = true;
    bool Mouse0Avaiable = true;

    float PlayerSpeed;
    float RotationSpeed = 300;
    public float JumpCooldown = 0;
    public float AttackCooldown = 0;
    public float ShieldCooldown = 0;
    public float Ability1Cooldown = 0;
    public float Ability2Cooldown = 0;
    public float Ability3Cooldown = 0;

    float attackprocess = 0;
    private int combo = 0;
    private float comboTimer = 0;

    [SerializeField] Material telo;
    [SerializeField] Material ZlataZbroj;
    [SerializeField] Material OcelovaZbroj;
    [SerializeField] GameObject sword;
    [SerializeField] GameObject sword2;
    [SerializeField] GameObject shield;
    [SerializeField] GameObject helmet;
    [SerializeField] GameObject helmet2;


    [SerializeField] GameObject trident;
    [SerializeField] GameObject Projectile;


    public Vector3 JumpVelocity;
    Vector3 Velocity;
    private Animator anim;

    GameObject attackHorizontal;
    GameObject attackVertical;
    GameObject ability1;

    ParticleSystem attackParticle;

    Poseidon enemyNavMesh = new Poseidon();

    public bool died = false;
    bool diedAnim = true;

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

        attackHorizontal = GameObject.Find("attackHorizontal");
        attackVertical = GameObject.Find("attackVertical");
        attackHorizontal.SetActive(false);
        attackVertical.SetActive(false);

        ability1 = GameObject.Find("ability1");
        ability1.SetActive(false);


        attackParticle = GameObject.Find("SwordSwing").GetComponent<ParticleSystem>();


        postprocesing = GameObject.Find("Postprocessing").GetComponent<Volume>();


        // reset abilit
        JumpCooldown = managerVariables.Player.JumpCooldown;
        AttackCooldown = managerVariables.Player.AttackCooldown;
        ShieldCooldown = managerVariables.Player.ShieldCooldown;
        Ability1Cooldown = managerVariables.Player.Ability1Cooldown;
        Ability2Cooldown = managerVariables.Player.Ability2Cooldown;
        Ability3Cooldown = managerVariables.Player.Ability3Cooldown;

    }



    void Update()
    {
        if (died)
        {
            anim.SetBool("died", true);
            if (diedAnim)
            {
                diedAnim = false;
                int r = Random.Range(1, 7);
                if(r == 1) anim.SetTrigger("died1");
                else if(r == 2) anim.SetTrigger("died2");
                else if (r == 3) anim.SetTrigger("died3");
                else if (r == 4) anim.SetTrigger("died4");
                else if (r == 5) anim.SetTrigger("died5");
                else if (r == 6) anim.SetTrigger("died6");
                else if (r == 7) anim.SetTrigger("died7");

            }
        }
        else
        {

        
            //cooldowns
            if (JumpCooldown < managerVariables.Player.JumpCooldown)
                JumpCooldown += Time.deltaTime;
            else
                JumpCooldown = managerVariables.Player.JumpCooldown;


            if (ShieldCooldown < managerVariables.Player.ShieldCooldown)
                ShieldCooldown += Time.deltaTime;
            else
                ShieldCooldown = managerVariables.Player.ShieldCooldown;

            if (managerVariables.Player.AttackInProcess)
            {

                attackprocess += Time.deltaTime;
                if (attackprocess >= 0.5f)
                {
                    managerVariables.Player.AttackInProcess = false;
                    attackprocess = 0;
                }
            }

            if (AttackCooldown < managerVariables.Player.AttackCooldown)
                AttackCooldown += Time.deltaTime;
            else
                AttackCooldown = managerVariables.Player.AttackCooldown;



            if (Ability1Cooldown < managerVariables.Player.Ability1Cooldown)
            {
                Ability1Cooldown += Time.deltaTime;
            }
            else Ability1Cooldown = managerVariables.Player.Ability1Cooldown;

            if (Ability2Cooldown < managerVariables.Player.Ability2Cooldown)
            {
                Ability2Cooldown += Time.deltaTime;
            }
            else Ability2Cooldown = managerVariables.Player.Ability2Cooldown;

            if (Ability3Cooldown < managerVariables.Player.Ability3Cooldown)
            {
                Ability3Cooldown += Time.deltaTime;
            }
            else
            {
                Ability3Cooldown = managerVariables.Player.Ability3Cooldown;
                managerVariables.Player.Ability3trident = true;
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
            //print(controls.MoveUp);
            if (!anim.GetCurrentAnimatorStateInfo(0).IsName("Trident") && !anim.GetCurrentAnimatorStateInfo(0).IsName("Attack1") && !anim.GetCurrentAnimatorStateInfo(0).IsName("Attack2") && !anim.GetCurrentAnimatorStateInfo(0).IsName("Attack3"))
            {
                if (Input.GetKey(controls.MoveUp) && Input.GetKey(controls.MoveRight))
                {
                    MoveZ = 0.707f;
                    MoveX = 0.707f;
                }

                else if (Input.GetKey(controls.MoveDown) && Input.GetKey(controls.MoveRight))
                {
                    MoveZ = -0.707f;
                    MoveX = 0.707f;
                }

                else if (Input.GetKey(controls.MoveDown) && Input.GetKey(controls.MoveLeft))
                {
                    MoveZ = -0.707f;
                    MoveX = -0.707f;
                }

                else if (Input.GetKey(controls.MoveUp) && Input.GetKey(controls.MoveLeft))
                {
                    MoveZ = 0.707f;
                    MoveX = -0.707f;
                }
                else
                {
                    MoveZ = 0;
                    MoveX = 0;
                }


                if (Input.GetKey(controls.MoveUp))
                {
                    if (MoveZ == 0)
                        MoveZ++;
                }
                if (Input.GetKey(controls.MoveDown))
                {
                    if (MoveZ == 0 || MoveZ == 1)
                        MoveZ--;
                }
                if (Input.GetKey(controls.MoveRight))
                {
                    if (MoveX == 0)
                        MoveX++;
                }
                if (Input.GetKey(controls.MoveLeft))
                {
                    if (MoveX == 0 || MoveX == 1)
                        MoveX--;
                }


            }


            PlayerSpeed = managerVariables.Player.Speed * Time.deltaTime;

            if (!anim.GetCurrentAnimatorStateInfo(0).IsName("Trident") && !managerVariables.Player.Jumping && !Input.GetKey(controls.Block) || managerVariables.Player.ShieldCooldown != ShieldCooldown)
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
                    audioManager.StopRun();
                }
                else
                {
                    audioManager.PlayRun();
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
                if (managerVariables.Player.target == null)
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

            if (Input.GetKey(controls.Jump) && !(Velocity[0] == 0 && Velocity[2] == 0) && SpaceAvaiable)
            {
                if (JumpCooldown == managerVariables.Player.JumpCooldown)
                {
                    if (managerVariables.Player.Stamina >= managerVariables.Player.JumpCost)
                    {
                        anim.SetBool("rolling", true);
                        animationSelect(" ");
                        anim.SetTrigger("isRolling");
                        StartCoroutine(Dash());
                        managerVariables.Player.Stamina -= managerVariables.Player.JumpCost;
                        JumpCooldown = 0;
                        SpaceAvaiable = false;
                        JumpVelocity = new Vector3(MoveX, 0, MoveZ) * managerVariables.Player.Speed;
                        //jump

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
            else
            {
                SpaceAvaiable = true;
            }

            if (Input.GetKey(controls.Block) && !anim.GetCurrentAnimatorStateInfo(0).IsName("Roll") && combo == 0)
            {
                if (ShieldCooldown >= managerVariables.Player.ShieldCooldown)
                {
                    anim.SetBool("block", true);
                    managerVariables.Player.Resistence = 80;
                }
                else
                {
                    anim.SetBool("block", false);
                    managerVariables.Player.Resistence = 0;
                }

            }
            else
            {
                anim.SetBool("block", false);
                managerVariables.Player.Resistence = 0;
            }

            comboTimer -= Time.deltaTime;
            if (!anim.GetCurrentAnimatorStateInfo(0).IsName("Trident") && Input.GetKey(controls.Attack) && managerVariables.Player.AttackReady && Mouse0Avaiable && !Input.GetKey(controls.Block) && managerVariables.Player.Stamina >= managerVariables.Player.AttackCost)
            {
                managerVariables.Player.Stamina -= managerVariables.Player.AttackCost;
                comboTimer = 1f;
                Mouse0Avaiable = false;
                if (combo == 0)
                {
                    anim.SetTrigger("attack1");
                    anim.SetBool("isRunning", false);
                    animationSelect(" ");
                    combo++;
                }
                else if (combo == 1)
                {
                    anim.SetTrigger("attack2");
                    anim.SetBool("isRunning", false);
                    animationSelect(" ");
                    combo++;

                }
                else if (combo == 2)
                {
                    anim.SetTrigger("attack3");
                    anim.SetBool("isRunning", false);
                    animationSelect(" ");
                    combo++;
                }



                managerVariables.Player.AttackReady = false;

                if (combo == 3)
                {
                    if (managerVariables.Player.AttackCost <= managerVariables.Player.Stamina)
                    {
                        attackParticle.startColor = new Color(1, 0.2f, 0, 1);
                        attackParticle.Play();
                        managerVariables.Player.DamageIncrease = managerVariables.Player.Damage * 2;
                        Invoke(nameof(ResetAttack), managerVariables.Player.AttackCooldown);
                        Invoke(nameof(DoAttackVertical), .55f);
                        attackVertical.SetActive(true);
                        audioManager.PlayPlayerAttackS();
                        combo = 0;
                        managerVariables.Player.Stamina -= managerVariables.Player.AttackCost;
                    }

                }
                else
                {
                    if (managerVariables.Player.AttackCost <= managerVariables.Player.Stamina)
                    {
                        audioManager.PlayPlayerAttack();
                        attackParticle.startColor = new Color(1, 1, 1, 1);
                        attackParticle.Play();
                        managerVariables.Player.DamageIncrease = 0;
                        Invoke(nameof(ResetAttack), managerVariables.Player.BetweenAttackCooldown);
                        Invoke(nameof(DoAttackHorizontal), .55f);
                        managerVariables.Player.Stamina -= managerVariables.Player.AttackCost;
                    }


                }

                managerVariables.Player.AttackInProcess = true;
                AttackCooldown = 0;
            }
            else if (combo != 0 && comboTimer < 0) combo = 0;

            if (!Input.GetKey(controls.Attack)) Mouse0Avaiable = true;
            // stamina regen
            if (managerVariables.Player.Stamina + managerVariables.Player.StaminaRegen < managerVariables.Player.MaxStamina)
            {
                managerVariables.Player.Stamina += managerVariables.Player.StaminaRegen * Time.deltaTime;
            }
            else
            {
                managerVariables.Player.Stamina = managerVariables.Player.MaxStamina;
            }

            if (Input.GetKeyDown(controls.ability1))
            {
                if (Ability1Cooldown == managerVariables.Player.Ability1Cooldown)
                {
                    if (managerVariables.Player.Ability1StaminaCost <= managerVariables.Player.Stamina)
                    {
                        Invoke(nameof(ResetAbility1), .1f);
                        ability1.SetActive(true);
                        Ability1Cooldown = 0;
                        managerVariables.Player.Stamina -= managerVariables.Player.Ability1StaminaCost;
                    }

                }


            }
            if (Input.GetKeyDown(controls.ability2))
            {
                if (Ability2Cooldown == managerVariables.Player.Ability2Cooldown)
                {
                    if (managerVariables.Player.Ability2StaminaCost <= managerVariables.Player.Stamina)
                    {
                        managerVariables.Player.Ability2Raged = true;
                        managerVariables.Player.DamageIncrease *= 2;
                        managerVariables.Player.Resistence = 50;

                        Ability2Cooldown = 0;
                        managerVariables.Player.Stamina -= managerVariables.Player.Ability2StaminaCost;
                    }

                }


            }

            if (managerVariables.Player.Ability2Raged)
            {
                managerVariables.Player.Ability2timeToRageTick += Time.deltaTime;
                telo.color = new Color32(150, 89, 69, 255);
                if (this.gameObject.transform.localScale != managerVariables.Player.Ability2growSize)
                    this.gameObject.transform.localScale += new Vector3(0.01f, 0.01f, 0.01f);

                if (managerVariables.Player.Ability2timeToRageTick > managerVariables.Player.Ability2Duration)
                {
                    managerVariables.Player.DamageIncrease /= 2;
                    managerVariables.Player.Resistence = 0;
                    managerVariables.Player.Ability2timeToRageTick = 0;
                    managerVariables.Player.Ability2Raged = false;
                    telo.color = new Color32(144, 128, 97, 255);

                }
            }
            else
            {
                if (this.gameObject.transform.localScale != managerVariables.Player.Ability2normalSize)
                    this.gameObject.transform.localScale -= new Vector3(0.01f, 0.01f, 0.01f);
                telo.color = new Color32(144, 128, 97, 255);
            }


            if (Input.GetKeyDown(controls.ability3))
            {
                if (Ability3Cooldown == managerVariables.Player.Ability3Cooldown)
                {
                    if (managerVariables.Player.Ability3StaminaCost <= managerVariables.Player.Stamina)
                    {
                        managerVariables.Player.Ability3trident = false;
                        anim.SetTrigger("trident");
                        Invoke(nameof(throwTrident), .5f);


                        Ability3Cooldown = 0;
                        managerVariables.Player.Stamina -= managerVariables.Player.Ability3StaminaCost;
                    }

                }


            }

            /*
            postprocesing.profile.GetComponent<Vignette>().color = new ColorParameter(new Color(1, 0, 0, 1), true);
            */
            if (managerVariables.Player.absorb)
            {
                managerVariables.Player.absorb = false;
                anim.SetTrigger("absorb");
            }
            else if (managerVariables.Player.absorb2)
            {
                managerVariables.Player.absorb2 = false;
                anim.SetTrigger("absorb2");
            }
            //wearing trident
            if (managerVariables.Player.PoseidonUnlocked && managerVariables.Player.Ability3trident)
            {
                trident.SetActive(true);
            }
            else
            {
                trident.SetActive(false);
            }

            if (managerVariables.Player.enlightened)
            {
                helmet2.SetActive(true);
                helmet.SetActive(false);
                sword2.SetActive(true);
                sword.SetActive(false);
                shield.GetComponent<Renderer>().material = ZlataZbroj;
            }
            else
            {
                helmet2.SetActive(false);
                helmet.SetActive(true);
                sword2.SetActive(false);
                sword.SetActive(true);
                shield.GetComponent<Renderer>().material = OcelovaZbroj;
            }


        }
    }

    IEnumerator Dash()
    {
        yield return new WaitForSeconds(.1f);
        anim.SetBool("rolling", false);
        float angle = Mathf.Atan2(JumpVelocity[0], JumpVelocity[2]) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, angle, 0));
        audioManager.PlayPlayerRoll();

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
        for (int i = 1; i < 6; i++)
        {
            if (other.gameObject.name == "Portal" + i)
            {


                if (Input.GetKey(controls.Interact))
                {
                    audioManager.PlayPortalEnter();
                    managerVariables.levelIndex = i;
                    lvlloader.SwitchScene();
                }
            }
        }


    }
    private void OnTriggerEnter(Collider other)
    {
        for (int i = 1; i < 6; i++)
        {
            if (other.gameObject.name == "Portal" + i)
            {
                helpCanvas.SetActive(true);


            }
        }
        if (other.gameObject.name == "tridentPickUp")
        {
            Ability3Cooldown = managerVariables.Player.Ability3Cooldown;
            Destroy(other.gameObject);
        }

    }
    private void OnTriggerExit(Collider other)
    {
        for (int i = 1; i < 6; i++)
        {
            if (other.gameObject.name == "Portal" + i)
            {
                helpCanvas.SetActive(false);


            }
        }


    }

    void throwTrident()
    {
        Instantiate(Projectile, trident.transform.position, Quaternion.Euler(new Vector3(90, transform.rotation.eulerAngles.y, 0))).name = "PlayerTrident";
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

    void DoAttackHorizontal()
    {
        attackHorizontal.SetActive(true);
        Invoke(nameof(ResetAttackHorizontal), .1f);
    }

    void ResetAttackHorizontal()
    {
        attackHorizontal.SetActive(false);
    }

    void DoAttackVertical()
    {
        attackVertical.SetActive(true);
        Invoke(nameof(ResetAttackVertical), .1f);
    }

    void ResetAttackVertical()
    {
        attackVertical.SetActive(false);
    }

    void ResetAbility1()
    {
        ability1.SetActive(false);
    }
}
