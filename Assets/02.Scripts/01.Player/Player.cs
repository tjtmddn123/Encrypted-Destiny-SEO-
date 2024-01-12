using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    [field: SerializeField] public PlayerSO Data { get; private set; }
    public Rigidbody Rigidbody { get; private set; }

    public PlayerInput Input { get; private set; }

    public CharacterController Controller { get; private set; }

    private PlayerStateMachine stateMachine;

    private void Awake()
    {
        Rigidbody = GetComponent<Rigidbody>();
        Input = GetComponent<PlayerInput>();
        Controller = GetComponent<CharacterController>();

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

    private void FixedUpdate()
    {
        stateMachine.PhysicsUpdate();
    }
}
