using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    enum AIState{
        Idle, Patrolling, Chasing, Attacking
    }

    private Animator animator;

    [Header("Movement Speeds")]
    [SerializeField] private float patrolSpeed = 1.5f;
    [SerializeField] private float chaseSpeed = 4f;


    [Header("Patrol")]
    [SerializeField] private Transform waypoints;
    [SerializeField] private float  waitAtPoint = 2f;
    private int currentWaypoint;
    private float waitCounter;

    [Header("Components")]
    UnityEngine.AI.NavMeshAgent agent;

    [Header("AI States")]
    [SerializeField] private AIState currentState;

    [Header("Chasing")]
    [SerializeField] private float chaseRange;

    [Header("Suspicious")]
    [SerializeField] private float suspiciousTime;
    private float timeSinceLastSawPlayer;

    [Header("Attacking")]
    [SerializeField] private float attackRange = 1f;
    [SerializeField] private float  attackTime = 2f;
    private float timeToAttack;

    private GameObject player;
    private Player playerScript;


    void Start()
    {

        animator = GetComponent<Animator>();

        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player");

        playerScript = player.GetComponent<Player>();

        waitCounter = waitAtPoint;
        timeSinceLastSawPlayer = suspiciousTime;
        timeToAttack = attackTime;
    }

    void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);
        
        switch(currentState){
            case AIState.Idle:
                agent.speed = patrolSpeed;
                animator.Play("Z_Idle");
                if(waitCounter > 0){
                    waitCounter -= Time.deltaTime;
                }
                else{
                    currentState = AIState.Patrolling;
                    agent.SetDestination(waypoints.GetChild(currentWaypoint).position);
                }

                if(distanceToPlayer <= chaseRange){
                    currentState = AIState.Chasing;
                }

                break;

        case AIState.Patrolling:
            agent.speed = patrolSpeed;
            animator.Play("Z_Walk_InPlace");
            if(agent.remainingDistance <= 0.2f)
            {
                currentWaypoint = (Random.Range(1, 5));
                if(currentWaypoint >= waypoints.childCount)
                {
                    currentWaypoint = 0;
                }

                currentState = AIState.Idle;
                waitCounter = waitAtPoint;
            }
            
            if(distanceToPlayer <= chaseRange)
            {
                currentState = AIState.Chasing;
            }
            
            break;
        
        case AIState.Chasing:
            agent.speed = chaseSpeed;
            animator.Play("Z_Run_InPlace");
            agent.SetDestination(player.transform.position);
            if(distanceToPlayer > chaseRange)
            {
                agent.isStopped = true;
                agent.velocity = Vector3.zero;
                timeSinceLastSawPlayer -= Time.deltaTime;

                if(timeSinceLastSawPlayer <= 0)
                {
                    currentState = AIState.Idle;
                    timeSinceLastSawPlayer = suspiciousTime;
                    agent.isStopped = false;
                }
            }
                
            if(distanceToPlayer <= attackRange)
            {
                animator.Play("Z_Attack");
                currentState = AIState.Attacking;
                agent.velocity = Vector3.zero;
                agent.isStopped = true;
            }
                
            break;
        
        case AIState.Attacking:
            Vector3 lookPos = player.transform.position;
            lookPos.y = transform.position.y;
            transform.LookAt(lookPos);

            timeToAttack -= Time.deltaTime;

            if (timeToAttack <= 0)
            {
                animator.Play("Z_Attack");
                timeToAttack = attackTime;

                if (distanceToPlayer <= attackRange)
                {
                    playerScript.setHealth(-10f); // Uberie 10 HP pri útoku
                }
            }

            if(distanceToPlayer > attackRange)
            {
                currentState = AIState.Chasing;
                agent.isStopped = false;
            }
        break;
        }
    }   
}
