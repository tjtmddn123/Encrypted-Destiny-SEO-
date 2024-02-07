using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mark2 : MonoBehaviour
{
    // 표식 오브젝트 배열
    public GameObject[] signs;

    // 표식이 완성되는 각도 범위
    public float mergeAngleThreshold = 30f;

    // 표식이 합쳐졌을 때 보여질 완성된 표식 오브젝트
    public GameObject completedSign;

    void Update()
    {
        // 플레이어의 위치와 각도를 기준으로 합쳐진 표식을 보여줄지 결정
        Vector3 playerPosition = transform.position;
        Vector3 directionToSign = completedSign.transform.position - playerPosition;
        float angle = Vector3.Angle(transform.forward, directionToSign);

        if (angle < mergeAngleThreshold)
        {
            // 표식이 합쳐질 각도 범위 안에 있을 때 완성된 표식을 활성화
            completedSign.SetActive(true);

            // 기존의 표식들을 숨김
            foreach (var sign in signs)
            {
                sign.SetActive(false);
            }
        }
        else
        {
            // 표식이 합쳐지지 않을 각도 범위에 있을 때 모든 표식을 활성화
            completedSign.SetActive(false);
            foreach (var sign in signs)
            {
                sign.SetActive(true);
            }
        }
    }
}
