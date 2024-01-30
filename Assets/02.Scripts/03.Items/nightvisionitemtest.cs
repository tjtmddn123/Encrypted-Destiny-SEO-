using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class nightvisionitemtest : MonoBehaviour
{
    public GameObject WallText;
    public GameObject RText;
    private NightVision nightVision;

    private void Start()
    {
        nightVision = Camera.main.GetComponent<NightVision>();
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.N))
        {
            if (nightVision.hasNightVision == true)
            {
                WallText.SetActive(!WallText.activeInHierarchy);
                RText.SetActive(!RText.activeInHierarchy);
            }           
        }
    }
}
