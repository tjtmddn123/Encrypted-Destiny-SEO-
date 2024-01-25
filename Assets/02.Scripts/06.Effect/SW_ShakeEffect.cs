using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using Cinemachine;
using NavKeypad; // NavKeypad 네임스페이스 참조

public class SW_ShakeEffect : MonoBehaviour, IInteractable_HT
{
    public CinemachineVirtualCamera virtualCamera; // 버추얼 카메라 참조
    public float shakeDuration; // 진동 지속 시간
    public float amplitudeGain; // 진동 강도
    public float frequencyGain; // 진동 빈도

    [Header("Keypad Reference")]
    [SerializeField] private Keypad keypad; // Keypad 참조

    private bool shakeEffectApplied = false; // 진동 효과 적용 여부

    void Start()
    {
        // Keypad의 OnAccessGranted 이벤트에 진동 효과 메서드를 연결
        if (keypad != null)
        {
            keypad.OnAccessGranted.AddListener(ActivateShakeEffect);
            Debug.Log("SW_Effect: Keypad 이벤트 연결됨.");
        }
        else
        {
            Debug.Log("SW_Effect: Keypad 참조가 없음.");
        }

        // 버추얼 카메라 설정 확인
        if (virtualCamera != null)
        {
            Debug.Log("SW_Effect: 버추얼 카메라 설정됨.");
        }
        else
        {
            Debug.Log("SW_Effect: 버추얼 카메라 설정되지 않음.");
        }
    }

    // 트리거 영역에 진입 시 진동 효과 적용
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !shakeEffectApplied)
        {
            ApplyShakeEffect();
            Debug.Log("SW_Effect: 플레이어가 트리거에 진입하여 진동 효과 적용됨.");
        }
    }

    // 상호작용 인터페이스 구현
    public void OnInteract()
    {
        if (virtualCamera != null && !shakeEffectApplied)
        {
            ApplyShakeEffect();
            Debug.Log("SW_Effect: 상호작용을 통해 진동 효과 적용됨.");
        }
    }

    // Keypad의 성공 이벤트에 대한 응답
    private void ActivateShakeEffect()
    {
        ApplyShakeEffect();
        Debug.Log("SW_Effect: Keypad 성공 이벤트에 응답하여 진동 효과 적용됨.");
    }

    // 진동 효과 적용
    private void ApplyShakeEffect()
    {
        if (shakeEffectApplied) return;

        shakeEffectApplied = true;
        var perlin = virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        perlin.m_AmplitudeGain = amplitudeGain;
        perlin.m_FrequencyGain = frequencyGain;
        StartCoroutine(StopCameraShake());
        Debug.Log("SW_Effect: 진동 효과 적용됨.");
    }

    // 진동 효과 중지 코루틴
    IEnumerator StopCameraShake()
    {
        yield return new WaitForSeconds(shakeDuration);
        var perlin = virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        perlin.m_AmplitudeGain = 0;
        perlin.m_FrequencyGain = 0;
        Debug.Log("SW_Effect: 진동 효과 중지됨.");
    }
}

