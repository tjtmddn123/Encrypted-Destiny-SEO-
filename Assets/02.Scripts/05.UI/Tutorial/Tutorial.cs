using TMPro;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    public TextMeshProUGUI tutorial;

    private Vector3 lastMousePosition;

    void Start()
    {
        lastMousePosition = Input.mousePosition;
    }
    void Update()
    {
        Vector3 currentMousePosition = Input.mousePosition;
        float mouseMovementThreshold = 0.1f; // 임계값 설정

        if (Vector3.Distance(currentMousePosition, lastMousePosition) > mouseMovementThreshold)
        {
            tutorial.gameObject.SetActive(false);
        }

        lastMousePosition = currentMousePosition;
    }
}
