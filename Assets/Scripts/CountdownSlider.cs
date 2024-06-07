using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountdownSlider : MonoBehaviour
{
    public Slider timeSlider; // 슬라이더 UI
    private float countdownTime = 15f; // 카운트다운 시간 (초)
    private float currentTime;

    private Coroutine currentSystem;

    public GameManager gameManager;

    public void Start()
    {
        SetCountdown(gameManager.timeGap);
        timeSlider.maxValue = countdownTime;
        timeSlider.value = countdownTime;
    }

    IEnumerator StartCountdown()
    {
        currentTime = countdownTime;
        while (currentTime > 0)
        {
            // 시간 업데이트
            currentTime -= Time.deltaTime;
            timeSlider.value = currentTime;
            yield return null;
        }

        // 카운트다운이 완료되었을 때
        timeSlider.value = 0;
    }

    public void StopCountdown()
    {
        if (currentSystem != null)
        {
            Debug.Log("Time Limit Stop");
            StopCoroutine(currentSystem);
            timeSlider.value = 0;
            currentSystem = null;
        }
    }

    public void StartCountdown(float countdown)
    {
        StopCountdown();
        countdownTime = countdown;
        timeSlider.maxValue = countdownTime;
        timeSlider.value = countdownTime;
        currentSystem = StartCoroutine(StartCountdown());
    }

    public void SetCountdown(float countdown)
    {
        countdownTime = countdown;
    }
}
