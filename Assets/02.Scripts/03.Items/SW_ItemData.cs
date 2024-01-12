using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 아이템의 타입을 정의하는 열거형(enum)
public enum ItemType
{
    Important, // 중요한단서 용
    Unique,    // 유니크 아이템
    Normal     // 일반 아이템
}

// Unity 에디터에서 새로운 아이템을 생성할 수 있게 해주는 어트리뷰트
[CreateAssetMenu(fileName = "Item", menuName = "New Item")]
public class SW_ItemData : ScriptableObject
{
    [Header("Info")]
    public string displayName;   // 아이템의 표시 이름
    public string description;   // 아이템의 설명
    public ItemType type;        // 아이템의 타입 (Unique 혹은 Normal)
    public Sprite icon;          // 아이템의 아이콘 (UI에 표시될 이미지)
    public GameObject dropPrefab; // 게임 세계에 드롭될 때 사용되는 프리팹
}
