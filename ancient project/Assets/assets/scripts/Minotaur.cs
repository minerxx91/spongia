using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using TMPro;
using UnityEngine.UIElements;
using Unity.VisualScripting;

public class Minotaur : MonoBehaviour
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
    public float timeBetweenAttacks;
    bool alreadyAttacked;

    //States


    float gravityIncrease = 0;
    public float sightRange, MeleeAttackRange, MidAttackRange, RangerAttackRange;
    public bool playerInSightRange, playerInMeleeAttackRange, playerInMidAttackRange, playerInMidAttackRange2, playerInRangerAttackRange, playerInRangerAttackRange2;



    public float Health = 100;
    public float maxHealth = 100;
    TextMeshPro healthbar;
    ParticleSystem selectAura;
    Light orangeLight;
    [SerializeField] ParticleSystem swing;
    [SerializeField] ParticleSystem GroundBlast;
    [SerializeField] ParticleSystem MeleeBlast;

    private bool Animating;
    private Animator anim;
    private Vector3 Targetposition;

    //public GameObject AttackMelee1;
    //public GameObject AttackMelee2;


    private bool MidAttackLook = false;

    void Awake()
    {

        agent = GetComponent<NavMeshAgent>();
        rend = GetComponent<Renderer>();

        anim = GetComponent<Animator>();

        //AttackMelee1.SetActive(false);
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

    }

    void DoAttackMelee1()
    {
        //AttackMelee1.SetActive(true);
        Invoke(nameof(ResetAttackMelee1), .1f);
    }

    void ResetAttackMelee1()
    {
        //AttackMelee1.SetActive(false);
    }

    void DoAttackMelee2()
    {
        //AttackMelee2.SetActive(true);
        Invoke(nameof(ResetAttackMelee2), .1f);
    }

    void ResetAttackMelee2()
    {
        //AttackMelee2.SetActive(false);
    }

    void Update()
    {
        if (MidAttackLook == true) transform.LookAt(Targetposition);
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
        playerInMidAttackRange = Physics.CheckSphere(transform.position, MidAttackRange, whatIsPlayer);
        playerInMidAttackRange2 = Physics.CheckSphere(transform.position, MidAttackRange - 3, whatIsPlayer);
        playerInRangerAttackRange = Physics.CheckSphere(transform.position, RangerAttackRange, whatIsPlayer);
        playerInRangerAttackRange2 = Physics.CheckSphere(transform.position, 8, whatIsPlayer);
        transform.rotation = Quaternion.Euler(new Vector3(0, transform.rotation.eulerAngles.y, 0));
        if (!Animating)
        {
            if (this.gameObject.name == "Poseidon")
            {
                if (!playerInSightRange && !playerInMeleeAttackRange) Patroling();
                if (playerInSightRange && !playerInMeleeAttackRange) Chasing();
                if (playerInSightRange && (playerInMeleeAttackRange || playerInRangerAttackRange))
                {
                    if (playerInMeleeAttackRange)
                    {
                        MeleeAttacking();
                    }
                    else if (playerInMidAttackRange && !playerInMidAttackRange2)
                    {
                        MidAttacking();
                    }
                    else if (playerInRangerAttackRange && !playerInRangerAttackRange2)
                    {
                        RangedAttacking();
                    }
                }




            }
            else
            {
                if (!playerInSightRange && !playerInMeleeAttackRange) Patroling();
                if (playerInSightRange && !playerInMeleeAttackRange) Chasing();
                if (playerInRangerAttackRange && playerInSightRange) RangedAttacking();
                if (playerInMeleeAttackRange && playerInSightRange) MeleeAttacking();
            }
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
            managerVariables.Poseidon.DamageIncrease = 0;
            anim.SetTrigger("melee1");
            //Invoke(nameof(MeleeBlastParticel), 1f);
            Invoke(nameof(DoAttackMelee1), 1f);
            swing.Play();

            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }


    }

    void swingParticel()
    {
        swing.Play();
        MidAttackLook = false;
        Animating = true;
    }

    void MeleeBlastParticel()
    {
        MeleeBlast.Play();
    }
    void swingTP()
    {

    }

    void GroundBlastParticel()
    {
        GroundBlast.Play();
    }

    private void MidAttacking()
    {
        anim.SetBool("walk", false);
        

        if (!alreadyAttacked)
        {
            MidAttackLook = true;
            managerVariables.Poseidon.DamageIncrease = 60;
            
            anim.SetTrigger("melee2");

            Invoke(nameof(DoAttackMelee2), 2.5f);
            Invoke(nameof(swingParticel), 2f);
            Invoke(nameof(GroundBlastParticel), 2.5f);


            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }


    }

    void throwTrident()
    {
        //Instantiate(projectile, trident.transform.position, Quaternion.Euler(new Vector3(90, transform.rotation.eulerAngles.y, 0)));
    }
    void RangedAttacking()
    {
        transform.LookAt(Targetposition);
        anim.SetBool("walk", true);
        if (!alreadyAttacked)
        {
            managerVariables.Poseidon.DamageIncrease = 10;
            anim.SetTrigger("range");
            Invoke(nameof(throwTrident), .5f);



            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
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
        Gizmos.DrawWireSphere(transform.position, MidAttackRange);
        Gizmos.DrawWireSphere(transform.position, MidAttackRange - 3);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, RangerAttackRange);
    }
}