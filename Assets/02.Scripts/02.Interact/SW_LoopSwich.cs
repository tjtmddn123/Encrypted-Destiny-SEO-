using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SW_LoopSwitch : MonoBehaviour, IInteractable_HT
{
    [SerializeField]
    private List<GameObject> loopPointObjects; // 루프 지점 오브젝트 리스트

    private bool isInteractable = true; // 상호작용 가능 여부

    public void OnInteract()
    {
        if (isInteractable)
        {
            foreach (var loopPointObject in loopPointObjects)
            {
                if (loopPointObject != null)
                {
                    Destroy(loopPointObject); // 각 루프 지점 오브젝트 제거
                }
            }
            isInteractable = false; // 상호작용 비활성화
            Debug.Log("모든 루프 지점 오브젝트 제거 완료");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            foreach (var loopPointObject in loopPointObjects)
            {
                if (loopPointObject != null)
                {
                    Destroy(loopPointObject); // 각 루프 지점 오브젝트 제거
                }
            }
        }
    }
}


