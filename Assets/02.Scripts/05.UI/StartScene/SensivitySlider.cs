using UnityEngine;
using UnityEngine.UI;
using Cinemachine;

public class CameraSensitivityController : MonoBehaviour
{
    public Slider sensitivitySlider; 
    public CinemachineVirtualCamera virtualCamera;
    public Player_HT player;

    [SerializeField]
    private float minSensitivity = 1f;
    [SerializeField]
    private float maxSensitivity = 1000f;
    [SerializeField]
    private float Sensitivity = 300f;    

    void Start()
    {
        sensitivitySlider.minValue = minSensitivity;
        sensitivitySlider.maxValue = maxSensitivity;
        sensitivitySlider.value = Sensitivity;
        sensitivitySlider.onValueChanged.AddListener(UpdateCameraSensitivity);
    }


    private void Update()
    {
        if (player.IsUIOpening())
        {
            CameraStop();
        }
        else
        {
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

        virtualCamera.GetCinemachineComponent<CinemachinePOV>().m_VerticalAxis.m_MaxSpeed = sensitivityValue;
        virtualCamera.GetCinemachineComponent<CinemachinePOV>().m_HorizontalAxis.m_MaxSpeed = sensitivityValue;
        
    }
}
