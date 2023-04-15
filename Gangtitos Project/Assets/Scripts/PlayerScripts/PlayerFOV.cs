using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFOV : MonoBehaviour
{
    public Camera cameraMain;
    Plane[] cameraFrustum;
    Collider colliderMain;

    public bool canSeeEnemy = false;

    public void Start()
    {
        colliderMain = GetComponent<Collider>();
    }

    private void Update()
    {
        var bounds = colliderMain.bounds;
        cameraFrustum = GeometryUtility.CalculateFrustumPlanes(cameraMain);
        if (GeometryUtility.TestPlanesAABB(cameraFrustum, bounds))
        {
            // Use raycasting to check if there are any objects blocking the view
            RaycastHit hit;
            if (Physics.Raycast(cameraMain.transform.position, transform.position - cameraMain.transform.position, out hit))
            {
                if (hit.collider != null && hit.collider.gameObject.CompareTag("Enemy"))
                {
                    canSeeEnemy = true;
                }
                else
                {
                    canSeeEnemy = false;
                }

                // Draw a line to visualize the raycast
                Debug.DrawLine(cameraMain.transform.position, hit.point, Color.red);
            }
            else
            {
                canSeeEnemy = false;
            }
        }
        else
        {
            canSeeEnemy = false;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawRay(cameraMain.transform.position, transform.position - cameraMain.transform.position);
    }
}


