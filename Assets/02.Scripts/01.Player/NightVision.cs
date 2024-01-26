using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NightVision : MonoBehaviour
{
    public GameObject nightvision;
    private bool hasNightVision = false;

    public void AddNightVisionItem()
    {
        hasNightVision = true;
        nightvision.SetActive(hasNightVision);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.N))
        {
            if (hasNightVision)
            {
                nightvision.SetActive(!nightvision.activeInHierarchy);
            }
        }
        
    }
}
