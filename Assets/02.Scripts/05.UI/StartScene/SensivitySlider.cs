using Cinemachine;
using UnityEngine;
using UnityEngine.UI;

public class SensitivitySlider : MonoBehaviour
{
    public Slider sensitivitySlider;
    public CinemachineVirtualCamera virtualCamera;

    public float minSensitivity = 1f; // 최소 감도
    public float maxSensitivity = 10f; // 최대 감도
    public float sensitivityMultiplier = 100f; // 감도배율

    private void Start()
    {
        sensitivitySlider.onValueChanged.AddListener(delegate { UpdateCameraSensitivity(); });
    }

    public void UpdateCameraSensitivity()
    {
        float sensitivityValue = sensitivitySlider.value; // 슬라이더 값

        
        float clampedSensitivity = Mathf.Clamp(sensitivityValue, minSensitivity, maxSensitivity);

        // 메인 카메라의 감도를 업데이트.
        virtualCamera.GetCinemachineComponent<CinemachinePOV>().m_VerticalAxis.m_MaxSpeed = clampedSensitivity * sensitivityMultiplier;
        virtualCamera.GetCinemachineComponent<CinemachinePOV>().m_HorizontalAxis.m_MaxSpeed = clampedSensitivity * sensitivityMultiplier;
    }
}
