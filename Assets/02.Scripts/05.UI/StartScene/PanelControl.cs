using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuOption : MonoBehaviour
{
    public GameObject menu;
    public GameObject option;

    private bool isPaused = false;


    private void Start()
    {
        option.SetActive(false); 
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    void PauseGame()
    {       
        Cursor.lockState = CursorLockMode.None; 
        Cursor.visible = true; 
        option.SetActive(true); 
        isPaused = true;
    }

    void ResumeGame()
    {      
        Cursor.lockState = CursorLockMode.Locked; 
        Cursor.visible = false; 
        option.SetActive(false); 
        isPaused = false;
    }

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
