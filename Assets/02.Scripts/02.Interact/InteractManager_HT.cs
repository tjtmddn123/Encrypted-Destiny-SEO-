using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public interface IInteractable_HT
{
    void OnInteract();
}
public class InteractManager_HT : MonoBehaviour
{

    [Header("Door")]
    private DoorController doorController;

    [Header("Lamp")]
    private LampBtn lamp;

    [Header("Keypad")]
    private NavKeypad.KeypadButton keypad;

    [Header("Interact")]
    [SerializeField] private float maxCheckDistance;    //최대 상호작용 가능 거리
    [SerializeField] private LayerMask layerMask;       //상호작용 시킬 레이어
    private float checkRate = 0.05f;
    private float lastCheckTime;
    public TextMeshProUGUI promptText;
    private GameObject curInteractGameobject;
    private IInteractable_HT curInteractable;

    /*
    [Header("ChangeTall")]
    private bool isSmall = false;    //작아졌는지 여부 확인을 위한 bool 입니다
    */


    [Header("Camera")]
    private Camera _camera;

    [Header("outLine")]
    private Material material;
    private Renderer renderers;
    private List<Material> materialList = new List<Material>();

    void Start()
    {
        _camera = Camera.main;
        material = new Material(Shader.Find("Shader Graphs/Interact"));
    }

    void Update()
    {

        if (Time.time - lastCheckTime > checkRate)
        {
            lastCheckTime = Time.time;
            Ray ray = _camera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2));
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, maxCheckDistance, layerMask))
            {
                if (hit.collider.gameObject != curInteractGameobject)
                {
                    curInteractGameobject = hit.collider.gameObject;
                    LookAtThis(curInteractGameobject.tag, curInteractGameobject);
                }   
            }
            else
            {
                if (curInteractGameobject != null && curInteractGameobject.CompareTag("Item"))
                {
                    Renderer renderer = curInteractGameobject.GetComponent<Renderer>();

                    materialList.Clear();
                    materialList.AddRange(renderer.sharedMaterials);
                    materialList.Remove(material);

                    renderer.materials = materialList.ToArray();
                }
                curInteractGameobject = null;
                curInteractable = null;
                promptText.gameObject.SetActive(false);
            }
        }
        if (curInteractGameobject != null)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                Interact(curInteractGameobject.tag);
            }
        }
    }

    private void LookAtThis(string tag, GameObject lookThis)
    {
        
        switch (tag)
        {
            case "Item":
                SetPromptText("[E] Take");
                curInteractable = lookThis.GetComponent<IInteractable_HT>();
                renderers = curInteractGameobject.GetComponent<Renderer>();

                materialList.Clear();
                materialList.AddRange(renderers.sharedMaterials);
                materialList.Add(material);

                renderers.materials = materialList.ToArray();
                break;
            case "None":
                SetPromptText("[E] Use");
                curInteractable = lookThis.GetComponent<IInteractable_HT>();
                break;
            case "Door":
                doorController = lookThis.GetComponent<DoorController>();
                if (doorController.isReverse == false)
                {
                    if (doorController.isOpen == false)
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
                    if (doorController.isOpen == false)
                    {
                        SetPromptText("[E] Close");
                    }
                    else
                    {
                        SetPromptText("[E] Open");
                    }
                }
                break;
            case "Button":
                keypad = lookThis.GetComponent<NavKeypad.KeypadButton>();
                SetPromptText("[E] Push");
                break;
            case "Case":
                doorController = lookThis.GetComponent<DoorController>();
                if (doorController.isOpen == false)
                {
                    SetPromptText("[E] Open");
                }
                else
                {
                    SetPromptText("[E] Close");
                }
                break;
            case "Lamp":
                lamp = lookThis.GetComponent<LampBtn>();
                SetPromptText("[E] Turn on");
                break;
        }
    }

    private void Interact(string tag)
    {
        switch (tag)
        {
            case "Door":
                doorController.OpenDoor(curInteractGameobject);                
                break;
            case "Button":
                keypad.PressButton();
                break;
            case "Case":
                doorController.OpenRackCase(curInteractGameobject);
                break;
            case "Lamp":
                lamp.ToggleLight();
                break;
        }
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
    //public void ChangeTall()
    //{
    //    player.Controller.transform.localPosition += new Vector3(0,1f,0);
    //    if (isSmall == false)
    //    {
    //        TallToSmall();
    //    }
    //    else
    //    {
    //        TallToNormal();
    //    }
    //}

    //public void TallToSmall()
    //{
    //    isSmall = true;
    //    player.Controller.transform.localScale = new Vector3(0.01f, 0.01f, 0.01f); //Scale을 0.01로
    //    player.Controller.stepOffset = 0.1f;
    //}
    //public void TallToNormal()
    //{
    //    isSmall = false;
    //    player.Controller.transform.localScale = new Vector3(1f, 1f, 1f);      //Scale을 1로
    //    player.Controller.stepOffset = 0.3f;

    //}
}
