using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFOV : MonoBehaviour
{
    public Camera enemyCamera;
    public LayerMask defaultLayer;
    public float maxDistance;
    public float maxAngle;

    public Transform player;
    [SerializeField] private bool canSeePlayer = false;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        if (player != null)
        {
            Vector3 direction = player.position - enemyCamera.transform.position;
            float angle = Vector3.Angle(direction, enemyCamera.transform.forward);
            if (direction.magnitude < maxDistance && angle < maxAngle)
            {
                RaycastHit hit;
                if (Physics.Raycast(enemyCamera.transform.position, direction.normalized, out hit, maxDistance))
                {
                    if (hit.transform.CompareTag("Player"))
                    {
                        canSeePlayer = true;
                    }
                    else
                    {
                        canSeePlayer = false;
                    }
                }
                else
                {
                    canSeePlayer = true;
                }
            }
            else
            {
                canSeePlayer = false;
            }
        }
        else
        {
            canSeePlayer = false;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            player = other.transform;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            player = null;
            canSeePlayer = false;
        }
    }

    public bool CanSeePlayer()
    {
        return canSeePlayer;
    }

    void OnDrawGizmosSelected()
    {
        if (enemyCamera == null)
            enemyCamera = GetComponent<Camera>();

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, maxDistance);

        Quaternion leftRayRotation = Quaternion.AngleAxis(-maxAngle, enemyCamera.transform.up);
        Quaternion rightRayRotation = Quaternion.AngleAxis(maxAngle, enemyCamera.transform.up);

        Vector3 leftRayDirection = leftRayRotation * enemyCamera.transform.forward;
        Vector3 rightRayDirection = rightRayRotation * enemyCamera.transform.forward;

        Gizmos.color = Color.red;
        Gizmos.DrawLine(enemyCamera.transform.position, enemyCamera.transform.position + leftRayDirection * maxDistance);

        Gizmos.color = Color.green;
        Gizmos.DrawLine(enemyCamera.transform.position, enemyCamera.transform.position + enemyCamera.transform.forward * maxDistance);

        Gizmos.color = Color.blue;
        Gizmos.DrawLine(enemyCamera.transform.position, enemyCamera.transform.position + rightRayDirection * maxDistance);
    }

}


