using UnityEngine;
using System.Collections.Generic;


public class SW_KeypadEventUI : MonoBehaviour
{
    [Header("Objects to Activate/Deactivate")]
    [SerializeField] private List<GameObject> objectsToToggle = new List<GameObject>(); // 활성화/비활성화할 오브젝트 목록

    private void Awake()
    {
        // 기본적으로 오브젝트를 비활성화
        ToggleObjects(false);
    }

    // 인벤토리 암호 입력 성공 시 호출될 메서드
    public void ActivateObjects()
    {
        ToggleObjects(true); // 오브젝트 활성화
    }

    // 플레이어가 트리거 영역에 들어왔을 때 호출될 메서드
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            ToggleObjects(false); // 오브젝트 비활성화
            Destroy(this.gameObject); // 트리거 오브젝트 제거
        }
    }

    // 오브젝트 활성화/비활성화를 처리하는 메서드
    private void ToggleObjects(bool activate)
    {
        foreach (var obj in objectsToToggle)
        {
            if (obj != null)
            {
                obj.SetActive(activate);
            }
        }
    }
}


