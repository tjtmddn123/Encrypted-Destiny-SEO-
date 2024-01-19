using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Camera playerCamera;
    private bool isHoldingObject = false;
    private GameObject heldObject;

    void Start()
    {
        playerCamera = Camera.main;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            InteractWithObject();
        }
    }

    void InteractWithObject()
    {
        RaycastHit hit;

        if (Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out hit, 2f))
        {
            if (hit.collider.CompareTag("Item"))
            {
                if (isHoldingObject)
                {
                    
                    DropObject();
                }
                else
                {
                    
                    PickUpObject(hit.collider.gameObject);
                }
            }
        }
        else if (isHoldingObject)
        {
            
            DropObject();
        }
    }

    void PickUpObject(GameObject objToPickup)
    {
        isHoldingObject = true;
        heldObject = objToPickup;
        objToPickup.transform.SetParent(transform);
        objToPickup.GetComponent<Rigidbody>().isKinematic = true;
    }

    void DropObject()
    {
        isHoldingObject = false;
        heldObject.transform.SetParent(null);
        heldObject.GetComponent<Rigidbody>().isKinematic = false;
        heldObject = null;
    }
}
