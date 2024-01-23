using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class nightvisionitemtest : MonoBehaviour
{
    public GameObject WallText;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.N))
        {
            WallText.SetActive(!WallText.activeInHierarchy); 
        }
    }
}
