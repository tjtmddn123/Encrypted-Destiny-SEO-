using UnityEngine;
using UnityEngine.UI;

public class LampBtn : MonoBehaviour
{
    public GameObject lightObject; 
    public GameObject paperObject; 
    public GameObject paperText; 

    private bool isLightOn = false; 

    void Start()
    {
       
        paperText.gameObject.SetActive(false);
    }

    void Update()
    {
     
        if (Input.GetKeyDown(KeyCode.E))
        {
            ToggleLight();
        }
    }

    void ToggleLight()
    {
       
        isLightOn = !isLightOn;

       
        if (isLightOn)
        {
            lightObject.SetActive(true);
            paperText.gameObject.SetActive(true);
        }
        else
        {
            lightObject.SetActive(false);
            paperText.gameObject.SetActive(false);
        }
    }


    
}
