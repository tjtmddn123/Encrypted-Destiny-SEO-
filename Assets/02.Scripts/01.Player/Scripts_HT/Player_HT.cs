using UnityEngine;
using Cinemachine;
using System;

public class Player_HT : MonoBehaviour
{

    [field: SerializeField] public PlayerSO Data { get; private set; }
    public Rigidbody Rigidbody { get; private set; }

    public PlayerInput Input { get; private set; }

    public CharacterController Controller { get; private set; }

    public ForceReceiver ForceReceiver { get; private set; }

    private PlayerStateMachine stateMachine;

    public CinemachineVirtualCamera virtualCamera;

    public UIManager UIManager;

    private void Awake()
    {
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
        stateMachine.HandleInput();
        stateMachine.Update();
    }

}
