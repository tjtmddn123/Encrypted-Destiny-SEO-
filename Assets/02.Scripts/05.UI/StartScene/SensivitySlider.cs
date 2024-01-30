using UnityEngine;
using UnityEngine.UI;
using Cinemachine;

public class CameraSensitivityController : MonoBehaviour
{
    public Slider sensitivitySlider; 
    public CinemachineVirtualCamera virtualCamera; 

    public float minSensitivity = 1f; 
    public float maxSensitivity = 100f; 

    void Update()
    {
        sensitivitySlider.onValueChanged.AddListener(delegate { UpdateCameraSensitivity(); });
    }

    public void UpdateCameraSensitivity()
    {
        float sensitivityValue = sensitivitySlider.value; // 슬라이더 값
        
        float clampedSensitivity = Mathf.Clamp(sensitivityValue, minSensitivity, maxSensitivity);
      
        virtualCamera.GetCinemachineComponent<CinemachinePOV>().m_VerticalAxis.m_MaxSpeed = clampedSensitivity;
        virtualCamera.GetCinemachineComponent<CinemachinePOV>().m_HorizontalAxis.m_MaxSpeed = clampedSensitivity;
    }
}
