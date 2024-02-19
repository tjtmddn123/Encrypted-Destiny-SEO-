using NavKeypad;

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

    [Header("Lamp")]
    private LampBtn lamp;

    [Header("Keypad")]
    private KeypadButton keypad;

    [Header("Interact")]
    [SerializeField] private float maxCheckDistance;    //최대 상호작용 가능 거리
    [SerializeField] private LayerMask layerMask;       //상호작용 시킬 레이어
    private float checkRate = 0.05f;
    private float lastCheckTime;
    public TextMeshProUGUI promptText;
    private GameObject curInteractGameobject;
    private IInteractable_HT curInteractable;
    private int i = 0;
    private SW_ItemObject itemObj; // 아이템의 데이터를 저장하는 변수
    private SubtitleManager subtitleManager;
    private SW_END END;
    /*
    [Header("ChangeTall")]
    private bool isSmall = false;    //작아졌는지 여부 확인을 위한 bool 입니다
    */

    [Header("Camera")]
    private Camera _camera;
    private NightVision nightVision;
    [SerializeField]
    private TextMeshProUGUI getText;
    private ChangeCinemachine cinemachine;
    public bool CameraChaging = false;

    [Header("outLine")]
    private Material material;
    private Renderer renderers;
    private List<Material> materialList = new List<Material>();

    void Start()
    {
        _camera = Camera.main;
        material = new Material(Shader.Find("Shader Graphs/Interact"));
        nightVision = _camera.GetComponent<NightVision>();
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
                if (curInteractGameobject != null)
                {
                    if (curInteractGameobject.tag == "Item")
                    {
                        Renderer renderer = curInteractGameobject.GetComponent<Renderer>();

                        materialList.Clear();
                        materialList.AddRange(renderer.sharedMaterials);
                        materialList.Remove(material);

                        renderer.materials = materialList.ToArray();
                    }
                }
                doorController = null;
                itemObj = null;
                curInteractable = null;
                keypad = null;
                cinemachine = null;
                lamp = null;
                promptText.gameObject.SetActive(false);
                curInteractGameobject = null;
                subtitleManager = null;
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
        if (CameraChaging == false)
        {
            switch (tag)
            {
                case "Item":
                    itemObj = curInteractGameobject.GetComponent<SW_ItemObject>();
                    curInteractable = lookThis.GetComponent<IInteractable_HT>();                    
                    SetPromptText($"[E] Take {itemObj.item.displayName}");

                    renderers = curInteractGameobject.GetComponent<Renderer>();
                    materialList.Clear();
                    materialList.AddRange(renderers.sharedMaterials);
                    materialList.Add(material);
                    if (materialList.Count >= 3)
                    {
                        materialList.RemoveAt(materialList.Count - 1);
                    }
                    renderers.materials = materialList.ToArray();

                    break;
                case "None":
                    SetPromptText("[E] Interact");
                    curInteractable = lookThis.GetComponent<IInteractable_HT>();
                    break;
                case "END":
                    SetPromptText("[E] Interact");
                    subtitleManager = lookThis.GetComponent<SubtitleManager>();
                    curInteractable = lookThis.GetComponent<IInteractable_HT>();
                    break;
                case "Finish":
                    SetPromptText("[E] Exit");
                    END = lookThis.GetComponent<SW_END>();
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
                    keypad = lookThis.GetComponent<KeypadButton>();
                    SetPromptText("[E] Push");
                    break;
                case "Case":
                    doorController = lookThis.GetComponent<DoorController>();
                    if (doorController.isReverse == false)
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
                    if (doorController.isReverse == true)
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
                    break;
                case "Lamp":
                    lamp = lookThis.GetComponent<LampBtn>();
                    SetPromptText("[E] Turn on");
                    break;
                case "CameraChanger":
                    cinemachine = curInteractGameobject.GetComponent<ChangeCinemachine>();
                    SetPromptText("[E] Interact");
                    break;
                default:
                    SetPromptText("");
                    doorController = null;
                    itemObj = null;
                    curInteractable = null;
                    keypad = null;
                    cinemachine = null;
                    lamp = null;
                    promptText.gameObject.SetActive(false);
                    curInteractGameobject = null;
                    subtitleManager = null;
                    break;
            }
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
            case "Finish":
                END.OnInteract();
                break;
            case "Case":
                nightVision.AddNightVisionItem();
                if(i == 0)
                {
                    StartCoroutine(TakeItem());
                    i = 1;
                }
                doorController.OpenRackCase(curInteractGameobject);
                break;
            case "END":
                SetPromptText("[E] Interact");
                subtitleManager.OnInteract();
                break;
            case "Lamp":
                lamp.ToggleLight();
                break;
            case "CameraChanger":
                cinemachine.CinemachineTest();
                break;
        }
    }

    private IEnumerator TakeItem()
    {
        getText.gameObject.SetActive(true);
        getText.text = string.Format("야간투시경을 주웠다. \n N을 눌러 사용 할 수 있다.");
        yield return new WaitForSeconds(2f);

        getText.gameObject.SetActive(false);
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
