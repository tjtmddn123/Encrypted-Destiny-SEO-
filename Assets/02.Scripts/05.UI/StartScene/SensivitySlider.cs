using Cinemachine;
using UnityEngine;
using UnityEngine.UI;

public class SensitivitySlider : MonoBehaviour
{
    public Slider sensitivitySlider;
    public CinemachineVirtualCamera virtualCamera;

    public float minSensitivity = 1f; // 최소 감도
    public float maxSensitivity = 100f; // 최대 감도
    public float sensitivityMultiplier = 1000f; // 감도배율

    private void Start()
    {
        sensitivitySlider.onValueChanged.AddListener(delegate { UpdateCameraSensitivity(); });
    }

    public void UpdateCameraSensitivity()
    {
        float sensitivityValue = sensitivitySlider.value; 

        
        float clampedSensitivity = Mathf.Clamp(sensitivityValue, minSensitivity, maxSensitivity);

        
        virtualCamera.GetCinemachineComponent<CinemachinePOV>().m_VerticalAxis.Value = clampedSensitivity * sensitivityMultiplier;
        virtualCamera.GetCinemachineComponent<CinemachinePOV>().m_HorizontalAxis.Value = clampedSensitivity * sensitivityMultiplier;
    }
}
