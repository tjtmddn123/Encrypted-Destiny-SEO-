using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SW_LightTrigger : MonoBehaviour
{
    // 인스펙터에서 할당할 라이트 컴포넌트 배열
    public Light[] lightsToToggle;

    private void Start()
    {
        // 게임 시작 시 모든 라이트는 기본적으로 꺼져 있어야 합니다.
        foreach (var light in lightsToToggle)
        {
            if (light != null)
            {
                light.enabled = false;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // 플레이어와 충돌했는지 확인
        if (other.CompareTag("Player"))
        {
            // 모든 라이트 활성화
            foreach (var light in lightsToToggle)
            {
                if (light != null)
                {
                    light.enabled = true;
                }
            }

            // 트리거 오브젝트의 역할이 끝났으므로 삭제
            Destroy(gameObject);
        }
    }
}


