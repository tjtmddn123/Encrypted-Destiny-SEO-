using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SW_LightController : MonoBehaviour, IInteractable_HT
{
    public List<GameObject> lightsOn; // 켜져 있는 라이트 오브젝트 목록
    public List<GameObject> lightsOff; // 꺼져 있는 라이트 오브젝트 목록

    // IInteractable_HT 인터페이스의 OnInteract 메서드 구현
    public void OnInteract()
    {
        Interact();
    }

    // 실제 상호작용 로직을 처리하는 메서드
    private void Interact()
    {
        // 꺼져 있는 오브젝트들을 켜짐 상태로 전환
        foreach (var obj in lightsOff)
        {
            obj.SetActive(true);
        }

        // 켜져 있는 오브젝트들을 꺼짐 상태로 전환
        foreach (var obj in lightsOn)
        {
            obj.SetActive(false);
        }
    }
}


