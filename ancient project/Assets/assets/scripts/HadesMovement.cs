using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;

public class HadesMovement : MonoBehaviour
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

    public float sightRange, MeleeAttackRange, RangerAttackRange;
    public bool playerInSightRange, playerInMeleeAttackRange, playerInRangerAttackRange, playerInRangerAttackRange2;

    Transform player;
    private Vector3 Targetposition;
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        rend = GetComponent<Renderer>();
        anim = GetComponent<Animator>();

        Manager = GameObject.Find("Manager");
        managerVariables = Manager.GetComponent<manager>();
        player = GameObject.Find("Player").transform;
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();

        managerVariables.Hades.Health = managerVariables.Hades.maxHealth;
    }

    void Update()
    {
        if (!GameObject.Find("Player").GetComponent<Player>().died)
        {
            Targetposition = new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z);
            Chasing();
        }
        else 
        {
            agent.SetDestination(transform.position); 
            anim.SetBool("walk", false);
        }
    }

    private void Chasing()
    {
        transform.LookAt(Targetposition);
        anim.SetBool("walk", true);
        agent.SetDestination(player.position);
        agent.speed = chasingSpeed;


    }
}
