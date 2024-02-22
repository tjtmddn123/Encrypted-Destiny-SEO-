using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class SW_END : MonoBehaviour
{
    public Image fadePanel; // 화면을 어두워지게 할 UI 패널
    public TextMeshProUGUI creditsText; // 크레딧을 표시할 TMP 객체
    public TextMeshProUGUI ClickText;
    public GameObject player; // 플레이어 게임 오브젝트
    public GameObject promptText;
    public float delayBeforeTeleport;
    private float creditsSpeed;
    private float creditSpeedModifier;
    private float creditTime;


    [Header("해상도")]
    private int height = Screen.height;


    private void Start()
    {
        if(height == 1440)
        {
            creditSpeedModifier = 200;
            creditTime = 6500;
        }
        else if (height == 2160)
        {
            creditSpeedModifier = 300;
            creditTime = 10000;
        }
        else if (height == 1080)
        {
            creditSpeedModifier = 150;
            creditTime = 5000;
        }
        else
        {
            creditSpeedModifier = 150;
            creditTime = 5000;
        }

        // 스크립트 및 크레딧 텍스트 비활성화 상태로 시작
        enabled = false;
        creditsText.gameObject.SetActive(false);  // 크레딧 텍스트 비활성화
        ClickText.gameObject.SetActive(false); 
    }

    public void OnInteract()
    {
        // 오브젝트의 태그가 "Finish"이면 앤딩 시퀀스 시작
        if (gameObject.tag == "Finish")
        {
            enabled = true; // 스크립트 활성화
            promptText.SetActive(false);
            StartCoroutine(EndSequence());
        }
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            creditsSpeed = creditSpeedModifier * 1.5f;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            creditsSpeed = creditSpeedModifier * 1;
        }
    }

    IEnumerator EndSequence()
    {
        // 플레이어 비활성화
        if (player != null)
        {
            player.SetActive(false);
        }

        // 화면을 어두워지게 하는 과정
        float timer = 0;
        while (timer < delayBeforeTeleport)
        {
            timer += Time.deltaTime;
            float alpha = Mathf.Clamp01(timer / delayBeforeTeleport);
            fadePanel.color = new Color(255, 255, 255, alpha);
            yield return null;
        }

        // 화면 어두워지는 과정이 완료된 후 크레딧 텍스트 활성화
        creditsText.gameObject.SetActive(true);
        ClickText.gameObject.SetActive(true);

        // 크레딧 스크롤링
        float startY = creditsText.transform.position.y;
        float endY = startY + creditTime; // 스크롤할 높이 설정
        while (creditsText.transform.position.y < endY)
        {
            creditsText.transform.position += new Vector3(0, creditsSpeed * Time.deltaTime, 0);
            yield return null;
        }

        // 크레딧 스크롤링이 끝난 후 3초 대기
        yield return new WaitForSeconds(3);

        // 씬 재시작
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
