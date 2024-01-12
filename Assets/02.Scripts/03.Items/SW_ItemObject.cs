using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 아이템 오브젝트를 나타내는 클래스, 상호작용 가능한 인터페이스(IInteractable)를 구현
public class SW_ItemObject : MonoBehaviour, IInteractable
{
    public SW_ItemData item; // 아이템의 데이터를 저장하는 변수

    // 상호작용 시 나타날 텍스트를 반환하는 함수
    public string GetInteractPrompt()
    {
        // 아이템의 이름을 포함한 텍스트 반환
        return string.Format("Pickup {0}", item.displayName);
    }

    // 아이템과 상호작용할 때 실행되는 함수
    public void OnInteract()
    {
        SW_Inventory.instance.AddItem(item); // 인벤토리에 아이템 추가
        Destroy(gameObject); // 상호작용 후 아이템 오브젝트 제거
    }
}
