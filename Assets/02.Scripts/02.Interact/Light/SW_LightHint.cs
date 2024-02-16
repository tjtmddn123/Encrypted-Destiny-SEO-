using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SW_LightHint : MonoBehaviour
{
    public List<DoorController> doorOpen; // 열리게 될 문의 DoorController 목록
    public List<DoorController> doorClose; // 닫히게 될 문의 DoorController 목록

    private bool hasActivated = false; // ActivateOpen 메서드가 이미 실행되었는지 추적하는 변수

    void Update()
    {
        CheckDoorsAndActivate();
    }

    private void CheckDoorsAndActivate()
    {
        // 이미 ActivateOpen이 실행되었다면, 더 이상 실행하지 않음
        if (hasActivated) return;

        // doorOpen 목록에 있는 문들이 모두 isOpen 상태인지 확인
        bool allDoorsOpen = true;
        foreach (var door in doorOpen)
        {
            if (!door.isOpen) // 하나라도 isOpen 상태가 아니라면
            {
                allDoorsOpen = false;
                break;
            }
        }

        // doorClose 목록에 있는 문들이 모두 isOpen 상태가 아닌지 확인
        bool allDoorsClose = true;
        foreach (var door in doorClose)
        {
            if (door.isOpen) // 하나라도 isOpen 상태라면
            {
                allDoorsClose = false;
                break;
            }
        }

        // 모든 조건이 만족되면 canOpenState를 활성화
        if (allDoorsOpen && allDoorsClose)
        {
            ActivateOpen();
        }
    }

    private void ActivateOpen()
    {
        // 현재 오브젝트에 부착된 DoorController 컴포넌트를 찾음
        DoorController doorController = GetComponent<DoorController>();

        // DoorController 컴포넌트가 존재하면 CanOpen 메서드를 호출하여 canOpenState를 활성화
        if (doorController != null)
        {
            doorController.CanOpen();

            // ActivateOpen 메서드가 실행되었음을 표시하고, 추가 실행을 방지
            hasActivated = true;
        }
    }

}
