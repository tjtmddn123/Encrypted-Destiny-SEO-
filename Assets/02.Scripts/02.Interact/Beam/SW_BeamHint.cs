using TMPro;
using UnityEngine;

public class SW_BeamHint : MonoBehaviour
{
    public enum HintColor
    {
        white = 0,
        red = 1,
        yellow = 2,
        cyan = 3
    }

    public HintColor hintColor; // 인스펙터에서 설정한 힌트의 색상
    public SW_ColorSwitch colorSwitch; // 인스펙터에서 할당한 SW_ColorSwitch 스크립트의 참조
    public SW_Beam beamScript; // 인스펙터에서 할당한 SW_Beam 스크립트의 참조

    private TextMeshPro tmpText; // TMP 컴포넌트 참조

    private void Awake()
    {
        tmpText = GetComponent<TextMeshPro>();
        if (tmpText == null)
        {
            Debug.LogError("[SW_BeamHint] TMP 컴포넌트를 찾을 수 없습니다.");
        }

        // 초기에 TMP 컴포넌트를 비활성화 상태로 설정
        if (tmpText != null) tmpText.enabled = false;
    }

    private void Update()
    {
        // colorSwitch와 beamScript의 인스턴스가 존재하고, 빔이 활성화 상태인지 확인
        if (colorSwitch != null && beamScript != null && beamScript.isBeamActive)
        {
            // 빔이 활성화 상태이고, 현재 색상 인덱스가 힌트의 색상과 일치하는지 확인
            bool isActiveAndColorMatches = ((int)hintColor == colorSwitch.currentColorIndex);

            // 조건에 따라 TMP 컴포넌트의 활성화 상태를 업데이트
            if (tmpText != null) tmpText.enabled = isActiveAndColorMatches;
        }
        else
        {
            // colorSwitch 또는 beamScript의 인스턴스가 없거나, 빔이 비활성화 상태일 경우
            if (tmpText != null) tmpText.enabled = false;
        }
    }
}
