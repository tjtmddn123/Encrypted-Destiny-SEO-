using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerBaseState : IState
{

    protected PlayerStateMachine stateMachine;
    protected readonly PlayerGroundData groundData;

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



    public virtual void PhysicsUpdate()
    {
        
    }

    public virtual void Update()
    {
        Move();
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
    private void Move(Vector3 movementDirection)
    {
        float movementSpeed = GetMovementSpeed();

        stateMachine.Player.Controller.Move(
            (movementDirection * movementSpeed) * Time.deltaTime
            );
    }

    private float GetMovementSpeed()
    {
        float movementSpeed = stateMachine.MovementSpeed * stateMachine.MovementSpeedModifier;
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
    protected virtual void OnRunStarted(InputAction.CallbackContext context)
    {

    }

    protected virtual void OnMovementCanceled(InputAction.CallbackContext context)
    {

    }
}
