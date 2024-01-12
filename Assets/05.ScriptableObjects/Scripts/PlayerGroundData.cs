using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[Serializable]
public class PlayerGroundData
{
    [field: SerializeField] [field: Range(0f, 25f)] public float BaseSpeed { get; private set; } = 5f;
    [field: SerializeField] [field: Range(0f, 25f)] public float BaseRoattionDamping { get; private set; } = 1f;

    [field: Header("WalkData")]
    [field: SerializeField] [field: Range(0f, 2f)] public float WalkSpeedModifier { get; private set; } = 0.225f;

}
