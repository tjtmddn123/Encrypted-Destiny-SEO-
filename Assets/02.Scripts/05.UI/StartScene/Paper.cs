using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    public float interactionDistance = 100f; 
    public LayerMask interactableLayer; 

    private GameObject heldObject; 

    void Update()
    {
       
        if (Input.GetKeyDown(KeyCode.E))
        {
           
            InteractWithObject();
        }

       
        if (heldObject != null && Input.GetKeyDown(KeyCode.E))
        {
            DropObject();
        }
    }

    void InteractWithObject()
    {
        
        Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, interactionDistance, interactableLayer))
        {
          
            if (hit.collider.CompareTag("Interactable"))
            {
                
                if (heldObject == null)
                {
                  
                    PickUpObject(hit.collider.gameObject);
                }
                else
                {
                  
                    DropObject();
                }
            }
        }
    }

    void PickUpObject(GameObject objToPickUp)
    {
       
        heldObject = objToPickUp;
        heldObject.SetActive(false); 
    }

    void DropObject()
    {
        
        heldObject.SetActive(true);
        heldObject = null;
    }
}
