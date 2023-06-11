using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPickUp : MonoBehaviour
{
    [SerializeField]
    private LayerMask pickableLayerMask;

    [SerializeField]
    private Transform playerCameraTransform;

    [SerializeField]
    private GameObject pickUpUI;

    [SerializeField]
    [Min(1)]
    private float hitRange = 3;

    [SerializeField]
    private Transform pickUpParent;

    [SerializeField]
    private GameObject inHandItem;

    private RaycastHit hit;

    private ControllerUI cui;

    private void Start()
    {
        cui = FindObjectOfType<ControllerUI>();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Q))
        {
            if (inHandItem != null)
            {
                inHandItem.transform.SetParent(null);
                inHandItem = null;
                Rigidbody rb = hit.collider.GetComponent<Rigidbody>();
                if (rb != null)
                {
                    rb.isKinematic = false;
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            if (hit.collider != null && inHandItem == null)
            {
                Debug.Log(hit.collider.name);
                Rigidbody rb = hit.collider.GetComponent<Rigidbody>();
                if (hit.collider.GetComponent<RightHandObject>())
                {
                    inHandItem = hit.collider.gameObject;
                    inHandItem.transform.position = Vector3.zero;
                    inHandItem.transform.rotation = Quaternion.identity;
                    inHandItem.transform.SetParent(pickUpParent.transform, false);
                    if (rb != null)
                    {
                        rb.isKinematic = true;
                    }
                    return;
                }
                if (hit.collider.GetComponent<MovableObject>())
                {
                    inHandItem = hit.collider.gameObject;
                    inHandItem.transform.SetParent(pickUpParent.transform, true);
                    if (rb != null)
                    {
                        rb.isKinematic = true;
                    }
                    return;
                }
            }
        }

        Debug.DrawRay(playerCameraTransform.position, playerCameraTransform.forward * hitRange, Color.red);
        if (hit.collider != null)
        {
            hit.collider.GetComponent<Highlight>()?.ToggleHighlight(false);
            //pickUpUI.SetActive(false);
            cui.DeactivateTextObject();
        }

        if (inHandItem != null)
        {
            return;
        }

        if (Physics.Raycast(playerCameraTransform.position, playerCameraTransform.forward, out hit, hitRange, pickableLayerMask))
        {
            hit.collider.GetComponent<Highlight>()?.ToggleHighlight(true);
            //pickUpUI.SetActive(true);
            cui.ActivateAndSetTextObject($"Agarrar {hit.collider.gameObject.name} con la E");
        }
    }

    
}
    

