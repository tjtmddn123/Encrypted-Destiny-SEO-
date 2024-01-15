using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.XR;
using static UnityEditor.Progress;

public class ItemSlot
{
    public SW_ItemData item;
    public int quantity;
}


public class SW_Inventory : MonoBehaviour
{
    public SW_ItemSlotUI[] uiSlots; // 인벤토리 UI 슬롯 배열
    public ItemSlot[] slots;     // 아이템 슬롯 배열

    public GameObject inventoryWindow; // 인벤토리 윈도우 UI
    public Transform dropPosition;     // 아이템을 버릴 때 위치

    [Header("Selected Item")]
    private ItemSlot selectedItem;     // 현재 선택된 아이템
    private int selectedItemIndex;     // 선택된 아이템의 인덱스
    public TextMeshProUGUI selectedItemName; // 선택된 아이템 이름 UI
    public TextMeshProUGUI selectedItemDescription; // 선택된 아이템 설명 UI

    public GameObject useButton;       // 사용 버튼 UI
    public GameObject mixButton;       // 믹스(조합) 버튼 UI
    public GameObject dropButton;      // 버리기 버튼 UI

    private SW_PlayerController controller;   // 플레이어 컨트롤러

    [Header("Events")]
    public UnityEvent onOpenInventory;   // 인벤토리 열기 이벤트
    public UnityEvent onCloseInventory;  // 인벤토리 닫기 이벤트

    public static SW_Inventory instance; // 싱글톤 인스턴스

    // 아이템 믹스 기능을 위한 변수 선언
    private ItemSlot mixingItem; // 현재 믹스를 위해 선택된 아이템

    void Awake()
    {
        // 클래스 인스턴스를 싱글톤으로 설정
        instance = this;
        controller = GetComponent<SW_PlayerController>();
    }

    private void Start()
    {
        // 게임 시작 시 인벤토리 UI를 숨기고 각 슬롯 초기화
        inventoryWindow.SetActive(false);
        slots = new ItemSlot[uiSlots.Length];
        for (int i = 0; i < slots.Length; i++)
        {
            slots[i] = new ItemSlot();
            uiSlots[i].index = i;
            uiSlots[i].Clear();
        }
        ClearSelectedItemWindow();
    }

    public void OnInventoryButton(InputAction.CallbackContext callbackContext)
    {
        // 인벤토리 버튼 입력 처리. 시작 단계에서만 인벤토리 토글
        if (callbackContext.phase == InputActionPhase.Started)
            Toggle();
    }

    public void Toggle()
    {
        // 인벤토리 창 상태 변경. 창이 열려 있으면 닫고, 닫혀 있으면 염
        if (inventoryWindow.activeInHierarchy)
        {
            inventoryWindow.SetActive(false);
            onCloseInventory?.Invoke(); // 인벤토리 닫기 이벤트 호출
            controller.ToggleCursor(false);
        }
        else
        {
            inventoryWindow.SetActive(true);
            onOpenInventory?.Invoke(); // 인벤토리 열기 이벤트 호출
            controller.ToggleCursor(true);
        }
    }

    public bool IsOpen()
    {
        // 인벤토리 창 열림 상태 반환
        return inventoryWindow.activeInHierarchy;
    }

    public void AddItem(SW_ItemData item)
    {
        // 새 아이템 인벤토리에 추가. 빈 슬롯이 있으면 거기에 추가, 없으면 아이템 버림
        ItemSlot emptySlot = GetEmptySlot();
        if (emptySlot != null)
        {
            emptySlot.item = item;
            UpdateUI();
        }
        else
            ThrowItem(item);
    }

    void ThrowItem(SW_ItemData item)
    {
        // 아이템 게임 세계에 생성. 아이템의 드롭 프리팹 인스턴스화
        Instantiate(item.dropPrefab, dropPosition.position, Quaternion.Euler(Vector3.one * Random.value * 360f));
    }

