using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DocumentObject_HT : MonoBehaviour
{
    public DocumentData document;

    public string GetInteractPrompt()
    {
        return string.Format("[E] Take");
    }

    public void OnInteract()
    {
        Inventory_HT.instance.AddItem(document);
        Destroy(gameObject);
    }
}
