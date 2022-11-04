using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using TMPro;
using UnityEngine.UIElements;
using Unity.VisualScripting;
using UnityEngine.UIElements.Experimental;
using EZCameraShake;

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
    public float timeBetweenAttacks, timebetweenRangedAttacks, shorttimebetweenAttacks, timeToRage;
    bool alreadyAttacked;
    public float RageTime = 5;
    float RagedTime = 0;
    float timeToRageTick = 0;
    bool Raged = false;
    Vector3 Bigsize = new Vector3(0.25f, 0.25f, 0.25f);
    Vector3 size = new Vector3(0.15f, 0.15f, 0.15f);

    //States
    float gravityIncrease = 0;
    public float sightRange, MeleeAttackRange, RangerAttackRange;
    public bool playerInSightRange, playerInMeleeAttackRange, playerInRangerAttackRange, playerInRangerAttackRange2;





    //Particles
    ParticleSystem selectAura;
    Light orangeLight;
    [SerializeField] ParticleSystem SwingRight;
    [SerializeField] ParticleSystem SwingLeft;

    //materials
    [SerializeField] Material telo;

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
        timeToRageTick += Time.deltaTime;
        if (timeToRageTick >= timeToRage)
        {
            Raged = true;
            GetComponent<AudioSource>().clip = GameObject.Find("AudioManager").GetComponent<AudioManager>().RageDychanie;
            if (!GetComponent<AudioSource>().isPlaying)
                GetComponent<AudioSource>().Play();
            telo.color = new Color32(150, 89, 69, 255);
            if (this.gameObject.transform.localScale != Bigsize)
                this.gameObject.transform.localScale += new Vector3(0.01f, 0.01f, 0.01f);
            if (timeToRageTick > timeToRage + RageTime)
            {
                timeToRageTick = 0;
                Raged = false;
                telo.color = new Color32(115, 89, 69, 255);

            }
        }
        if (!Raged)
        {
            GetComponent<AudioSource>().clip = GameObject.Find("AudioManager").GetComponent<AudioManager>().Dychanie ;
            if (!GetComponent<AudioSource>().isPlaying)
                GetComponent<AudioSource>().Play();

            if (this.gameObject.transform.localScale != size)
                this.gameObject.transform.localScale -= new Vector3(0.01f, 0.01f, 0.01f);
            telo.color = new Color32(115, 89, 69, 255);
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
        if (!Charging)
        {
            if (this.gameObject.name == "Minotaur")
            {
                if (!playerInSightRange && !playerInMeleeAttackRange) Patroling();
                if (playerInSightRange && !playerInMeleeAttackRange) Chasing();
                if (playerInSightRange && (playerInMeleeAttackRange || playerInRangerAttackRange))
                {
                    if (playerInMeleeAttackRange)
                    {
                        MeleeAttacking();
                    }

                    else if (!alreadyAttacked && !playerInRangerAttackRange)
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
        else
        {
            transform.position += transform.forward * 16 * Time.deltaTime;
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
            alreadyAttacked = true;
            managerVariables.Minotaur.DamageIncrease = 0;
            if (meleeAnim == 0)
            {
                anim.SetTrigger("melee1");
                meleeAnim = 1;
                SwingRight.Play();
                //attack melee 1


                if (!Raged)
                {
                    audioManager.PlayMinotaurAttackDelay();
                }
                else
                {
                    audioManager.PlayMinotaurAttackRageDelay();
                    
                }
                Invoke(nameof(ResetAttack), shorttimebetweenAttacks);
            }
            else
            {
                anim.SetTrigger("melee2");
                meleeAnim = 0;
                SwingLeft.Play();
                //attack melee 2

                if (!Raged)
                {
                    audioManager.PlayMinotaurAttackDelay();
                }
                else
                {
                    audioManager.PlayMinotaurAttackRageDelay();

                }

                Invoke(nameof(ResetAttack), timeBetweenAttacks);
            }



            //Invoke(nameof(MeleeBlastParticel), 1f);
            Invoke(nameof(DoAttackMelee1), .72f);
        }


    }

    void swingParticel()
    {
        SwingRight.Play();
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
        //transform.LookAt(Targetposition);
        //anim.SetBool("walk", true);
        if (!Charging)
        {
            managerVariables.Minotaur.DamageIncrease = 10;

            Invoke(nameof(Charge), .5f);
            //run attack


            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timebetweenRangedAttacks);
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
        Gizmos.DrawWireSphere(transform.position, RangerAttackRange - 8);
    }
}