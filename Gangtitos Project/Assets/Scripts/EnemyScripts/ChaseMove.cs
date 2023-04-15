using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ChaseMove : MonoBehaviour
{
    public NavMeshAgent agent;
    public float range;
    public float chaseTime; // tiempo que perseguirá al jugador antes de volver a su patrón aleatorio
    public float viewRange; // rango de vision
    public Transform centrePoint;

    private Transform target;
    private bool isChasing = false;
    private float timeLeft; // tiempo restante de persecución
    private EnemyFOV enemyFOV;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        timeLeft = chaseTime;
        enemyFOV = GetComponent<EnemyFOV>();
    }

    void Update()
    {
        if (isChasing)
        {
            if (target != null) // si el jugador está en rango de vision
            {
                agent.SetDestination(target.position);
                timeLeft -= Time.deltaTime; // disminuir el tiempo de persecución
                if (timeLeft <= 0) // si se acaba el tiempo, volver al patrón aleatorio
                {
                    isChasing = false;
                    timeLeft = chaseTime;
                }
            }
            else // si el jugador ya no está en rango de vision
            {
                isChasing = false;
                timeLeft = chaseTime;
            }
        }
        else // patrón aleatorio
        {
            if (agent.remainingDistance <= agent.stoppingDistance)
            {
                Vector3 point;
                if (RandomPoint(centrePoint.position, range, out point))
                {
                    agent.SetDestination(point);
                }
            }
        }

        if (enemyFOV.CanSeePlayer())
        {
            isChasing = true;
            target = enemyFOV.player;
        }
        else
        {
            isChasing = false;
            target = null;
            timeLeft = chaseTime;
        }
    }

    bool RandomPoint(Vector3 center, float range, out Vector3 result)
    {
        Vector3 randomPoint = center + Random.insideUnitSphere * range;
        NavMeshHit hit;
        if (NavMesh.SamplePosition(randomPoint, out hit, 1.0f, NavMesh.AllAreas))
        {
            result = hit.position;
            return true;
        }
        result = Vector3.zero;
        return false;
    }

    private void OnTriggerEnter(Collider other) // cuando el jugador entra en rango de vision
    {
        if (other.CompareTag("Player"))
        {
            target = other.transform;
            isChasing = true;
        }
    }

    private void OnTriggerExit(Collider other) // cuando el jugador sale de rango de vision
    {
        if (other.CompareTag("Player"))
        {
            target = null;
            isChasing = false;
            timeLeft = chaseTime;
        }
    }
}
