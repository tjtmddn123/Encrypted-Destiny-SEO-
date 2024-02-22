using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class UIManager : MonoBehaviour
{
    public Player_HT player;

    public GameObject playerObj;

    public GameObject[] OpenableUI;

    public GameObject OptionMenu;

    private GameObject OpenedUI;

    // Update is called once per frame
    void Update()
    { 

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            OpenedUI = Array.Find(OpenableUI, element => element.activeInHierarchy == true);
            if (IsUIOpening())
            {
                OpenedUI.SetActive(false);
            }
            else if(IsUIOpening() == false && playerObj.activeInHierarchy)
            {
                OptionMenu.SetActive(true);
            }
        }        
    }

    public bool IsUIOpening()
    {
        if (Array.Find(OpenableUI, element => element.activeInHierarchy == true))
        {
            Cursor.lockState = CursorLockMode.None;
            return true;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            return false;
        }
    }

}
