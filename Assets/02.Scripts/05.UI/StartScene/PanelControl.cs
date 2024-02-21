using UnityEngine;

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
        option.SetActive(true); 
        isPaused = true;
    }

    void ResumeGame()
    {      
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
