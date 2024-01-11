using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

// 상호작용 가능한 객체를 위한 인터페이스
public interface IInteractable
{
    string GetInteractPrompt(); // 상호작용 프롬프트 텍스트를 얻기 위한 메소드
    void OnInteract();          // 상호작용이 발생했을 때 수행할 메소드
}

public class SW_InteractionManager : MonoBehaviour
{
    public float checkRate = 0.05f; // 상호작용 체크 빈도
    private float lastCheckTime;    // 마지막으로 체크한 시간
    public float maxCheckDistance;  // 상호작용 체크 최대 거리
    public LayerMask layerMask;     // 상호작용 가능한 레이어 마스크

    private GameObject curInteractGameobject; // 현재 상호작용 중인 게임 오브젝트
    private IInteractable curInteractable;    // 현재 상호작용 중인 인터페이스

    public TextMeshProUGUI promptText; // 상호작용 프롬프트 UI 텍스트
    private Camera camera;            // 카메라 참조

    // 첫 프레임 전에 호출되는 Start 메소드
    void Start()
    {
        camera = Camera.main; // 메인 카메라를 가져옴
    }

    // 매 프레임마다 호출되는 Update 메소드
    void Update()
    {
        // 체크 빈도에 따라 상호작용 가능한 객체 체크
        if (Time.time - lastCheckTime > checkRate)
        {
            lastCheckTime = Time.time;

            // 카메라 중앙에서 레이캐스트를 발사
            Ray ray = camera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2));
            RaycastHit hit;

            // 레이캐스트에 충돌한 오브젝트가 있는지 확인
            if (Physics.Raycast(ray, out hit, maxCheckDistance, layerMask))
            {
                // 새로운 상호작용 가능한 오브젝트를 감지했을 때
                if (hit.collider.gameObject != curInteractGameobject)
                {
                    curInteractGameobject = hit.collider.gameObject;
                    curInteractable = hit.collider.GetComponent<IInteractable>();
                    SetPromptText(); // 상호작용 프롬프트 설정
                }
            }
            else
            {
                // 상호작용 가능한 오브젝트가 없을 때
                curInteractGameobject = null;
                curInteractable = null;
                promptText.gameObject.SetActive(false); // 프롬프트 숨김
            }
        }
    }

    // 상호작용 프롬프트 텍스트 설정 메소드
    private void SetPromptText()
    {
        promptText.gameObject.SetActive(true);
        promptText.text = string.Format("<b>[E]</b> {0}", curInteractable.GetInteractPrompt());
    }

    // 상호작용 입력이 들어왔을 때 호출되는 메소드
    public void OnInteractInput(InputAction.CallbackContext callbackContext)
    {
        // 상호작용 입력이 시작되고 현재 상호작용 가능한 오브젝트가 있을 때
        if (callbackContext.phase == InputActionPhase.Started && curInteractable != null)
        {
            curInteractable.OnInteract(); // 상호작용 수행
            curInteractGameobject = null;
            curInteractable = null;
            promptText.gameObject.SetActive(false); // 프롬프트 숨김
        }
    }
}

