using UnityEngine;

public class Reticle : MonoBehaviour
{
    void Start()
    {
        // 화면 가운데로 이동
        RectTransform rectTransform = GetComponent<RectTransform>();
        rectTransform.position = new Vector3(Screen.width / 2f, Screen.height / 2f, 0f);
    }
}
