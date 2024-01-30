using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SW_Loop : MonoBehaviour
{
    public Transform loopEndPosition; // 루프 앤드 위치
    public float delayBeforeTeleport; // 플레이어가 텔레포트되기 전 지연 시간
    public Image fadePanel; // 화면을 어두워지게 할 UI 패널

    private void OnTriggerEnter(Collider other)
    {
        // 트리거에 플레이어가 진입했을 때 실행
        if (other.CompareTag("Player"))
        {
            // 플레이어와의 충돌을 감지하면 코루틴 실행
            StartCoroutine(FadeScreenAndTeleport(other.gameObject));
        }
    }

    IEnumerator FadeScreenAndTeleport(GameObject player)
    {
        // 움직임 비활성화
        CharacterController controller = player.GetComponent<CharacterController>();
        if (controller != null)
        {
            controller.enabled = false;
        }

        // 화면을 어두워지게 하는 과정
        float timer = 0;
        while (timer < delayBeforeTeleport)
        {
            timer += Time.deltaTime;
            // 화면 어두움 정도를 지연시간에 따라 조절
            float alpha = Mathf.Clamp01(timer / delayBeforeTeleport);
            fadePanel.color = new Color(0, 0, 0, alpha); // 패널의 투명도 변경
            yield return null;
        }

        // 플레이어 위치 이동
        player.transform.position = loopEndPosition.position;
        Debug.Log("플레이어 위치를 루프 앤드 위치로 변경했습니다.");

        // 화면을 다시 밝게 만드는 과정
        timer = 0;
        while (timer < delayBeforeTeleport)
        {
            timer += Time.deltaTime;
            // 화면 밝기 회복 정도를 지연시간에 따라 조절
            float alpha = Mathf.Clamp01(1 - (timer / delayBeforeTeleport));
            fadePanel.color = new Color(0, 0, 0, alpha); // 패널의 투명도 변경
            yield return null;
        }

        // 움직임 다시 활성화
        if (controller != null)
        {
            controller.enabled = true;
        }
    }
}
