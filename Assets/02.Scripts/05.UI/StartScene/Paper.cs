using UnityEngine;

public class PaperInteraction : MonoBehaviour
{
    public GameObject heldPaper; 
    public bool isHoldingPaper = false; 

    public void Update()
    {
       
        if (Input.GetMouseButtonDown(0))
        {
            
            if (!isHoldingPaper)
            {
                PickUpPaper();
            }
            else
            {
                
                DropPaper();
            }
        }

        
        if (isHoldingPaper)
        {
            MoveHeldPaper();
        }
    }

    public void PickUpPaper()
    {
        
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            
            if (hit.collider.CompareTag("Paper"))
            {
                isHoldingPaper = true;
                heldPaper = hit.collider.gameObject;
            }
        }
    }

    public void DropPaper()
    {
        
        isHoldingPaper = false;
        heldPaper = null;
    }

    public void MoveHeldPaper()
    {
        
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            heldPaper.transform.position = new Vector3(hit.point.x, hit.point.y, heldPaper.transform.position.z);
        }
    }
}
