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

    [Header("Door")]
    private DoorController doorController;
    


    [Header("Keypad")]
    private NavKeypad.KeypadButton keypad;

    [Header("Interact")]
    [SerializeField] private float maxCheckDistance;    //최대 상호작용 가능 거리
    [SerializeField] private LayerMask layerMask;       //상호작용 시킬 레이어
    private Player_HT player;
    private float checkRate = 0.05f;
    private float lastCheckTime;
    private bool canPress = true;
    public TextMeshProUGUI promptText;
    private GameObject curInteractGameobject;
    private IInteractable_HT curInteractable;


    [Header("ChangeTall")]
    private bool isSmall = false;    //작아졌는지 여부 확인을 위한 bool 입니다
    public GameObject btn;           //작아지게 하는 오브젝트

    [Header("Water")]
    public GameObject water;
    private WaterRemover waterRemover; 

    [Header("Camera")]
    private Camera _camera;

    // Start is called before the first frame update
    void Start()
    {
        _camera = Camera.main;
        player = GetComponent<Player_HT>();
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Time.time - lastCheckTime > checkRate)
        {
            lastCheckTime = Time.time;
            Ray ray = _camera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2));
            RaycastHit hit;
   
            if (Physics.Raycast(ray, out hit, maxCheckDistance, layerMask))
            {
                if (hit.collider.gameObject != curInteractGameobject && hit.collider.CompareTag("Item"))
                {
                    curInteractGameobject = hit.collider.gameObject;
                    curInteractable = hit.collider.GetComponent<IInteractable_HT>();
                    SetPromptText("[E] Take");
                }
                else if (hit.collider.gameObject != curInteractGameobject && hit.collider.CompareTag("None"))
                {
                    curInteractGameobject = hit.collider.gameObject;
                    curInteractable = hit.collider.GetComponent<IInteractable_HT>();
                    SetPromptText("[E] Use");
                }
                else if (hit.collider.gameObject != curInteractGameobject && hit.collider.CompareTag("Door"))
                {
                    curInteractGameobject = hit.collider.gameObject;
                    doorController = curInteractGameobject.GetComponent<DoorController>();
                    if (doorController.isReverse == false)
                    {
                        if (doorController.isOpening == false)
                        {
                            SetPromptText("[E] Open");
                        }
                        else
                        {
                            SetPromptText("[E] Close");
                        }
                    }
                    if (doorController.isReverse == true)
                    {
                        if (doorController.isOpening == false)
                        {
                            SetPromptText("[E] Close");
                        }
                        else
                        {
                            SetPromptText("[E] Open");
                        }
                    }
                }
                else if (hit.collider.gameObject != curInteractGameobject && hit.collider.CompareTag("Button"))
                {
                    curInteractGameobject = hit.collider.gameObject;
                    keypad = curInteractGameobject.GetComponent<NavKeypad.KeypadButton>();
                    SetPromptText("[E] Push");
                }
                else if (hit.collider.gameObject != curInteractGameobject && hit.collider.CompareTag("Water"))
                {
                    curInteractGameobject = hit.collider.gameObject;
                    waterRemover = curInteractGameobject.GetComponent<WaterRemover>();
                    SetPromptText("[E] Use");
                }
                else if (hit.collider.gameObject != curInteractGameobject && hit.collider.CompareTag("Case"))
                {
                    curInteractGameobject = hit.collider.gameObject;
                    doorController = curInteractGameobject.GetComponent<DoorController>();
                    if (doorController.isOpening == false)
                    {
                        SetPromptText("[E] Open");
                    }
                    else
                    {
                        SetPromptText("[E] Close");
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
        if (curInteractGameobject != null)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (curInteractGameobject.CompareTag("Door") && canPress)
                {
                    Debug.Log("열림");
                    doorController.OpenDoor(curInteractGameobject);
                    StartCoroutine(DelayedInput());
                }
                if (curInteractGameobject == btn)
                {
                    ChangeTall();
                }
                if (curInteractGameobject.CompareTag("Button"))
                {
                    keypad.PressButton();
                }
                if (curInteractGameobject.CompareTag("Water"))
                {
                    waterRemover.MoveWater(water);
                }
                if (curInteractGameobject.CompareTag("Case"))
                {
                    doorController.OpenRackCase(curInteractGameobject);
                }
            }
        }
    }
    IEnumerator DelayedInput()
    {
        canPress = false;

        yield return new WaitForSeconds(doorController.openSpeed);

        canPress = true;
    }

    private void SetPromptText(string text)
    {
        promptText.gameObject.SetActive(true);
        promptText.text = string.Format(text);
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

    //public void OnInteractInput(InputAction.CallbackContext callbackContext)
    //{
    //    if (callbackContext.phase == InputActionPhase.Started && curInteractable != null)
    //    {
    //        curInteractable.OnInteract();
    //        curInteractGameobject = null;
    //        curInteractable = null;
    //        promptText.gameObject.SetActive(false);
    //    }
    //}

    public void ChangeTall()
    {
        player.Controller.transform.localPosition += new Vector3(0,1f,0);
        if (isSmall == false)
        {
            TallToSmall();
        }
        else
        {
            TallToNormal();
        }
    }

    public void TallToSmall()
    {
        isSmall = true;
        player.Controller.transform.localScale = new Vector3(0.01f, 0.01f, 0.01f); //Scale을 0.01로
        player.Controller.stepOffset = 0.1f;
    }
    public void TallToNormal()
    {
        isSmall = false;
        player.Controller.transform.localScale = new Vector3(1f, 1f, 1f);      //Scale을 1로
        player.Controller.stepOffset = 0.3f;

    }
}
