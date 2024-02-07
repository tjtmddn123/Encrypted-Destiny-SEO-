using UnityEngine;

public class SW_ColorSwitch : MonoBehaviour, IInteractable_HT
{
    public Light beamLight; // 빔 라이트 컴포넌트
    public SW_Beam beamScript; // SW_Beam 스크립트 참조
    public int currentColorIndex = 0; // 현재 색상 인덱스
    private Color[] colors = { Color.white, Color.red, Color.yellow, Color.cyan }; // 변경할 색상 배열


    public void OnInteract()
    {
        if (beamScript.isBeamActive)
        {
            // 색상 인덱스를 증가시키고 배열 범위를 넘어가면 0으로 리셋
            currentColorIndex = (currentColorIndex + 1) % colors.Length;
            // 라이트 색상 변경
            beamLight.color = colors[currentColorIndex];
        }
    }
}