    void UpdateUI()
    {
        // 인벤토리 UI 업데이트. 각 슬롯의 상태를 반영
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].item != null)
                uiSlots[i].Set(slots[i]);
            else
                uiSlots[i].Clear();
        }
    }

    ItemSlot GetEmptySlot()
    {
        // 빈 인벤토리 슬롯 검색 및 반환
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].item == null)
                return slots[i];
        }
        return null;
    }

    public void SelectItem(int index)
    {
        // 선택된 아이템 인덱스에 따라 아이템 정보 업데이트 및 UI에 표시
        if (slots[index].item == null)
            return;

        selectedItem = slots[index];
        selectedItemIndex = index;
        selectedItemName.text = selectedItem.item.displayName;
        selectedItemDescription.text = selectedItem.item.description;

        // 아이템 타입에 따른 버튼 활성화
        switch (selectedItem.item.type)
        {
            case ItemType.Normal:
                // 노멀 아이템: 드랍 및 믹스 버튼 활성화
                useButton.SetActive(false);
                mixButton.SetActive(true);
                dropButton.SetActive(true);
                break;
            case ItemType.Unique:
                // 유니크 아이템: 드랍 버튼만 활성화
                useButton.SetActive(false);
                mixButton.SetActive(false);
                dropButton.SetActive(true);
                break;
            case ItemType.ImportantMix:
                // 중요한단서 용 아이템 조합형: 유즈 및 믹스 버튼 활성화
                useButton.SetActive(true);
                mixButton.SetActive(true);
                dropButton.SetActive(false);
                break;
            case ItemType.Important:
                // 중요한단서 용 아이템: 유즈 버튼만 활성화
                useButton.SetActive(true);
                mixButton.SetActive(false);
                dropButton.SetActive(false);
                break;
            default:
                // 기본 설정: 모든 버튼 비활성화
                useButton.SetActive(false);
                mixButton.SetActive(false);
                dropButton.SetActive(false);
                break;
        }
    }

    private void ClearSelectedItemWindow()
    {
        // 선택된 아이템 정보 초기화 및 UI 갱신
        selectedItem = null;
        selectedItemName.text = string.Empty;
        selectedItemDescription.text = string.Empty;
        useButton.SetActive(false);
        mixButton.SetActive(false);
        dropButton.SetActive(false);
    }

    public void OnUseButton()
    {
        // Important 타입 아이템일 경우 소모되지 않음. 나머지 아이템은 사용 시 제거
        if (selectedItem.item.type != ItemType.Important)
            RemoveSelectedItem();
    }

    public void OnMixButton()
    {
        if (selectedItem != null && selectedItem.item.type == ItemType.Normal)
        {
            if (mixingItem == null)
            {
                // 첫 번째 아이템 선택 시
                mixingItem = selectedItem;
                Debug.Log("첫 번째 아이템 선택: " + mixingItem.item.displayName);
            }
            else
            {
                // 두 번째 아이템 선택 시
                Debug.Log("두 번째 아이템 선택 시도: " + selectedItem.item.displayName);
                var mixScript = mixingItem.item.dropPrefab.GetComponent<SW_ItemMix>();
                if (mixScript == null)
                {
                    Debug.Log("SW_ItemMix 컴포넌트를 찾을 수 없음");
                    return;
                }

                if (mixScript != null && mixScript.CanMix(new List<ItemSlot> { mixingItem, selectedItem }))
                {
                    // 조합 성공, 결과 아이템 생성 및 인벤토리에 추가
                    AddItem(mixScript.resultItem);
                    RemoveItem(mixingItem);
                    RemoveSelectedItem();
                    Debug.Log("아이템 조합 완료: " + mixScript.resultItem.displayName);
                }
                else
                {
                    // 조합 실패
                    Debug.Log("조합 실패");
                }
                mixingItem = null; // 믹스 대상 아이템 초기화
            }
        }
        else
        {
            Debug.Log("선택된 아이템이 없거나 아이템 타입이 Normal이 아님");
        }
    }


    // 아이템을 인벤토리에서 제거하는 메서드
    private void RemoveItem(ItemSlot itemSlot)
    {
        if (itemSlot == null || itemSlot.item == null) return;
        itemSlot.item = null;
        UpdateUI(); // 인벤토리 UI 업데이트
    }

    public void OnDropButton()
    {
        // 아이템 버리기 기능. 선택된 아이템 게임 세계에 생성 및 인벤토리에서 제거
        ThrowItem(selectedItem.item);
        RemoveSelectedItem();
    }

    private void RemoveSelectedItem()
    {
        // 선택된 아이템 제거 및 UI 갱신
        selectedItem.item = null;
        ClearSelectedItemWindow();
        UpdateUI();
    }
}



