using UnityEngine;

public class InteractWithObjects : MonoBehaviour
{
    public GameObject player; 
    public GameObject[] objectsToInteract; 
    public Transform interactPosition; 

    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.E) && IsPlayerFacingObjects())
        {
           
            foreach (GameObject obj in objectsToInteract)
            {
                if (obj.name == "1" || obj.name == "2")
                    obj.SetActive(false);
                else if (obj.name == "4")
                    obj.SetActive(true);
            }
        }
    }

    
    bool IsPlayerFacingObjects()
    {
        Vector3 direction = interactPosition.position - player.transform.position;
        float angle = Vector3.Angle(player.transform.forward, direction);

      
        return angle < 30f; 
    }
}
