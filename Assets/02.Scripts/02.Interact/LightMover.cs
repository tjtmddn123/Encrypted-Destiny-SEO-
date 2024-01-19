using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class LightMover : MonoBehaviour
{
    [SerializeField] private float max;
    [SerializeField] private float min;
    public GameObject flash;
    public Slider slider;
    private bool isClicking = false;

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

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            isClicking = true;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            isClicking = false;
        }

        if (isClicking)
        {
            // 클릭 중일 때만 레버를 이동
            MoveLever();
        }
    }
    void MoveLever()
    {
        float leverValue = slider.value;

        // 원하는 동작 구현 (예: 좌우로 움직이기)
        float moveAmount = Input.GetAxis("Horizontal");
        leverValue += moveAmount;

        // 레버의 이동 범위 제한
        leverValue = Mathf.Clamp(leverValue, 0f, max);

        // 레버 이동 적용
        slider.value = leverValue;

        // 여기에 필요한 다른 작업을 추가할 수 있습니다.
    }
}