using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using TMPro;
using UnityEngine.UIElements;
using Unity.VisualScripting;
using UnityEngine.UIElements.Experimental;
using EZCameraShake;

public class Hades : MonoBehaviour
{
    public NavMeshAgent agent;
    public float patrolingSpeed;
    public float chasingSpeed;

    public Transform player;

    GameObject Manager;
    manager managerVariables;
    AudioManager audioManager;
    Renderer rend;
    float materialDelay;
    public LayerMask whatIsGround, whatIsPlayer;

    //Patroling
    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;

    //Attacking
    public float timeBetweenAttacks, timebetweenRangedAttacks, shorttimebetweenAttacks;
    float timebetweenRangedAttackstick;
    bool alreadyAttacked;
    bool RangerAttack = false;
    public float RangedAttackTime = 2.5f;


    //States
    float gravityIncrease = 0;
    public float sightRange, MeleeAttackRange, RangerAttackRange;
    public bool playerInSightRange, playerInMeleeAttackRange, playerInRangerAttackRange, playerInRangerAttackRange2;





    //Particles
    ParticleSystem selectAura;
    Light orangeLight;
    [SerializeField] ParticleSystem RangedAbility;
    [SerializeField] GameObject RangedHitbox;

    [SerializeField] GameObject Bident;
    [SerializeField] GameObject projectile; 


    private bool Animating;
    private Animator anim;
    private Vector3 Targetposition;

    public GameObject AttackMelee1;
    //public GameObject AttackMelee2;

    int meleeAnim = 0;
    private bool MidAttackLook = false;
    float abilityChasingTime = 0;
    Vector3 runRotation;
    private bool Charging;
    float randomSoundTime = 5;
    float randomSoundTick = 0;
    public bool Stun = false;

    public Material normalMaterial;
    public Material stunMaterial;
    public GameObject body;


    GameObject MainCamera;
    Shake CameraShake;
    CameraShaker CamShaker;

    void Awake()
    {

        agent = GetComponent<NavMeshAgent>();
        rend = GetComponent<Renderer>();

        anim = GetComponent<Animator>();

        //AttackMelee2.SetActive(false);
    }
    private void Start()
    {
        selectAura = transform.Find("Aura").GetComponent<ParticleSystem>();
        orangeLight = transform.Find("Orange").GetComponent<Light>();
        orangeLight.gameObject.SetActive(false);
        Manager = GameObject.Find("Manager");
        managerVariables = Manager.GetComponent<manager>();
        player = GameObject.Find("Player").transform;
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();


        MainCamera = GameObject.Find("Main Camera").gameObject;
        CameraShake = MainCamera.GetComponent<Shake>();
        CamShaker = CameraShake.GetComponent<CameraShaker>();

        managerVariables.Hades.Health = managerVariables.Hades.maxHealth;

    }

    void DoAttackMelee1()
    {
        AttackMelee1.SetActive(true);
        Invoke(nameof(ResetAttackMelee1), .1f);
        CamShaker.ShakeOnce(2, 2, .1f, 1.3f);
    }

    void ResetAttackMelee1()
    {
        AttackMelee1.SetActive(false);
    }





