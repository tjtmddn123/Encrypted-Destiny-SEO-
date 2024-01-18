using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

using static UnityEngine.Rendering.DebugUI;

public class LightMover : MonoBehaviour
{
    [SerializeField] private float max;
    [SerializeField] private float min;
    public GameObject flash;
    public Slider slider;

    private void Start()
    {
        slider.maxValue = max;
        slider.minValue = min;
        slider.onValueChanged.AddListener(Move);
    }

    private void Move(float value)
    {
        flash.transform.localPosition = new Vector3(-value, 0, 0);
    }
}