using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerBaseState : IState 
{

    protected PlayerStateMachine stateMachine;
    protected readonly PlayerGroundData groundData;
    private bool isCrouch;           //앉은 상태 구분
    private float SpeedModifier= 1f; //플레이어 이동속도 배율 제어
    private bool isRunning = false;

    public PlayerBaseState(PlayerStateMachine playerStateMachine)
    {
        stateMachine = playerStateMachine;
        groundData = stateMachine.Player.Data.GroundData;
    }
    public virtual void Enter()
    {
        AddInputActionsCallbacks();
    }

    public virtual void Exit()
    {
        RemoveInputActionsCallbacks();
    }

    public virtual void HandleInput()
    {
        ReadMovementInput();
    }


    public virtual void Update()
    {
        Move();
        if (Input.GetKeyDown(KeyCode.LeftControl) || Input.GetKeyDown(KeyCode.C))
        {
            Crouch();
        }
        if (isCrouch == false && Input.GetKey(KeyCode.LeftShift))
        {
            isRunning = true;
            SpeedModifier = 1.4f;
        }
        if (isCrouch == false && Input.GetKeyUp(KeyCode.LeftShift))
        {
            isRunning = false;
            SpeedModifier = 1f;
        }
    }

    private void ReadMovementInput()
    {
        stateMachine.MovementInput = stateMachine.Player.Input.PlayerActions.Move.ReadValue<Vector2>();
    }

    private void Move()
    {
        Vector3 movementDirection = GetMovementDirection();

        Rotate(movementDirection);

        Move(movementDirection);
    }
    private void Move(Vector3 movementDirection)
    {
        float movementSpeed = GetMovementSpeed();

        stateMachine.Player.Controller.Move(
            ((movementDirection * movementSpeed)
            + stateMachine.Player.ForceReceiver.Movement)
            * Time.deltaTime
            );
    }

    private void Crouch()
    {
        isCrouch = !isCrouch;
        if (isCrouch)
        {
            stateMachine.Player.Controller.height = 0.7f;
            SpeedModifier = 0.6f;
        }
        else 
        {
            stateMachine.Player.Controller.height = 1.8f;
            SpeedModifier = 1f;
        }
    }

    private Vector3 GetMovementDirection()
    {
        Vector3 forward = stateMachine.MainCameraTransform.forward;
        Vector3 right = stateMachine.MainCameraTransform.right;

        forward.y = 0;
        right.y = 0;

        forward.Normalize();
        right.Normalize();

        return forward * stateMachine.MovementInput.y + right * stateMachine.MovementInput.x;
    }

    private float GetMovementSpeed()
    {
        float movementSpeed = stateMachine.MovementSpeed * stateMachine.MovementSpeedModifier * SpeedModifier;
        return movementSpeed;
    }

    private void Rotate(Vector3 movementDirection)
    {
        if (movementDirection != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(movementDirection);
            stateMachine.Player.transform.rotation = Quaternion.Slerp(stateMachine.Player.transform.rotation, targetRotation, stateMachine.RotationDamping * Time.deltaTime);
        }
    }

    protected virtual void AddInputActionsCallbacks()
    {
        PlayerInput input = stateMachine.Player.Input;
        input.PlayerActions.Move.canceled += OnMovementCanceled;
    }

    protected virtual void RemoveInputActionsCallbacks()
    {
        PlayerInput input = stateMachine.Player.Input;
        input.PlayerActions.Move.canceled -= OnMovementCanceled;
    }

    protected virtual void OnMovementCanceled(InputAction.CallbackContext context)
    {

    }

    public void ToggleCursor(bool toggle)
    {
        Cursor.lockState = toggle ? CursorLockMode.None : CursorLockMode.Locked; // 커서 상태 변경
    }
}
