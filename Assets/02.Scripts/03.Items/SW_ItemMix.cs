using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SW_ItemMix : MonoBehaviour
{
    public List<SW_ItemData> requiredItems; // 조합에 필요한 아이템 목록
    public SW_ItemData resultItem; // 조합 결과로 얻게 될 아이템

    // 조합 가능 여부를 판단하는 메서드
    public bool CanMix(List<ItemSlot> inventorySlots)
    {
        Debug.Log("CanMix 검사 시작");
        foreach (var requiredItem in requiredItems)
        {
            bool found = false;
            foreach (var slot in inventorySlots)
            {
                if (slot.item != null && slot.item.displayName == requiredItem.displayName)
                {
                    found = true;
                    Debug.Log("필요한 아이템 발견: " + requiredItem.displayName);
                    break;
                }
            }
            if (!found)
            {
                Debug.Log("필요한 아이템 없음: " + requiredItem.displayName);
                return false; // 필요한 아이템이 하나라도 없으면 false 반환
            }
        }
        Debug.Log("모든 필요 아이템이 인벤토리에 있음");
        return true; // 모든 필요 아이템이 인벤토리에 있으면 true 반환
    }
}