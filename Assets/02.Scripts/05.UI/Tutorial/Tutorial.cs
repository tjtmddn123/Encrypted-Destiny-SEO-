using System.Collections;
using System.Collections.Generic;
using TMPro;

using UnityEngine;

public class Tutorial : MonoBehaviour
{

    public float fadeDuration = 2.0f; // 사라지는 데 걸리는 시간
    private float currentFadeTime = 0.0f;
    public TextMeshProUGUI tutorial;

    private Vector3 lastMousePosition;


    private void Start()
    {
        lastMousePosition = Input.mousePosition;
        StartCoroutine(ShowTutorial());
    }

    private void Update()
    {
        Vector3 currentMousePosition = Input.mousePosition;

        if (currentMousePosition != lastMousePosition)
        {
            Tutorials();
        }

        lastMousePosition = currentMousePosition;
    }
    public void Tutorials()
    {
        // 페이드 인터폴레이션 계산
        currentFadeTime += Time.deltaTime;
        float alpha = 1.0f - Mathf.Clamp01(currentFadeTime / fadeDuration);

        // 알파 값을 설정하여 Text를 사라지게 함
        Color textColor = tutorial.color;
        textColor.a = alpha;
        tutorial.color = textColor;

        // 사라질 때 추가 작업 수행
        if (alpha <= 0.0f)
        {
            textColor.a = 1f;
            tutorial.gameObject.SetActive(false); // Text를 비활성화하여 완전히 사라지도록 함
        }
    }

    private IEnumerator ShowTutorial()
    {
        yield return new WaitForSeconds(2f);
        tutorial.gameObject.SetActive(true);
    }
}
