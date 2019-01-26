using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyController : MonoBehaviour
{
    private enum AI
    {
        AttackPlayer,
        Roam,
        WalkTarget
    }

    private NavMeshAgent agent;

    [SerializeField]
    private AI currentAI = AI.AttackPlayer;

    private enum AttackState
    {
        Approach,
        FindTarget,
        Idle,
        StartIdle
    }


    private AttackState currentAttackState = AttackState.Approach;

    private Vector3 playerApproachPoint;
    private float currentmoveTime = 0;
    [Header("Attack")]
    public Transform player;

    public EnemyAttackSettings attackSettings;
    
    [Header("Roam")]
    public Transform[] roamPoints;
    public float reachDistance = 0.5f;
    private int pointID = 0;
    private int lastID = -1;
    [Header("WalkTarget")]
    public Transform target;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        if(currentAI == AI.Roam)
        {
            FindNewRoamPoint();
        }
        if(currentAI == AI.WalkTarget)
        {
            agent.SetDestination(target.position);
        }
    }

    // Update is called once per frame
    void Update()
    {
        switch(currentAI)
        {
            case AI.AttackPlayer:
                switch(currentAttackState)
                {
                    case AttackState.FindTarget:
                        Vector2 randCirc = Random.insideUnitCircle;
                        playerApproachPoint = player.transform.position + (new Vector3(randCirc.x, 0, randCirc.y) * attackSettings.playerApproachDistance);
                        currentmoveTime = Random.Range(attackSettings.minMoveTime, attackSettings.maxMoveTime);
                        agent.isStopped = false;
                        currentAttackState = AttackState.Approach;
                        break;
                    case AttackState.Approach:
                        agent.SetDestination(playerApproachPoint);
                        currentmoveTime -= Time.deltaTime;
                        if (currentmoveTime <= 0)
                        {
                            currentAttackState = AttackState.StartIdle;
                        }
                        break;
                    case AttackState.StartIdle:
                        agent.isStopped = true;
                        currentmoveTime = Random.Range(attackSettings.minIdleTime, attackSettings.maxIdleTime);
                        currentAttackState = AttackState.Idle;
                        break;
                    case AttackState.Idle:
                        currentmoveTime -= Time.deltaTime;
                        if(currentmoveTime <= 0)
                        {
                            currentAttackState = AttackState.FindTarget;
                        }
                        break;
                }
                break;
            case AI.Roam:
                if (Vector3.Distance(transform.position, roamPoints[pointID].position) <= reachDistance)
                {
                    FindNewRoamPoint();
                }
                break;
            case AI.WalkTarget:
                agent.SetDestination(target.position);
                break;
        }
    }


    private void FindNewRoamPoint()
    {
        bool found = false;
        do
        {
            pointID = Random.Range(0, roamPoints.Length);
            if (pointID != lastID)
            {
                found = true;
                lastID = pointID;
            }
        } while (!found);
        agent.SetDestination(roamPoints[pointID].position);
    }
}
