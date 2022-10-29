using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using TMPro;

public class EnemyNavMesh : MonoBehaviour
{
    public NavMeshAgent agent;
    public float patrolingSpeed;
    public float chasingSpeed;

    public Transform player;

    GameObject Manager;
    manager managerVariables;

    public Material[] material;

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
    public GameObject projectile;
    public GameObject trident;

    //States
    public float sightRange, MeleeAttackRange, RangerAttackRange;
    public bool playerInSightRange, playerInMeleeAttackRange, playerInRangerAttackRange;
    float gravityIncrease = 0;

    public float Health = 100;
    TextMeshPro healthbar;
    ParticleSystem selectAura;
    Light orangeLight;
    [SerializeField] ParticleSystem swing;

    private Animator anim;


    void Awake()
    {
        
        agent = GetComponent<NavMeshAgent>();
        rend = GetComponent<Renderer>();
        
        anim = GetComponent<Animator>();
       


    }
    private void Start()
    {
        healthbar = transform.Find("Health").GetComponent<TextMeshPro>();
        selectAura = transform.Find("Aura").GetComponent<ParticleSystem>();
        orangeLight = transform.Find("Orange").GetComponent<Light>();
        orangeLight.gameObject.SetActive(false);
        Manager = GameObject.Find("Manager");
        managerVariables = Manager.GetComponent<manager>();
        player = GameObject.Find("Player").transform;
        
    }

    void Update()
    {
        
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInMeleeAttackRange = Physics.CheckSphere(transform.position, MeleeAttackRange, whatIsPlayer);
        playerInRangerAttackRange = Physics.CheckSphere(transform.position, RangerAttackRange, whatIsPlayer);
        transform.LookAt(player);
        transform.rotation = Quaternion.Euler(new Vector3(0, transform.rotation.eulerAngles.y, 0)); 
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
                else
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
        
        materialDelay += Time.deltaTime;

        healthbar.text = Health.ToString();

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
        anim.SetBool("walk", true);
        agent.SetDestination(player.position);
        agent.speed = chasingSpeed;

        
    }

    private void MeleeAttacking()
    {
        anim.SetBool("walk", false);
        agent.SetDestination(transform.position);

        if (!alreadyAttacked)
        {
            anim.SetTrigger("melee1");
            swing.Play();

            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
        
        
    }
    void RangedAttacking()
    {
        anim.SetBool("walk", true);
        if (!alreadyAttacked)
        {
            anim.SetTrigger("range");
            print(gameObject.transform.rotation.y);
            Instantiate(projectile, trident.transform.position, Quaternion.Euler(new Vector3(90, transform.rotation.eulerAngles.y, 0)));


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
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, RangerAttackRange);
    }
}
