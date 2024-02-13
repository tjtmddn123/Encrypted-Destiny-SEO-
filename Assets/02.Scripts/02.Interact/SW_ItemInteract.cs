using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;


public class SW_ItemInteract : MonoBehaviour, IInteractable_HT
{
    public SW_ItemData requiredItemData; // 필요한 아이템 데이터
    public GameObject actionUI; // 성공 표시할 UI 오브젝트
    public GameObject failUI; // 실패 표시할 UI 오브젝트
    [SerializeField]
    private TMPro.TextMeshProUGUI failText;
    [SerializeField]
    private string Info;
    public string newTag; // 상호작용 후 변경할 태그
    public void OnInteract()
    {
        Interact();
    }

    private void Interact()
    {
        // 플레이어의 인벤토리 데이터 가져오기
        SW_Inventory inventory = SW_Inventory.instance;

        // 인벤토리에서 필요한 아이템을 찾기
        ItemSlot foundItem = FindItemInInventory(inventory, requiredItemData.displayName);

        // 아이템이 존재하면 제거하고 필요한 동작 수행
        if (foundItem != null)
        {
            Debug.Log($"필요한 아이템 '{requiredItemData.displayName}'을(를) 찾았습니다.");

            // 필요한 동작 수행 (예: 문 열기, 기계 작동 등)
            PerformRequiredAction();

            // 아이템 타입을 Normal로 변경
            foundItem.item.type = ItemType.Normal; // 아이템 타입 변경 로직 추가
            Debug.Log($"아이템 타입을 {ItemType.Normal}로 변경했습니다.");

            if (actionUI != null)
            {
                // 성공 UI 표시
                StartCoroutine(ShowAndHideUI(actionUI, 1.5f));
            }
        }
        else
        {
            StartCoroutine(ShowAndHideUI(failUI, 1.5f));
        }
    }

    private ItemSlot FindItemInInventory(SW_Inventory inventory, string displayName)
    {
        foreach (var slot in inventory.slots)
        {
            if (slot.item != null && slot.item.displayName == displayName)
            {
                return slot;
            }
        }
        return null; // 해당 아이템이 없으면 null 반환
    }

    private void PerformRequiredAction()
    {
        // 필요한 동작을 여기에 구현
        // 예: 문을 열거나, 특정 메커니즘을 활성화하는 등
        Debug.Log("필요한 동작을 수행합니다.");

        // 실제 GameObject의 태그 변경
        gameObject.tag = newTag;
        Debug.Log($"GameObject의 태그가 '{newTag}'로 변경되었습니다.");
    }

    IEnumerator ShowAndHideUI(GameObject uiObject, float delay)
    {
        if (uiObject != null)
        {
            uiObject.SetActive(true);
            yield return new WaitForSeconds(delay);
            uiObject.SetActive(false);
        }
        else
        {
            failText.gameObject.SetActive(true);
            failText.text = string.Format(Info);
            yield return new WaitForSeconds(delay);
            failText.gameObject.SetActive(false);
            failText.text = string.Format("");
        }
    }
}




