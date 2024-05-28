using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CountdownTimer : MonoBehaviour
{
    public TMP_Text timerText; // 디지털 시계를 표시할 텍스트 UI
    private float countdownTime = 15f; // 카운트다운 시간 (초)

    void Start()
    {
        StartCoroutine(StartCountdown());
    }

    IEnumerator StartCountdown()
    {
        while (countdownTime > 0)
        {
            // 시간 업데이트
            countdownTime -= Time.deltaTime;
            // 시간 포맷팅 (시:분:초)
            int minutes = Mathf.FloorToInt(countdownTime / 60F);
            int seconds = Mathf.FloorToInt(countdownTime % 60F);
            int milliseconds = Mathf.FloorToInt((countdownTime * 100F) % 100F);
            timerText.text = string.Format("{0:00}:{1:00}:{2:00}", minutes, seconds, milliseconds);
            yield return null;
        }

        // 카운트다운이 완료되었을 때
        timerText.text = "00:00:00";
    }
}
