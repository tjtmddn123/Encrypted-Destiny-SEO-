using UnityEngine;
using UnityEngine.Events;

public class SW_BeamClear : MonoBehaviour
{
    // 비활성화할 컴포넌트들을 위한 참조
    public SW_Beam beamScript;
    public SW_ColorSwitch colorSwitchScript;
    public SW_BeamHint[] beamHintScripts;
    public SW_RotationSwitch rotationSwitchScript;

    // 제거할 오브젝트들을 담을 배열
    public GameObject[] objectsToRemove;

    // 키패드의 암호가 맞춰졌을 때 호출될 함수
    public void OnKeypadAccessGranted()
    {
        // 빔 관련 기능 비활성화 및 레이어, 태그 변경
        if (beamScript != null)
        {
            beamScript.enabled = false;
            beamScript.gameObject.layer = LayerMask.NameToLayer("Default");
            beamScript.gameObject.tag = "Untagged";
        }

        if (colorSwitchScript != null)
        {
            colorSwitchScript.enabled = false;
            colorSwitchScript.gameObject.layer = LayerMask.NameToLayer("Default");
            colorSwitchScript.gameObject.tag = "Untagged";
        }

        if (rotationSwitchScript != null)
        {
            rotationSwitchScript.enabled = false;
            rotationSwitchScript.gameObject.layer = LayerMask.NameToLayer("Default");
            rotationSwitchScript.gameObject.tag = "Untagged";
        }

        // 빔 힌트 스크립트들 비활성화
        foreach (var hintScript in beamHintScripts)
        {
            if (hintScript != null) hintScript.enabled = false;
        }

        // 지정된 오브젝트들 제거
        foreach (var obj in objectsToRemove)
        {
            if (obj != null) Destroy(obj);
        }
    }
}

