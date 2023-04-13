using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMove : MonoBehaviour
{
    [SerializeField] private Transform Player;

    NavMeshAgent agent;
    PlayerFOV playerFOV;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        playerFOV = GetComponent<PlayerFOV>();
    }

    private void Update()
    {
        if (playerFOV.canSeeEnemy == false)
        {
            agent.SetDestination(Player.position);
            agent.isStopped = false;
        }
        else
        {
            agent.isStopped = true;
        }

        float DistanceToPlayer = Vector3.Distance(Player.position, transform.position);
        if (DistanceToPlayer < 2)
        {
            //Attack uwu
        }
    }
}