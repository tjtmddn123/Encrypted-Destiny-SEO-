using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuOption : MonoBehaviour
{
    public GameObject Menu;
    public GameObject Option;

    public void Start()
    {
        Menu.SetActive(DataHolder.Instance.button1);
        Option.SetActive(DataHolder.Instance.button2);
    }
}
