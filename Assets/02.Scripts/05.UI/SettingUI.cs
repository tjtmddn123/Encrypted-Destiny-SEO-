using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingUI : MonoBehaviour
{
    public GameObject settingUI;
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            settingUI.SetActive(!settingUI.activeInHierarchy);
    }
}
