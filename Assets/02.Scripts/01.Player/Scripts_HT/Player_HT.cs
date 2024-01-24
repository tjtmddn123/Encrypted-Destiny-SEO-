using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Player_HT : MonoBehaviour
{

    [field: SerializeField] public PlayerSO Data { get; private set; }
    public Rigidbody Rigidbody { get; private set; }

    public PlayerInput Input { get; private set; }

    public CharacterController Controller { get; private set; }

    public ForceReceiver ForceReceiver { get; private set; }

    private PlayerStateMachine stateMachine;

    public CinemachineVirtualCamera virtualCamera;

    public SW_Inventory inventory;

    public GameObject memo;

    private void Awake()
    {
        inventory = GetComponent<SW_Inventory>();
        Rigidbody = GetComponent<Rigidbody>();
        Input = GetComponent<PlayerInput>();
        Controller = GetComponent<CharacterController>();
        ForceReceiver = GetComponent<ForceReceiver>();
        stateMachine = new PlayerStateMachine(this);
    }

    private void Start()
    {
        
        Cursor.lockState = CursorLockMode.Locked;
        stateMachine.ChangeState(stateMachine.idleState);
    }

    private void Update()
    {
        if (inventory.inventoryWindow.activeInHierarchy || memo.activeInHierarchy)
        {
            virtualCamera.GetCinemachineComponent<CinemachinePOV>().m_VerticalAxis.m_MaxSpeed = 0;
            virtualCamera.GetCinemachineComponent<CinemachinePOV>().m_HorizontalAxis.m_MaxSpeed = 0;
        }
        else
        {
            virtualCamera.GetCinemachineComponent<CinemachinePOV>().m_VerticalAxis.m_MaxSpeed = 300;
            virtualCamera.GetCinemachineComponent<CinemachinePOV>().m_HorizontalAxis.m_MaxSpeed = 300;
        }
        stateMachine.HandleInput();
        stateMachine.Update();
    }
}
