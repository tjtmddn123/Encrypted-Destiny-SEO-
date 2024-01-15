using System;
using System.Collections;
using System.Collections.Generic;

using TMPro;

using UnityEngine;
using UnityEngine.InputSystem;

public interface IInteractable_HT
{
    void OnInteract();
}
public class InteractManager_HT : MonoBehaviour
{
    public DoorController doorController;
    public float checkRate = 0.05f;
    private float lastCheckTime;
    public float maxCheckDistance;
    public LayerMask layerMask;

    private GameObject curInteractGameobject;
    private IInteractable_HT curInteractable;

    public TextMeshProUGUI promptText;
    private Camera camera;


    // Start is called before the first frame update
    void Start()
    {
        camera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Time.time - lastCheckTime > checkRate)
        {
            lastCheckTime = Time.time;
            Debug.Log(lastCheckTime);
            Ray ray = camera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2));
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, maxCheckDistance, layerMask))
            {
                Debug.Log("Raycast"+lastCheckTime);
                if (hit.collider.gameObject != curInteractGameobject && hit.collider.CompareTag("Item"))
                {
                    curInteractGameobject = hit.collider.gameObject;
                    curInteractable = hit.collider.GetComponent<IInteractable_HT>();
                    SetPromptTextTake();
                }
                else if (hit.collider.gameObject != curInteractGameobject && hit.collider.CompareTag("Door"))
                {
                    curInteractGameobject = hit.collider.gameObject;
                    doorController = curInteractGameobject.GetComponent<DoorController>();
                    SetPromptTextDoor();
                    Debug.Log("문을 바라봐");
                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        Debug.Log("열림");
                        doorController.OpenDoor(curInteractGameobject);
                    }
                }                
            }
            else
            {
                curInteractGameobject = null;
                curInteractable = null;
                promptText.gameObject.SetActive(false);
            }
        }
    }

    private void SetPromptTextTake()
    {
        promptText.gameObject.SetActive(true);
        promptText.text = string.Format("[E] Take");
    }

    private void SetPromptTextDoor()
    {
        promptText.gameObject.SetActive(true);
        if (doorController.isOpening == false)
        {
            promptText.text = string.Format("[E] Open");
        }
        else
        {
            promptText.text = string.Format("[E] Close");
        }
        
    }

    public void OnInteractInput(InputAction.CallbackContext callbackContext)
    {
        if (callbackContext.phase == InputActionPhase.Started && curInteractable != null)
        {
            curInteractable.OnInteract();
            curInteractGameobject = null;
            curInteractable = null;
            promptText.gameObject.SetActive(false);
        }
    }
}
