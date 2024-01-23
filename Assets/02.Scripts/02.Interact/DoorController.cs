using System.Collections;

using UnityEngine;

public class DoorController : MonoBehaviour
{
    public bool isOpening;      //열려있는 문인지를 판단하기 위한 bool
    public bool isReverse = false;  //반대로 열리는 문이 있으면 true로 해주세요
    public float openSpeed = 1.5f; // 문이 열리는 속도

    [SerializeField]
    private float moveRange = 0.35f; //서랍 이동 거리입니다.
    [SerializeField]
    private bool canOpenState = false;   //열수 있는지 구분 

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

    public void OpenRackCase(GameObject rackCase)
    {
        if (isOpening)
        {
            StartCoroutine(MoveRackCase(rackCase, moveRange));
        }
        else
        {
            StartCoroutine(MoveRackCase(rackCase, -moveRange));
        }
    }

    private IEnumerator MoveRackCase(GameObject rackCase, float offsetZ)
    {
        float elapsedTime = 0f;
        float duration = 1f;

        Vector3 initialPosition = rackCase.transform.localPosition;
        Vector3 targetPosition = initialPosition + new Vector3(0f, 0f, offsetZ);

        while (elapsedTime < duration)
        {
            rackCase.transform.localPosition = Vector3.Lerp(initialPosition, targetPosition, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        rackCase.transform.localPosition = targetPosition;
        ChangeOpenState(); 
    }
}
