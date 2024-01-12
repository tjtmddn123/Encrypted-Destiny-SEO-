using System.Collections;
using System.Collections.Generic;

using UnityEditor.ShaderGraph;

using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerWalkState : PlayerGroundState
{
    public PlayerWalkState(PlayerStateMachine playerStateMachine) : base(playerStateMachine)
    {
    }

    public override void Enter()
{
        stateMachine.MovementSpeedModifier = groundData.WalkSpeedModifier;
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }
}