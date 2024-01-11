using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SW_Inventory : MonoBehaviour
{
    public static SW_Inventory instance;  // 인벤토리 싱글턴 인스턴스

    void Awake()
    {
        instance = this;  // 싱글턴 인스턴스 설정
    }
}
