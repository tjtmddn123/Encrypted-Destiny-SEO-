using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    public bool isOpening;
    public void OpenDoor(GameObject door)
    {
        Quaternion opening = door.transform.localRotation;
        if (isOpening)
        {
            opening *= Quaternion.Euler(0f, 90f, 0f);
        }
        else
        {
            opening *= Quaternion.Euler(0f, -90f, 0f);
        }
        door.transform.localRotation = opening;
        isOpening = !isOpening;
    }

}
