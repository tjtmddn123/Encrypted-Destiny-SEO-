using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace NavKeypad
{
    public class KeypadButton : MonoBehaviour
    {
        [Header("Value")]
        [SerializeField] private string value;  //입력한 값
        [Header("Button Animation Settings")]
        [SerializeField] private float bttnSpeed = 0.1f;
        [SerializeField] private float moveDist = 0.0025f;
        [SerializeField] private float buttonPressedTime = 0.1f;
        [Header("Component References")]
        [SerializeField] private Keypad keypad;


        public void PressButton()
        {
            if (!moving)
            {
                keypad.AddInput(value);  //입력한 값 받아서 AddInput, 입련한 비밀번호 정보 전달
                StartCoroutine(MoveSmooth());
            }
        }
        private bool moving;

        //아래 코루틴은 연타 방지 및 버튼 에니메이션 관리
        private IEnumerator MoveSmooth()
        {

            moving = true;
            Vector3 startPos = transform.localPosition;
            Vector3 endPos = transform.localPosition + new Vector3(0, 0, moveDist);

            float elapsedTime = 0;
            while (elapsedTime < bttnSpeed)
            {
                elapsedTime += Time.deltaTime;
                float t = Mathf.Clamp01(elapsedTime / bttnSpeed);

                transform.localPosition = Vector3.Lerp(startPos, endPos, t);

                yield return null;
            }
            transform.localPosition = endPos;
            yield return new WaitForSeconds(buttonPressedTime);
            startPos = transform.localPosition;
            endPos = transform.localPosition - new Vector3(0, 0, moveDist);

            elapsedTime = 0;
            while (elapsedTime < bttnSpeed)
            {
                elapsedTime += Time.deltaTime;
                float t = Mathf.Clamp01(elapsedTime / bttnSpeed);

                transform.localPosition = Vector3.Lerp(startPos, endPos, t);

                yield return null;
            }
            transform.localPosition = endPos;

            moving = false;
        }
    }
}