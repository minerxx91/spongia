using EZCameraShake;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;

public class Hades : MonoBehaviour
{
    private Animator anim;
    public NavMeshAgent agent;
    public float patrolingSpeed;
    public float chasingSpeed;

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

    public float sightRange, MeleeAttackRange, RangerAttackRange;
    public bool playerInSightRange, playerInMeleeAttackRange, playerInRangerAttackRange;


    ParticleSystem selectAura;
    Light orangeLight;
    [SerializeField] ParticleSystem RangedAbility;
    [SerializeField] GameObject RangedHitbox;

    [SerializeField] GameObject Bident;


    private bool Animating;
    private Vector3 Targetposition;

    public GameObject AttackMelee1;
    public GameObject AttackMelee2;


    Transform player;

    float randomSoundTime = 5;
    float randomSoundTick = 0;
    public bool Stun = false;

    public Material normalMaterial;
    public Material stunMaterial;
    public GameObject body;


    GameObject MainCamera;
    Shake CameraShake;
    CameraShaker CamShaker;
    bool RangeAnimOnce = false;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        rend = GetComponent<Renderer>();

        anim = GetComponent<Animator>();
    }

    void Start()
    {
        selectAura = transform.Find("Aura").GetComponent<ParticleSystem>();
        orangeLight = transform.Find("Orange").GetComponent<Light>();
        orangeLight.gameObject.SetActive(false);
        Manager = GameObject.Find("Manager");
        managerVariables = Manager.GetComponent<manager>();
        player = GameObject.Find("Player").transform;
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        AttackMelee1.gameObject.SetActive(false);
        AttackMelee2.gameObject.SetActive(false);
        RangedHitbox.gameObject.SetActive(false);

        MainCamera = GameObject.Find("Main Camera").gameObject;
        CameraShake = MainCamera.GetComponent<Shake>();
        CamShaker = CameraShake.GetComponent<CameraShaker>();

        managerVariables.Hades.Health = managerVariables.Hades.maxHealth;
    }

   
    
    

    void Update()
    {
        if (!Stun)
        {
            

            ///////////////////////////////////
            

            randomSoundTick += Time.deltaTime;
            if (randomSoundTick >= randomSoundTime)
            {
                randomSoundTick = 0;
                randomSoundTime = Random.Range(3, 7);
                //audioManager.PlayMinotaurRandom();
                int chrcanie = Random.Range(1, 8);

                //audioManager.PlayMinotaurChrcanie();
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
                if (!playerInSightRange && !playerInMeleeAttackRange) Chasing();
                if (playerInSightRange && !playerInMeleeAttackRange && !GameObject.Find("Player").GetComponent<Player>().died) Chasing();


                if (playerInMeleeAttackRange && !GameObject.Find("Player").GetComponent<Player>().died)
                {
                    MeleeAttacking1();
                }

                else if (playerInRangerAttackRange && !GameObject.Find("Player").GetComponent<Player>().died)
                {
                    RangedAttacking();
                }

            }
            else
            {
                if (!playerInSightRange && !playerInMeleeAttackRange) Chasing();
                if (playerInSightRange && !playerInMeleeAttackRange && !GameObject.Find("Player").GetComponent<Player>().died) Chasing();
                if (playerInRangerAttackRange && playerInSightRange && !GameObject.Find("Player").GetComponent<Player>().died) RangedAttacking();
                if (playerInMeleeAttackRange && playerInSightRange && !GameObject.Find("Player").GetComponent<Player>().died) {
                    int r = Random.Range(0, 2);
                    if(r == 1) MeleeAttacking1();
                    else MeleeAttacking2();

                }
            }

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
    private void MeleeAttacking1()
    {

        transform.LookAt(Targetposition);
        anim.SetBool("walk", false);
        agent.SetDestination(transform.position);

        if (!alreadyAttacked)
        {
            alreadyAttacked = true;
            managerVariables.Hades.DamageIncrease = 0;

            {
                anim.SetTrigger("attack1");
                //SwingRight.Play();
                //attack melee 1

                Invoke(nameof(ResetAttack), timeBetweenAttacks);
                //Invoke(nameof(MeleeBlastParticel), 1f);
                Invoke(nameof(DoAttackMelee1), .72f);
            }
        }
    }

    private void MeleeAttacking2()
    {

        transform.LookAt(Targetposition);
        anim.SetBool("walk", false);
        agent.SetDestination(transform.position);

        if (!alreadyAttacked)
        {
            alreadyAttacked = true;
            managerVariables.Hades.DamageIncrease = 0;

            {
                anim.SetTrigger("attack3");
                //SwingRight.Play();
                //attack melee 1

                Invoke(nameof(ResetAttack), timeBetweenAttacks);
                //Invoke(nameof(MeleeBlastParticel), 1f);
                Invoke(nameof(DoAttackMelee2), .72f);
            }
        }
    }

    void RangedAttacking()
    {

        timebetweenRangedAttackstick += Time.deltaTime;
        if (timebetweenRangedAttackstick >= timebetweenRangedAttacks)
        {
            if (playerInRangerAttackRange)
            {
                if (!RangeAnimOnce)
                {
                    anim.SetTrigger("range");
                    RangeAnimOnce = true;
                }
                transform.LookAt(Targetposition);
                agent.SetDestination(transform.position);
                if (!RangedAbility.isPlaying)
                {
                    RangedAbility.Play();
                }
                agent.SetDestination(transform.position);
                anim.SetBool("walk", false);


               if (!anim.GetCurrentAnimatorStateInfo(0).IsName("Range"))
                {
                    anim.SetBool("walk", true);
                }
                
                
                if (timebetweenRangedAttackstick >= timebetweenRangedAttacks + RangedAttackTime)
                {
                    timebetweenRangedAttackstick = 0;
                    RangedAbility.Stop();
                    RangeAnimOnce = false;
                }
            }

        }
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

    void DoAttackMelee2()
    {
        AttackMelee2.SetActive(true);
        Invoke(nameof(ResetAttackMelee2), .1f);
        CamShaker.ShakeOnce(2, 2, .1f, 1.3f);
    }

    void ResetAttackMelee2()
    {
        AttackMelee1.SetActive(false);
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
    private void ResetAttack()
    {
        alreadyAttacked = false;
    }


}
