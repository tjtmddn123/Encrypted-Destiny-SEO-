using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NightVision : MonoBehaviour
{
    public GameObject nightvision;
    public GameObject nightvisionItem;
  
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.N))
            nightvision.SetActive(!nightvision.activeInHierarchy);
    }
}
