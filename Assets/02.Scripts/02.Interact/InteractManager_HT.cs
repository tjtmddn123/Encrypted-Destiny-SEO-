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
    private DoorController doorController;
    private WaterRemover waterRemover;
    private Player_HT player;
    public float checkRate = 0.05f;
    private float lastCheckTime;
    public float maxCheckDistance;
    public LayerMask layerMask;
    private bool canPress = true;
    private bool canRemove = true;

    private bool isSmall = false;    //작아졌는지 여부 확인을 위한 bool 입니다
    public GameObject btn;
    public GameObject water;

    private GameObject curInteractGameobject;
    private IInteractable_HT curInteractable;

    public TextMeshProUGUI promptText;
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
                    SetPromptTextTake();
                }
                else if (hit.collider.gameObject != curInteractGameobject && hit.collider.CompareTag("Door"))
                {
                    curInteractGameobject = hit.collider.gameObject;
                    SetPromptTextDoor();
                    Debug.Log("문을 바라봐");
                }
                waterRemover = curInteractGameobject.GetComponent<WaterRemover>();
                doorController = curInteractGameobject.GetComponent<DoorController>();
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
            if (Input.GetKeyDown(KeyCode.E) && curInteractGameobject.CompareTag("Door") && canPress)
            {
                Debug.Log("열림");
                doorController.OpenDoor(curInteractGameobject);
                StartCoroutine(DelayedInput());
            }

            if (Input.GetKeyDown(KeyCode.Q) && curInteractGameobject == btn)
            {
                ChangeTall();
            }

            if (Input.GetKeyDown(KeyCode.Q) && curInteractGameobject == water && canRemove == true)
            {
                canRemove = false;
                waterRemover.WaterRemove();
            }
        }
    }
    IEnumerator DelayedInput()
    {
        canPress = false;

        yield return new WaitForSeconds(doorController.openSpeed);

        canPress = true;
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

    public void ChangeTall()
    {
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
        player.Controller.transform.localPosition += new Vector3(0, 0.1f, 0);  //커질 때 땅 속으로 들어가는 문제 해결을 위한 코드
        player.Controller.transform.localScale = new Vector3(1f, 1f, 1f);      //Scale을 1로
        player.Controller.stepOffset = 0.3f;

    }
}
