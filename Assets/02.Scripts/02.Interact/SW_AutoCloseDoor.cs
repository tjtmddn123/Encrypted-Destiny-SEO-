using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SW_AutoCloseDoor : MonoBehaviour
{
    public DoorController doorController; // DoorController 컴포넌트 참조
    public GameObject door; // 자동으로 닫힐 문 객체
    public GameObject autoCloseTrigger; // AutoCloseTrigger 오브젝트
    private ChangeCinemachine change;
    public string newTag; // 문이 닫힌 후 적용할 새 태그

    private bool hasAutoClosed = false; // 문이 자동으로 닫힌 적이 있는지 확인

    private void Start()
    {
        change = autoCloseTrigger.GetComponent<ChangeCinemachine>();
    }

    void OnTriggerEnter(Collider other)
    {
        // 플레이어 태그와 충돌했는지 확인
        if (other.CompareTag("Player") && !hasAutoClosed)
        {
            Debug.Log("플레이어와 충돌. 문을 닫습니다.");
            CloseDoor();
            change.CinemachineTest();
        }
    }

    private void CloseDoor()
    {
        // 태그 변경
        if (!string.IsNullOrEmpty(newTag))
        {
            door.tag = newTag;
            Debug.Log("태그 변경됨");
        }

        // 문 닫히는 애니메이션 시작
        StartCoroutine(AutoCloseDoor());
    }

    private IEnumerator AutoCloseDoor()
    {
        yield return new WaitForSeconds(2f);
        // 닫히는 과정 시작
        float elapsedTime = 0f;
        float closeSpeed = 0.5f; // 닫히는데 걸리는 시간
        Quaternion initialRotation = door.transform.localRotation;
        Quaternion targetRotation = Quaternion.Euler(0, 0, 0); // 목표 회전값 (0,0,0)

        while (elapsedTime < closeSpeed)
        {
            door.transform.localRotation = Quaternion.Slerp(initialRotation, targetRotation, elapsedTime / closeSpeed);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        door.transform.localRotation = targetRotation; // 최종적으로 목표 회전값 설정

        // isOpening 상태 업데이트 및 자동 닫힘 플래그 설정
        doorController.isOpen = false;
        hasAutoClosed = true;
    }
}
