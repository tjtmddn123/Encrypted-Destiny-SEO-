using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EquipmentType
{
    Equipment,
}


[System.Serializable]
public class Item
{
    public EquipmentType itemType;
    public string itemName;
    public Sprite itemImage;

    public bool Use()
    {
        return false;
    }
}
