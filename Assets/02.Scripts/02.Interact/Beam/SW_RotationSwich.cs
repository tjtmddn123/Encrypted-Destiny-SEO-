using UnityEngine;

public class SW_RotationSwitch : MonoBehaviour, IInteractable_HT
{
    public GameObject beamObject; // 회전할 빔 오브젝트
    public SW_Beam beamScript; // SW_Beam 스크립트 참조
    private int currentRotationIndex = 0; // 현재 회전 인덱스
    private float[] rotations = { 180, 135, 90, 135, 180, 225, 270, 225 }; // 변경할 회전 각도 배열

    public void OnInteract()
    {
        if (beamScript.isBeamActive)
        {
            // 회전 인덱스를 증가시키고 배열 범위를 넘어가면 0으로 리셋
            currentRotationIndex = (currentRotationIndex + 1) % rotations.Length;
            // 빔 오브젝트의 회전 변경
            beamObject.transform.rotation = Quaternion.Euler(0, rotations[currentRotationIndex], 0);
            float currentRotation = rotations[currentRotationIndex];

            // 특정 각도에서 Range 조절
            if (currentRotation == 135 || currentRotation == 225)
            {
                beamScript.beamLight.range = 12;
            }
            else if (currentRotation == 90 || currentRotation == 270) // 90도와 270도에서 Range를 8로 조절
            {
                beamScript.beamLight.range = 8;
            }
            else
            {
                beamScript.beamLight.range = 10; // 기본 범위 설정
            }
        }
    }
}

