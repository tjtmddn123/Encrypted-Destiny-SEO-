using UnityEngine;
using UnityEngine.UI;
using Cinemachine;

public class CameraSensitivityController : MonoBehaviour
{
    public Slider sensitivitySlider; 
    public CinemachineVirtualCamera virtualCamera;
    public InteractManager_HT InteractManager;
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
        sensitivitySlider.value = Sensitivity;
        sensitivitySlider.onValueChanged.AddListener(UpdateCameraSensitivity);
    }


    private void Update()
    {
        if (Sinventory.inventoryWindow.activeInHierarchy)
        {
            CameraStop();
        }
        else
        {
            //현재 이 코드에 문제점이 있습니다. 위의 if문에 써 있듯이 인벤토리가 닫혀 있다면 무조건 Sensitivity의 값으로 감도가 고정이 됩니다.
            CameraMove();
        }
    }

    public void CameraStop()
    {
        virtualCamera.GetCinemachineComponent<CinemachinePOV>().m_VerticalAxis.m_MaxSpeed = 0;
        virtualCamera.GetCinemachineComponent<CinemachinePOV>().m_HorizontalAxis.m_MaxSpeed = 0;
    }
    public void CameraMove()
    {
        UpdateCameraSensitivity(Sensitivity);
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
