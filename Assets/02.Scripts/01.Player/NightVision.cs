using System.Collections.Generic;
using UnityEngine;

public class NightVision : MonoBehaviour
{
    public GameObject nightvision; // 나이트 비전 기능을 가진 게임 오브젝트
    public bool hasNightVision = false; // 나이트 비전 보유 여부
    public List<Light> LightObj; // 라이트 컴포넌트를 가진 오브젝트 목록

    // 나이트 비전 아이템을 플레이어에게 추가
    public void AddNightVisionItem()
    {
        hasNightVision = true;
    }

    // 매 프레임마다 사용자 입력을 체크하여 나이트 비전 활성화 여부를 토글
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.N) && hasNightVision)
        {
            // 나이트 비전 활성화 상태를 토글
            nightvision.SetActive(!nightvision.activeInHierarchy);
            // 라이트의 Render Mode를 토글 상태에 따라 설정
            ToggleLights(nightvision.activeInHierarchy);
        }
    }

    // 라이트 목록의 Render Mode를 설정
    private void ToggleLights(bool isNightVisionActive)
    {
        foreach (Light light in LightObj)
        {
            // 나이트 비전이 활성화되었을 경우 Render Mode를 Auto로 설정
            if (isNightVisionActive)
            {
                light.renderMode = LightRenderMode.Auto;
            }
            // 나이트 비전이 비활성화되었을 경우 Render Mode를 Important = ForcePixel로 설정
            else
            {
                light.renderMode = LightRenderMode.ForcePixel;
            }
        }
    }
}