    void Update()
    {


        if (!Stun)
        {
            randomSoundTick += Time.deltaTime;
            if (randomSoundTick >= randomSoundTime)
            {
                randomSoundTick = 0;
                randomSoundTime = Random.Range(3, 7);
                //audioManager.PlayMinotaurRandom();
                int chrcanie = Random.Range(1, 8);

                //audioManager.PlayMinotaurChrcanie();
            }






            if (abilityChasingTime < 0.5f)
            {
                abilityChasingTime += Time.deltaTime;
                this.gameObject.transform.rotation = Quaternion.Euler(runRotation);

            }



            Targetposition = new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z);
            if (!anim.GetCurrentAnimatorStateInfo(0).IsName("melee2"))
            {
                if (Animating)
                {


                    transform.position += transform.forward * 10;

                }
                Animating = false;
            }

            playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
            playerInMeleeAttackRange = Physics.CheckSphere(transform.position, MeleeAttackRange, whatIsPlayer);
            playerInRangerAttackRange = Physics.CheckSphere(transform.position, RangerAttackRange, whatIsPlayer);
            transform.rotation = Quaternion.Euler(new Vector3(0, transform.rotation.eulerAngles.y, 0));

            if (this.gameObject.name == "Hades")
            {
                if (!playerInSightRange && !playerInMeleeAttackRange) Patroling();
                if (playerInSightRange && !playerInMeleeAttackRange && !GameObject.Find("Player").GetComponent<Player>().died) Chasing();


                if (playerInMeleeAttackRange && !GameObject.Find("Player").GetComponent<Player>().died)
                {
                    MeleeAttacking();
                }

                else if (playerInRangerAttackRange && !GameObject.Find("Player").GetComponent<Player>().died)
                {
                    RangedAttacking();
                }

            }
            else
            {
                if (!playerInSightRange && !playerInMeleeAttackRange) Patroling();
                if (playerInSightRange && !playerInMeleeAttackRange && !GameObject.Find("Player").GetComponent<Player>().died) Chasing();
                if (playerInRangerAttackRange && playerInSightRange && !GameObject.Find("Player").GetComponent<Player>().died) RangedAttacking();
                if (playerInMeleeAttackRange && playerInSightRange && !GameObject.Find("Player").GetComponent<Player>().died) MeleeAttacking();
            }







            materialDelay += Time.deltaTime;


            if (managerVariables.Player.target == this.gameObject)
            {
                selectAura.Play();
                orangeLight.gameObject.SetActive(true);

            }
            else
            {
                selectAura.Stop();
                orangeLight.gameObject.SetActive(false);

            }
            if (!anim.GetCurrentAnimatorStateInfo(0).IsName("walk")) agent.SetDestination(transform.position);

            if (!gameObject.GetComponent<CharacterController>().isGrounded)
            {
                gravityIncrease += managerVariables.GravityForce * Time.deltaTime;

            }
            else
            {
                gravityIncrease = 0;
            }
        }
        else
        {
            Invoke(nameof(ResetStun), 5f);
            agent.SetDestination(transform.position);
            anim.speed = 0;
            body.GetComponent<Renderer>().material = stunMaterial;
        }




    }

    void ResetStun()
    {
        Stun = false;
        anim.speed = 1;
        body.GetComponent<Renderer>().material = normalMaterial;
    }

    private void Patroling()
    {
        anim.SetBool("walk", true);
        if (!walkPointSet) SearchWalkPoint();

        agent.SetDestination(walkPoint);
        agent.speed = patrolingSpeed;

        Vector3 distanceToWalk = transform.position - walkPoint;
        if (distanceToWalk.magnitude < 1f) walkPointSet = false;


    }

    private void SearchWalkPoint()
    {
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);
        if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround)) walkPointSet = true;
    }

    private void Chasing()
    {

        
        transform.LookAt(Targetposition);
        anim.SetBool("walk", true);
        agent.SetDestination(player.position);
        agent.speed = chasingSpeed;





    }

    private void MeleeAttacking()
    {

        transform.LookAt(Targetposition);
        anim.SetBool("walk", false);
        agent.SetDestination(transform.position);

        if (!alreadyAttacked)
        {
            alreadyAttacked = true;
            managerVariables.Hades.DamageIncrease = 0;
            if (meleeAnim == 0)
            {
                anim.SetTrigger("melee1");
                meleeAnim = 1;
                //SwingRight.Play();
                //attack melee 1


               
                Invoke(nameof(ResetAttack), shorttimebetweenAttacks);
            }
            else
            {
                //anim.SetTrigger("melee2");
                meleeAnim = 0;
                //SwingLeft.Play();
                //attack melee 2

                

                Invoke(nameof(ResetAttack), timeBetweenAttacks);
            }



            //Invoke(nameof(MeleeBlastParticel), 1f);
            Invoke(nameof(DoAttackMelee1), .72f);
        }


    }

    void swingParticel()
    {
        //SwingRight.Play();
        MidAttackLook = false;
        Animating = true;
    }



    void EndCharge()
    {
        Charging = false;
        anim.SetBool("charge", false);
    }




    void Charge()
    {
        anim.SetBool("charge", true);
        Charging = true;
        Invoke(nameof(EndCharge), 0.5f);
        //runRotation = transform.rotation.eulerAngles;
        //abilityChasingTime = 0;




    }
    void RangedAttacking()
    {
        print("ranged");
        timebetweenRangedAttackstick += Time.deltaTime;
        if (timebetweenRangedAttackstick >= timebetweenRangedAttacks)
        {
            if (playerInRangerAttackRange)
            {
                transform.LookAt(Targetposition);
                anim.SetBool("walk", false);
                agent.SetDestination(transform.position);
                if (!RangedAbility.isPlaying)
                {
                    RangedAbility.Play();
                }


                print("ano");
                if (timebetweenRangedAttackstick >= timebetweenRangedAttacks + RangedAttackTime)
                {
                    timebetweenRangedAttackstick = 0;
                    RangedAbility.Stop();
                }
            }

        }
    }

    private void ResetAttack()
    {
        alreadyAttacked = false;
    }


    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, MeleeAttackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange);
        Gizmos.color = Color.green;

        Gizmos.DrawWireSphere(transform.position, RangerAttackRange);
    }
}