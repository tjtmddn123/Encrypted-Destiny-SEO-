using System.Collections.Generic;
using UnityEngine;

public class SW_ItemDismantle : MonoBehaviour
{
    public SW_ItemData requiredItem_D; // 분해에 필요한 아이템
    public List<SW_ItemData> resultItems_D; // 분해 결과로 얻게 될 아이템 목록

    // 분해 가능 여부를 판단하는 메서드
    public bool CanDismantle(List<ItemSlot> inventorySlots)
    {
        Debug.Log("CanDismantle 검사 시작");
        // 인벤토리에서 필요한 아이템 찾기
        foreach (var slot in inventorySlots)
        {
            if (slot.item != null && slot.item.displayName == requiredItem_D.displayName)
            {
                Debug.Log("분해할 아이템 있음: " + requiredItem_D.displayName);
                // 분해할 아이템을 찾았다면, true 반환
                return true;
            }
        }

        // 필요한 아이템을 찾지 못했다면, false 반환
        Debug.Log("분해할 아이템 없음: " + requiredItem_D.displayName);
        return false;
    }
}

