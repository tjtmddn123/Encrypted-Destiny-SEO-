using System.Collections;

using UnityEngine;

public class DoorController : MonoBehaviour
{
    [Header("Door")]
    public bool isOpen;      //열려있는 문인지를 판단하기 위한 bool
    public bool isReverse = false;  //반대로 열리는 문이 있으면 true로 해주세요
    public float openSpeed = 1.5f; // 문이 열리는 속도

    [SerializeField]
    private bool isOpening = false;
    [SerializeField]
    private bool canOpenState = false;   //열수 있는지 구분 

    [Header("Case")]
    [SerializeField]
    private float moveRange = 0.35f; //서랍 이동 거리입니다.
    public AudioClip clip;

    public void OpenDoor(GameObject door)
    {
        if (!isOpening)
        {
            StartCoroutine(DoorOpening());
            if (canOpenState == true)
            {
                Quaternion targetRotation = door.transform.localRotation;
                AudioClip soundClip;
                if (isOpen)
                {
                    targetRotation *= Quaternion.Euler(0f, 90f, 0f);
                    soundClip = SoundManager.instance.closeClip;
                }
                else
                {
                    targetRotation *= Quaternion.Euler(0f, -90f, 0f);
                    soundClip = SoundManager.instance.openClip;
                }
                SoundManager.instance.SFXPlay(isOpen ? "Open" : "Close", soundClip);
                StartCoroutine(RotateDoor(door.transform, targetRotation));
                ChangeOpenState();
            }
        }
    }

    private IEnumerator DoorOpening()
    {
        isOpening = true;

        yield return new WaitForSeconds(openSpeed);

        isOpening = false;
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
    private void ChangeOpenState()
    {
        isOpen = !isOpen;
    }

    public void CanOpen()
    {
        canOpenState = true;
    }

    public void OpenRackCase(GameObject rackCase)
    {
        if (!isOpening)
        {
            StartCoroutine(DoorOpening());
            AudioClip soundClip;
            if (isReverse == true)
            {
                if (isOpen)
                {
                    StartCoroutine(MoveRackCase(rackCase, moveRange));
                }
                else
                {
                    StartCoroutine(MoveRackCase(rackCase, -moveRange));
                }
            }
            else
            {
                if (isOpen)
                {
                    StartCoroutine(MoveRackCase(rackCase, -moveRange));
                }
                else
                {
                    StartCoroutine(MoveRackCase(rackCase, moveRange));
                }
            }
            
        }
    }
    
}
