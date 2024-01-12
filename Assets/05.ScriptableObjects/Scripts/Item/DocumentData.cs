using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public enum ItemType
{
    Ducument
}

[CreateAssetMenu(fileName = "Document", menuName = "New Document")]
public class DocumentData : ScriptableObject
{
    [Header("Info")]
    public string docuTitle;
    public string description;
    public ItemType type;

    [Header("Stacking")]
    public int maxStackAmount;

}