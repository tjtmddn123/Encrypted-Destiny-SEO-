using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    // 이동 관련 변수 선언
    [Header("Movement")]
    public float moveSpeed; // 이동 속도
    private Vector2 curMovementInput; // 현재 이동 입력 값
    public LayerMask groundLayerMask; // 땅인지 확인하는 레이어 마스크

    // 시선 이동(카메라 조작) 관련 변수 선언
    [Header("Look")]
    public Transform cameraContainer; // 카메라를 담고 있는 컨테이너
    public float minXLook; // 카메라가 위로 올라갈 수 있는 최소 각도
    public float maxXLook; // 카메라가 아래로 내려갈 수 있는 최대 각도
    private float camCurXRot; // 카메라 현재 X축 회전 값
    public float lookSensitivity; // 시선 이동 감도

    // 앉기 관련 변수 선언
    [Header("Sit")]
    public float sitSpeed; // 앉았을 때 이동 속도 감소 비율
    public float sitColliderHeight; // 앉았을 때 Collider 높이 감소 비율
    public float sitCameraYPos; // 앉았을 때 카메라 Y 위치 감소 비율

    private bool isSitting = false; // 플레이어가 앉아있는지 여부
    private BoxCollider playerCollider; // 플레이어의 박스 콜라이더
    private float originalColliderHeight; // 원래 콜라이더 높이
    private float originalCameraYPos; // 원래 카메라 Y 위치
    private float originalMoveSpeed; // 원래 이동 속도

    private Vector2 mouseDelta; // 마우스 이동량

    [HideInInspector]
    public bool canLook = true; // 카메라를 움직일 수 있는지 여부

    private Rigidbody _rigidbody; // Rigidbody 컴포넌트

    public static PlayerController instance; // 싱글톤 인스턴스
    private void Awake()
    {
        instance = this; // 싱글톤 할당
        _rigidbody = GetComponent<Rigidbody>(); // Rigidbody 컴포넌트 가져오기
        playerCollider = GetComponent<BoxCollider>(); // 박스 콜라이더 가져오기
        originalColliderHeight = playerCollider.size.y; // 원래 콜라이더 높이 저장
        originalCameraYPos = cameraContainer.localPosition.y; // 원래 카메라 Y 위치 저장
        originalMoveSpeed = moveSpeed; // 원래 이동 속도 저장
    }

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked; // 게임 시작 시 커서 고정        
    }

    private void FixedUpdate()
    {
        Move(); // 이동 로직 처리
    }

    private void LateUpdate()
    {
        if (canLook)
        {
            CameraLook(); // 카메라 시선 이동 처리
        }
    }

    // 이동 처리 함수
    private void Move()
    {
        // 이동 방향 계산
        Vector3 dir = transform.forward * curMovementInput.y + transform.right * curMovementInput.x;
        dir *= moveSpeed; // 속도 적용
        dir.y = _rigidbody.velocity.y; // Y축 속도 유지

        _rigidbody.velocity = dir; // Rigidbody에 속도 적용
    }

    // 카메라 시선 이동 처리 함수
    void CameraLook()
    {
        // 마우스 입력에 따른 카메라 X축 회전 계산
        camCurXRot += mouseDelta.y * lookSensitivity;
        camCurXRot = Mathf.Clamp(camCurXRot, minXLook, maxXLook);
        cameraContainer.localEulerAngles = new Vector3(-camCurXRot, 0, 0);

        // 마우스 입력에 따른 캐릭터 Y축 회전
        transform.eulerAngles += new Vector3(0, mouseDelta.x * lookSensitivity, 0);
    }

    // 입력 처리 함수들
    public void OnLookInput(InputAction.CallbackContext context)
    {
        mouseDelta = context.ReadValue<Vector2>(); // 마우스 이동량 업데이트
    }

    public void OnMoveInput(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            curMovementInput = context.ReadValue<Vector2>(); // 이동 입력 값 업데이트
        }
        else if (context.phase == InputActionPhase.Canceled)
        {
            curMovementInput = Vector2.zero; // 이동 입력 중지
        }
    }

    // 앉기 처리 함수
    public void OnSitInput(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            ToggleSit(); // 앉기 토글
        }
    }

    // 앉기 상태 토글 함수
    private void ToggleSit()
    {
        isSitting = !isSitting; // 앉기 상태 토글

        if (isSitting)
        {
            SitDown(); // 앉기 동작
        }
        else
        {
            StandUp(); // 일어나기 동작
        }
    }

    // 앉기 동작 함수
    private void SitDown()
    {
        moveSpeed = originalMoveSpeed * sitSpeed; // 이동 속도 감소
        playerCollider.size = new Vector3(playerCollider.size.x, originalColliderHeight * sitColliderHeight, playerCollider.size.z); // 콜라이더 높이 감소
        cameraContainer.localPosition = new Vector3(cameraContainer.localPosition.x, originalCameraYPos * sitCameraYPos, cameraContainer.localPosition.z); // 카메라 위치 조절
    }

    // 일어나기 동작 함수
    private void StandUp()
    {
        moveSpeed = originalMoveSpeed; // 이동 속도 복원
        playerCollider.size = new Vector3(playerCollider.size.x, originalColliderHeight, playerCollider.size.z); // 콜라이더 높이 복원
        cameraContainer.localPosition = new Vector3(cameraContainer.localPosition.x, originalCameraYPos, cameraContainer.localPosition.z); // 카메라 위치 복원
    }

    // 캐릭터가 땅에 있는지 확인하는 함수
    private bool IsGrounded()
    {
        // 다양한 방향에서 레이를 쏴서 땅 체크
        Ray[] rays = new Ray[4]
        {
            new Ray(transform.position + (transform.forward * 0.2f) + (Vector3.up * 0.01f) , Vector3.down),
            new Ray(transform.position + (-transform.forward * 0.2f)+ (Vector3.up * 0.01f), Vector3.down),
            new Ray(transform.position + (transform.right * 0.2f) + (Vector3.up * 0.01f), Vector3.down),
            new Ray(transform.position + (-transform.right * 0.2f) + (Vector3.up * 0.01f), Vector3.down),
        };

        // 각 레이에 대해 땅 체크
        for (int i = 0; i < rays.Length; i++)
        {
            if (Physics.Raycast(rays[i], 0.1f, groundLayerMask))
            {
                return true; // 땅에 있으면 true 반환
            }
        }

        return false; // 땅에 없으면 false 반환
    }

    // Gizmo로 땅 체크용 레이를 그리는 함수 (디버깅 용도)
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position + (transform.forward * 0.2f), Vector3.down);
        Gizmos.DrawRay(transform.position + (-transform.forward * 0.2f), Vector3.down);
        Gizmos.DrawRay(transform.position + (transform.right * 0.2f), Vector3.down);
        Gizmos.DrawRay(transform.position + (-transform.right * 0.2f), Vector3.down);
    }

    // 커서 고정/해제 함수
    public void ToggleCursor(bool toggle)
    {
        Cursor.lockState = toggle ? CursorLockMode.None : CursorLockMode.Locked; // 커서 상태 변경
        canLook = !toggle; // 시선 이동 가능 상태 변경
    }
}

