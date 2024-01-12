using System.Collections;
using System.Collections.Generic;
using TMPro;

using UnityEngine;
using UnityEngine.UI;

public class DocumentItemSoltUI : MonoBehaviour
{
    public Button button;
    public TextMeshProUGUI quatityText;
    private DocumentSlot curSlot;
    private Outline outline;

    public int index;
    public bool equipped;

    private void Awake()
    {
        outline = GetComponent<Outline>();
    }

    private void OnEnable()
    {
        outline.enabled = equipped;
    }

    public void Set(DocumentSlot slot)
    {
        curSlot = slot;
        quatityText.text = slot.quantity > 1 ? slot.quantity.ToString() : string.Empty;

        if (outline != null)
        {
            outline.enabled = equipped;
        }
    }

    public void Clear()
    {
        curSlot = null;
        quatityText.text = string.Empty;
    }

    public void OnButtonClick()
    {
        Inventory_HT.instance.SelectItem(index);
    }
}
