using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NightVision : MonoBehaviour
{
    public GameObject nightvision;
    public GameObject nightvisionn;
    private bool hasNightVision = false;
    private bool canUseNightVision = true;

    public void AddNightVisionItem()
    {
        hasNightVision = true;
        canUseNightVision = true;
    }

    private void Update()
    {
        if (hasNightVision && Input.GetKeyDown(KeyCode.N))
        {
            nightvisionn.SetActive(!nightvisionn.activeInHierarchy);
        }
        else if (!hasNightVision)
        {
            canUseNightVision = false;
        }
    }
}
