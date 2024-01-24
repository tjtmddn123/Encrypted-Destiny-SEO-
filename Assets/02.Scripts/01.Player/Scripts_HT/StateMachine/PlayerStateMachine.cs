using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMachine : StateMachine
{
    public Player_HT Player { get; }

    public PlayerIdleState idleState { get; }
    public PlayerWalkState walkState { get; }

    public Vector2 MovementInput { get; set; }

    public float MovementSpeed { get; private set; }

    public float RotationDamping { get; private set; }

    public float MovementSpeedModifier { get; set; } = 1f;

    public Transform MainCameraTransform { get; set; }

    public PlayerStateMachine(Player_HT player)
    {
        this.Player = player;

        idleState = new PlayerIdleState(this);
        walkState = new PlayerWalkState(this);

        MainCameraTransform = Camera.main.transform;

        MovementSpeed = player.Data.GroundData.BaseSpeed;
        RotationDamping = player.Data.GroundData.BaseRotationDamping;
    }
}
