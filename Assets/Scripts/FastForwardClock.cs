using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FastForwardClock : MonoBehaviour
{
    public TMP_Text clockText; // 시계를 표시할 TextMeshPro 텍스트
    private float gameTime; // 게임 속 시간 (초 단위로 계산)
    private float startTimeInMinutes = 360f; // 시작 시간 (오전 6시, 분 단위: 6 * 60)
    private float endTimeInMinutes = 720f; // 끝 시간 (오후 12시, 분 단위: 12 * 60)
    private float timeMultiplier = 60f; // 게임 속 1분이 실제 1초에 해당

    void Start()
    {
        gameTime = startTimeInMinutes * 60f; // 시작 시간 (초 단위로 변환)
        StartCoroutine(UpdateClock());
    }

    System.Collections.IEnumerator UpdateClock()
    {
        while (gameTime < endTimeInMinutes * 60f)
        {
            gameTime += Time.deltaTime * timeMultiplier;

            int hours = Mathf.FloorToInt((gameTime / 3600f) % 24);
            int minutes = Mathf.FloorToInt((gameTime / 60f) % 60);
            clockText.text = string.Format("{0:00}:{1:00}", hours, minutes);

            yield return null;
        }

        // 시간이 끝나면 멈추거나 추가적인 행동을 수행할 수 있습니다.
    }
}
