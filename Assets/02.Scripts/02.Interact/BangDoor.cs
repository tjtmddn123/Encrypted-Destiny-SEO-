using System.Collections;
using UnityEngine;

public class BangDoor : MonoBehaviour
{
    [Header("Door")]
    public bool isOpen;      // 열려있는 문인지를 판단하기 위한 bool
    public bool isReverse = false;  // 반대로 열리는 문이 있으면 true로 해주세요
    public float openSpeed = 1.5f; // 문이 열리는 속도

    [SerializeField]
    private bool isOpening = false;
    [SerializeField]
    private bool canOpenState = false;   // 열 수 있는지 구분 

    [Header("Case")]
    [SerializeField]
    private float moveRange = 0.35f; // 서랍 이동 거리입니다.
    public AudioClip openSoundClip;
    public AudioClip closeSoundClip1;
    public AudioClip closeSoundClip2;

    private AudioSource audioSource1;
    private AudioSource audioSource2;

    private void Start()
    {
        // AudioSource 컴포넌트 추가
        audioSource1 = gameObject.AddComponent<AudioSource>();
        audioSource2 = gameObject.AddComponent<AudioSource>();
    }

    public void OpenDoor(GameObject door)
    {
        if (!isOpening)
        {
            StartCoroutine(DoorOpening());
            if (canOpenState == true)
            {
                Quaternion targetRotation = door.transform.localRotation;

                if (isOpen)
                {
                    targetRotation *= Quaternion.Euler(0f, 90f, 0f);

                    // 첫 번째 사운드 재생
                    audioSource1.clip = closeSoundClip1;
                    audioSource1.Play();

                    // 두 번째 사운드 재생
                    audioSource2.clip = closeSoundClip2;
                    audioSource2.Play();
                }
                else
                {
                    targetRotation *= Quaternion.Euler(0f, -90f, 0f);

                    // 열리는 사운드 재생
                    audioSource1.clip = openSoundClip;
                    audioSource1.Play();
                }

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
    private void ChangeOpenState()
    {
        isOpen = !isOpen;
    }

    public void CanOpen()
    {
        canOpenState = true;
    }
}
