using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class nightvisionitemtest : MonoBehaviour
{
    public GameObject WallText;
    public GameObject RText;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.N))
        {
            WallText.SetActive(!WallText.activeInHierarchy);
            RText.SetActive(!RText.activeInHierarchy);
        }
    }
}
