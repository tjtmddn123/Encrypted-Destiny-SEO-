using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class nightvisionitemtest : MonoBehaviour
{
    public GameObject Rustkey;
    public GameObject Text;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.N))
        { 
            Rustkey.SetActive(!Rustkey.activeInHierarchy); 
            Text.SetActive(!Text.activeInHierarchy);
        }
    }
}
