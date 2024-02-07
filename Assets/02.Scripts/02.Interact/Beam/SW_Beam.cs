using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// IInteractable_HT 인터페이스를 구현하는 SW_Beam 스크립트
public class SW_Beam : MonoBehaviour, IInteractable_HT
{
    public Light beamLight; // 오브젝트에 추가한 라이트 컴포넌트 참조
    public bool isBeamActive = false; // 빔이 켜져 있는지 확인하는 변수

    private void Start()
    {
        // 게임이 시작될 때 라이트는 꺼져 있어야 합니다.
        beamLight.enabled = false;
    }

    // IInteractable_HT 인터페이스의 OnInteract 메서드 구현
    public void OnInteract()
    {
        Interact();
    }

    // 실제 상호작용 로직
    private void Interact()
    {
        // 라이트의 활성화 상태를 토글합니다.
        beamLight.enabled = !beamLight.enabled;
        isBeamActive = !isBeamActive; // 빔 상태 토글
    }
}
