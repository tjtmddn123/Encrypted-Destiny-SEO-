using System.Collections;
using System.Collections.Generic;
using System.Reflection;

using TMPro;

using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

using static UnityEditor.Progress;

public class DocumentSlot
{
    public DocumentData document;
    public int quantity;
}

public class Inventory_HT : MonoBehaviour
{
    public DocumentItemSoltUI[] uiSlots;
    public DocumentSlot[] slots;

    public GameObject inventoryWindow;

    [Header("Selected Item")]
    private DocumentSlot selectedItem;
    private int selectedItemIndex;
    public TextMeshProUGUI selectedItemName;
    public TextMeshProUGUI selectedItemDescription;
    public TextMeshProUGUI selectedItemStatNames;
    public TextMeshProUGUI selectedItemStatValues;
    public GameObject useButton;
    public GameObject equipButton;
    public GameObject unEquipButton;
    public GameObject dropButton;


    [Header("Events")]
    public UnityEvent onOpenInventory;
    public UnityEvent onCloseInventory;

    public static Inventory_HT instance;
    void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        inventoryWindow.SetActive(false);
        slots = new DocumentSlot[uiSlots.Length];

        for (int i = 0; i < slots.Length; i++)
        {
            slots[i] = new DocumentSlot();
            uiSlots[i].index = i;
            uiSlots[i].Clear();
        }

        ClearSeletecItemWindow();
    }

    public void OnInventoryButton(InputAction.CallbackContext callbackContext)
    {
        if (callbackContext.phase == InputActionPhase.Started)
        {
            Toggle();
        }
    }


    public void Toggle()
    {
        if (inventoryWindow.activeInHierarchy)
        {
            inventoryWindow.SetActive(false);
            onCloseInventory?.Invoke();
        }
        else
        {
            inventoryWindow.SetActive(true);
            onOpenInventory?.Invoke();
        }
    }

    public bool IsOpen()
    {
        return inventoryWindow.activeInHierarchy;
    }

    public void AddItem(DocumentData item)
    {
        DocumentSlot emptySlot = GetEmptySlot();

        if (emptySlot != null)
        {
            emptySlot.document = item;
            emptySlot.quantity = 1;
            UpdateUI();
            return;
        }
    }

    void UpdateUI()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].document != null)
                uiSlots[i].Set(slots[i]);
            else
                uiSlots[i].Clear();
        }
    }

    DocumentSlot GetItemStack(DocumentData docu)
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].document == docu && slots[i].quantity < docu.maxStackAmount)
                return slots[i];
        }

        return null;
    }

    DocumentSlot GetEmptySlot()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].document == null)
                return slots[i];
        }

        return null;
    }

    public void SelectItem(int index)
    {
        if (slots[index].document == null)
            return;

        selectedItem = slots[index];
        selectedItemIndex = index;

        selectedItemName.text = selectedItem.document.docuTitle;
        selectedItemDescription.text = selectedItem.document.description;

        selectedItemStatNames.text = string.Empty;
        selectedItemStatValues.text = string.Empty;

    }

    private void ClearSeletecItemWindow()
    {
        selectedItem = null;
        selectedItemName.text = string.Empty;
        selectedItemDescription.text = string.Empty;

        selectedItemStatNames.text = string.Empty;
        selectedItemStatValues.text = string.Empty;

        useButton.SetActive(false);
        equipButton.SetActive(false);
        unEquipButton.SetActive(false);
        dropButton.SetActive(false);
    }


    public bool HasItems(DocumentData docu, int quantity)
    {
        return false;
    }
}