using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine;

public class SW_ItemRead : MonoBehaviour, IInteractable_HT
{
    public GameObject imageUI; // 이미지 UI에 대한 참조

    public void OnInteract()
    {
        // 상호작용 시 이미지 UI 활성화
        if (imageUI != null)
        {
            imageUI.SetActive(true);
        }
    }

    void Update()
    {
        // 사용자가 배경을 클릭하면 이미지 UI 비활성화
        if (Input.GetMouseButtonDown(0) && imageUI != null && !RectTransformUtility.RectangleContainsScreenPoint(
            imageUI.GetComponent<RectTransform>(), Input.mousePosition, Camera.main))
        {
            imageUI.SetActive(false);
        }
    }
}
