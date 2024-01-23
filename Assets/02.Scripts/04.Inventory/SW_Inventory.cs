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

    [Header("Events")]
    public UnityEvent onOpenInventory;   // 인벤토리 열기 이벤트
    public UnityEvent onCloseInventory;  // 인벤토리 닫기 이벤트

    public static SW_Inventory instance; // 싱글톤 인스턴스

    // 아이템 믹스 기능을 위한 변수 선언
    private ItemSlot mixingItem; // 현재 믹스를 위해 선택된 아이템

    private Canvas currentImportantItemCanvas; // 현재 활성화된 중요 아이템 캔버스
    private GameObject currentImportantItemUI; // 현재 활성화된 중요 아이템 UI

    // 조합 성공 및 실패 UI 오브젝트를 참조
    public GameObject mixSuccessUI;
    public GameObject mixFailedUI;
    public GameObject mixingUI; // Mixing UI 오브젝트 참조

    private SW_ItemSlotUI lastSelectedSlot; // 마지막으로 선택된 아이템 슬롯을 저장하는 변수

    private bool isMixing = false; // 조합 중인지 여부를 나타내는 변수

    private SW_ItemSlotUI firstMixSlotUI = null;


    void Awake()
    {
        // 클래스 인스턴스를 싱글톤으로 설정
        instance = this;
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
            ToggleCursor(false);

            ResetAllSlotColors(); // 모든 슬롯 색상 초기화

            if (mixingItem != null)
            {
                Debug.Log("조합 진행 중 인벤토리 닫음. 조합 진행 상황 초기화");
                mixingItem = null;
            }
            isMixing = false;

        }
        else
        {
            inventoryWindow.SetActive(true);
            onOpenInventory?.Invoke(); // 인벤토리 열기 이벤트 호출
            ToggleCursor(true);
        }
    }
    public void ToggleCursor(bool toggle)
    {
        Cursor.lockState = toggle ? CursorLockMode.None : CursorLockMode.Locked; // 커서 상태 변경
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

        // 새로운 슬롯 선택
        selectedItem = slots[index];
        selectedItemIndex = index;
        selectedItemName.text = selectedItem.item.displayName;
        selectedItemDescription.text = selectedItem.item.description;

        if (isMixing)
        {
            // 첫 번째 아이템이 이미 선택되어 있는 경우
            if (mixingItem != null && mixingItem == slots[index])
            {
                // 첫 번째 아이템을 다시 선택한 경우, 색상 변경 없이 리턴
                return;
            }
            else
            {
                // 두 번째 아이템 선택 시 이전 선택된 슬롯의 색상 초기화 (첫 번째 아이템 제외)
                if (lastSelectedSlot != null && lastSelectedSlot != firstMixSlotUI)
                {
                    lastSelectedSlot.ResetSlotColor();
                }

                // 두 번째 아이템의 색상을 노란색으로 변경
                lastSelectedSlot = uiSlots[index];
                lastSelectedSlot.SetSlotColor(Color.yellow);
            }
        }
        else
        {
            // 조합 과정이 아니면 이전 선택된 슬롯의 색상 초기화
            if (lastSelectedSlot != null)
            {
                lastSelectedSlot.ResetSlotColor();
            }

            // 새로운 슬롯의 색상을 노란색으로 변경
            lastSelectedSlot = uiSlots[index];
            lastSelectedSlot.SetSlotColor(Color.yellow);
        }

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
        // 현재 선택된 아이템이 null이 아니며, 아이템 타입이 ImportantMix 또는 Important인지 확인
        if (selectedItem != null && (selectedItem.item.type == ItemType.ImportantMix || selectedItem.item.type == ItemType.Important))
        {
            // 선택된 아이템의 importantItemCanvasPrefab과 importantItemDisplay가 null이 아닌지 확인
            if (selectedItem.item.importantItemCanvasPrefab != null && selectedItem.item.importantItemDisplay != null)
            {
                // 중요 아이템의 캔버스 프리팹을 인스턴스화하여 화면에 표시
                currentImportantItemCanvas = Instantiate(selectedItem.item.importantItemCanvasPrefab);

                // 중요 아이템의 UI를 캔버스의 자식으로 인스턴스화하여 화면에 표시
                currentImportantItemUI = Instantiate(selectedItem.item.importantItemDisplay, currentImportantItemCanvas.transform);

                // 인벤토리 창을 비활성화
                inventoryWindow.SetActive(false);
            }
        }
    }


    void Update()
    {
        // 사용자가 마우스 왼쪽 버튼을 클릭하고 중요 아이템 UI가 활성화된 경우
        if (Input.GetMouseButtonDown(0) && currentImportantItemCanvas != null)
        {
            Destroy(currentImportantItemCanvas.gameObject); // 중요 아이템 캔버스와 UI 제거
            currentImportantItemCanvas = null; // 참조 초기화
            currentImportantItemUI = null;
            inventoryWindow.SetActive(true); // 인벤토리 창 다시 열기
        }
    }


    public void OnMixButton()
    {
        if (selectedItem != null && (selectedItem.item.type == ItemType.Normal || selectedItem.item.type == ItemType.ImportantMix))
        {
            if (mixingItem == null)
            {
                // 첫 번째 아이템 선택
                mixingItem = selectedItem;
                Debug.Log("첫 번째 아이템 선택: " + mixingItem.item.displayName);
                isMixing = true; // 조합 과정 시작
                // 현재 선택된 아이템 슬롯의 색상을 붉은색으로 변경
                uiSlots[selectedItemIndex].SetSlotColor(Color.red);
                StartCoroutine(ShowAndHideUI(mixingUI)); // 조합중 UI 표시
            }
            else
            {
                // 두 번째 아이템 선택 시도
                Debug.Log("두 번째 아이템 선택 시도: " + selectedItem.item.displayName);
                var mixScript = mixingItem.item.dropPrefab.GetComponent<SW_ItemMix>();
                if (mixScript == null)
                {
                    Debug.Log("SW_ItemMix 컴포넌트를 찾을 수 없음");
                    ResetMixingProcess(); // 조합 진행 상황 초기화
                    return;
                }

                if (mixScript != null && mixScript.CanMix(new List<ItemSlot> { mixingItem, selectedItem }))
                {
                    // 조합 성공
                    AddItem(mixScript.resultItem);
                    RemoveItem(mixingItem);
                    RemoveSelectedItem();
                    ResetAllSlotColors();
                    StartCoroutine(ShowAndHideUI(mixSuccessUI)); // 조합 성공 UI 표시
                    isMixing = false; // 조합 과정 종료
                }
                else
                {
                    // 조합 실패
                    StartCoroutine(ShowAndHideUI(mixFailedUI)); // 조합 실패 UI 표시
                    Debug.Log("조합 실패");
                    ResetMixingProcess(); // 조합 진행 상황 초기화
                }
                mixingItem = null; // 믹스 대상 아이템 초기화
            }
        }
        else
        {
            Debug.Log("선택된 아이템이 없거나 아이템 타입이 Normal 또는 ImportantMix가 아님");
        }
    }

    // 지정된 UI를 표시하고 일정 시간 후에 숨기는 코루틴
    IEnumerator ShowAndHideUI(GameObject uiObject)
    {
        uiObject.SetActive(true); // UI 활성화
        yield return new WaitForSeconds(2f); // 2초 동안 기다림
        uiObject.SetActive(false); // UI 비활성화
    }



    // 아이템을 인벤토리에서 제거하는 메서드
    public void RemoveItem(ItemSlot itemSlot)
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

    private void ResetAllSlotColors()
    {
        // 모든 슬롯의 색상을 초기화하는 메서드
        foreach (var uiSlot in uiSlots)
        {
            uiSlot.ResetSlotColor();
        }
    }

    private void ResetMixingProcess()
    {
        mixingItem = null;
        ResetAllSlotColors(); // 모든 슬롯 색상 초기화
        isMixing = false;
        Debug.Log("조합 진행 상황 초기화");
    }
}



