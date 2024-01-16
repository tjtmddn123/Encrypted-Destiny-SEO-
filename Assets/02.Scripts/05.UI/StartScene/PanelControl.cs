using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuOption : MonoBehaviour
{
    public GameObject menu;
    public GameObject option;


    public void ActivatePanel1()
    {
        menu.SetActive(true);
        option.SetActive(false);
    }


    public void ActivatePanel2()
    {
        menu.SetActive(false);
        option.SetActive(true);
    }
}
