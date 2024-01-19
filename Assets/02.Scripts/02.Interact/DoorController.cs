using System.Collections;

using UnityEngine;

public class DoorController : MonoBehaviour
{
    public bool isOpening;
    public float openSpeed = 1.5f; // 문이 열리는 속도
    public bool canOpenState = false;
    public bool isReverse = false;

    public void OpenDoor(GameObject door)
    {
        if (canOpenState == true)
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
            ChangeOpenState();
        }
    }

    private IEnumerator RotateDoor(Transform doorTransform, Quaternion targetRotation)
    {
        float elapsedTime = 0f;
        Quaternion initialRotation = doorTransform.localRotation;

        while (elapsedTime < openSpeed)
        {
            doorTransform.localRotation = Quaternion.Slerp(initialRotation, targetRotation, elapsedTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        doorTransform.localRotation = targetRotation;
    }
    private void ChangeOpenState()
    {
        isOpening = !isOpening;
    }

    public void CanOpen()
    {
        canOpenState = true;
    }
}
