using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;

public class EnemyPleb : MonoBehaviour
{
    public NavMeshAgent agent;
    public float chasingSpeed;

    public Transform player;
    public Vector3 walkPoint;
    public bool walkPointSet;

    private Animator anim;

    public bool playerInSightRange, playerInMeleeAttackRange;
    public float sightRange, MeleeAttackRange;

    public LayerMask whatIsPlayer;

    public float timeBetweenAttacks, shorttimebetweenAttacks;
    bool alreadyAttacked = false;
    Vector3 Targetposition;
    int meleeAnim = 0;

    GameObject attackHorizontal;
    void Start()
    {
        anim = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.Find("Player").transform;
        attackHorizontal = GameObject.Find("attack1");
        attackHorizontal.SetActive(false);
    }

    void Update()
    {
        Targetposition = new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z);
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInMeleeAttackRange = Physics.CheckSphere(transform.position, MeleeAttackRange, whatIsPlayer);
        if (playerInMeleeAttackRange) MeleeAttacking();
        else if (playerInSightRange && !playerInMeleeAttackRange) Chasing();
        else
        {
            anim.SetBool("isRunning", false);
        }
    }

    private void Chasing()
    {
        transform.LookAt(Targetposition);
        anim.SetBool("isRunning", true);
        agent.SetDestination(player.position);
        agent.speed = chasingSpeed;


    }

    private void MeleeAttacking()
    {

        transform.LookAt(Targetposition);
        anim.SetBool("isRunning", false);
        agent.SetDestination(transform.position);

        if (!alreadyAttacked)
        {
            alreadyAttacked = true;
            if (meleeAnim == 0)
            {
                anim.SetTrigger("attack1");
                // meleeAnim = 1;
                Invoke(nameof(ResetAttack), timeBetweenAttacks);
            }
            else
            {
                anim.SetTrigger("attack2");
                meleeAnim = 0;
                Invoke(nameof(ResetAttack), timeBetweenAttacks);
            }

            Invoke(nameof(DoAttackMelee1), .55f);
        }
    }

    private void ResetAttack()
    {
        alreadyAttacked = false;
    }
    void DoAttackMelee1()
    {
        attackHorizontal.SetActive(true);
        Invoke(nameof(ResetAttackMelee1), .1f);
    }

    void ResetAttackMelee1()
    {
        attackHorizontal.SetActive(false);
    }


}
