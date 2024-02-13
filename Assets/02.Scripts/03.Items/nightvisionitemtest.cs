using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class nightvisionitemtest : MonoBehaviour
{
    public GameObject[] WallTexts; // 배열로 변경
    public GameObject[] RTexts; // 배열로 변경
    private NightVision nightVision;

    private void Start()
    {
        nightVision = Camera.main.GetComponent<NightVision>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.N))
        {
            if (nightVision.hasNightVision == true)
            {
                ToggleGameObjects(WallTexts);
                ToggleGameObjects(RTexts);
            }
        }
    }

    // GameObject 배열의 활성화/비활성화 상태를 토글하는 메서드
    void ToggleGameObjects(GameObject[] gameObjects)
    {
        foreach (var gameObject in gameObjects)
        {
            gameObject.SetActive(!gameObject.activeInHierarchy);
        }
    }
}

