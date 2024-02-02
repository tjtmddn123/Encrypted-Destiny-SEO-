using UnityEngine;
using UnityEngine.UI;
using Cinemachine;

public class CameraSensitivityController : MonoBehaviour
{
    public Slider sensitivitySlider; 
    public CinemachineVirtualCamera virtualCamera;

    public SW_Inventory Sinventory;

    [SerializeField]
    private float minSensitivity = 1f;
    [SerializeField]
    private float maxSensitivity = 100f;
    [SerializeField]
    private float Sensitivity;

    void Start()
    {
        sensitivitySlider.minValue = minSensitivity;
        sensitivitySlider.maxValue = maxSensitivity;
        sensitivitySlider.onValueChanged.AddListener(UpdateCameraSensitivity);
    }


    private void Update()
    {
        if (Sinventory.inventoryWindow.activeInHierarchy)
        {
            virtualCamera.GetCinemachineComponent<CinemachinePOV>().m_VerticalAxis.m_MaxSpeed = 0;
            virtualCamera.GetCinemachineComponent<CinemachinePOV>().m_HorizontalAxis.m_MaxSpeed = 0;
        }
        else
        {
            UpdateCameraSensitivity(Sensitivity);
        }
    }

    public void UpdateCameraSensitivity(float sensitivityValue)
    {
        Sensitivity = sensitivityValue;
        //sensitivityValue = sensitivitySlider.value; // 슬라이더 값

        //float clampedSensitivity = Mathf.Clamp(sensitivityValue, minSensitivity, maxSensitivity);

        virtualCamera.GetCinemachineComponent<CinemachinePOV>().m_VerticalAxis.m_MaxSpeed = sensitivityValue;
        virtualCamera.GetCinemachineComponent<CinemachinePOV>().m_HorizontalAxis.m_MaxSpeed = sensitivityValue;
        
    }
}
