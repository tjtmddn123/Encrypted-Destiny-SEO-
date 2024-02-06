using System.Collections;
using System.Collections.Generic;
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

    public GameObject[] OpenableUI;

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
    public bool IsUIOpening()
    {
        if (Array.Find(OpenableUI, element => element.activeInHierarchy == true))
        {
            Cursor.lockState = CursorLockMode.None;
            return true;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            return false;
        }
    }
}
