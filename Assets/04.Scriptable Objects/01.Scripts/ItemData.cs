using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    Equipable,
}

[System.Serializable]
public class ItemDataConsumable
{
    public float value;
}

[CreateAssetMenu(fileName = "Item", menuName = "New Item")]


public class ItemData : ScriptableObject
{
    [Header("Info")]
    public string displayName;
    public string description;
    public ItemType type;
    public Sprite icon;
    public GameObject dropPrefab;
}
