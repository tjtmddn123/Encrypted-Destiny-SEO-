using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OfficeCase : MonoBehaviour
{
    public GameObject InteractCase;
    

    public void TakeCase()
    {        
            Vector3 newPosition = InteractCase.transform.position;
            newPosition.x += 0.2f;
            InteractCase.transform.position = newPosition;              
        
    }
}
