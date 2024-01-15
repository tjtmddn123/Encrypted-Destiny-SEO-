using System.Collections;

using UnityEngine;

public class DoorController : MonoBehaviour
{
    public bool isOpening;
    public float openSpeed = 1.5f; // 문이 열리는 속도

    public void OpenDoor(GameObject door)
    {
        Quaternion targetRotation = door.transform.localRotation;

        if (isOpening)
        {
            targetRotation *= Quaternion.Euler(0f, 90f, 0f);
        }
        else
        {
            targetRotation *= Quaternion.Euler(0f, -90f, 0f);
        }

        StartCoroutine(RotateDoor(door.transform, targetRotation));
        Invoke("ChangeOpenState", openSpeed/2);
    }

    private IEnumerator RotateDoor(Transform doorTransform, Quaternion targetRotation)
    {
        float elapsedTime = 0f;
        Quaternion initialRotation = doorTransform.localRotation;

        while (elapsedTime < 1f)
        {
            doorTransform.localRotation = Quaternion.Slerp(initialRotation, targetRotation, elapsedTime);
            elapsedTime += Time.deltaTime * openSpeed;
            yield return null;
        }

        doorTransform.localRotation = targetRotation;
    }
    private void ChangeOpenState()
    {
        isOpening = !isOpening;
    }
}
