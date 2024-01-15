using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

// 인벤토리 시스템 내의 아이템 슬롯 UI를 나타내는 클래스
public class SW_ItemSlotUI : MonoBehaviour
{
    public Button button;          // 아이템 슬롯의 버튼 컴포넌트
    public Image icon;             // 아이템 아이콘을 표시할 이미지 컴포넌트
    private ItemSlot curSlot;      // 현재 슬롯에 할당된 ItemSlot 오브젝트

    public int index;              // 아이템 슬롯의 인덱스

    // 객체가 활성화될 때 실행되는 메서드
    private void Awake()
    {
        // 버튼 컴포넌트에 클릭 이벤트 리스너 추가
        button.onClick.AddListener(OnButtonClick);
    }
 

    // 아이템 슬롯을 설정하는 메서드
    public void Set(ItemSlot slot)
    {
        // 현재 이 UI 요소에 표시할 ItemSlot 객체를 할당.
        curSlot = slot;

        // 아이템의 아이콘을 UI에 활성화하고 설정.
        // icon.gameObject.SetActive(true)는 아이템 아이콘을 화면에 보이게 함
        icon.gameObject.SetActive(true);

        // icon.sprite = slot.item.icon은 해당 ItemSlot 객체의 아이템 데이터에서 아이콘 스프라이트를 가져와서 설정.
        // 이 스프라이트는 인벤토리 UI에서 아이템을 시각적으로 나타내는 데 사용
        icon.sprite = slot.item.icon;
    }


    // 아이템 슬롯을 비우는 메서드
    public void Clear()
    {
        curSlot = null;             // 현재 슬롯 초기화
        icon.gameObject.SetActive(false);  // 아이콘 비활성화
    }

    // 버튼 클릭 이벤트 처리 메서드
    public void OnButtonClick()
    {
        SW_Inventory.instance.SelectItem(index);  // 인벤토리에서 현재 인덱스의 아이템 선택
    }
}
