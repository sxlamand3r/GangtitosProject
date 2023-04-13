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
            canSeeEnemy = true;
        }
        else
        {
            canSeeEnemy = false;
        }
    }
}
