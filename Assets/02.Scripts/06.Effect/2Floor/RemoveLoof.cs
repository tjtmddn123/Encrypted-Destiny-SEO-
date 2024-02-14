using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveLoof : MonoBehaviour
{
    public GameObject Loop;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Loop.SetActive(false);
        }
    }
}
