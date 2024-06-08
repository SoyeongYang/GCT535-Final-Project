using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class GameManager : MonoBehaviour
{
    [Header("Game System")]
    public int characterNumber;
    public float score;
    public float timeGap;
    public int stage;
    public List<GameObject> targetCharacters = new List<GameObject>();
    public bool alarmPlay;

    [Header("Positions of gameobjects")]
    public GameObject positionCharacters;
    public GameObject positionStuffs;
    public GameObject positionSmall;
    public GameObject positionMedium;
    public GameObject positionLarge;

    [Header("List of gameobjects")]
    public List<GameObject> Small = new List<GameObject>();
    public List<GameObject> Medium = new List<GameObject>();
    public List<GameObject> Large = new List<GameObject>();
    public List<GameObject> Characters = new List<GameObject>();

    private Transform[] positions_small, positions_medium, positions_large, positions_character;

    [Header("Random ranges")]
    public float rangeCharacterMax;
    public float rangeCharacterMin;
    public float rangeStuffMax;
    public float rangeStuffMin;
    public int rangeVolumeMax;
    public int rangeVolumeMin;

    [Header("Canvas")]
    public FastForwardClock fastForwardClock;
    public CountdownSlider countdownSlider;

    [Header("Time")]
    public List<float> times;

    [Header("UI Elements")]
    public TextMeshProUGUI member1;  // 첫 번째 사람
    public TextMeshProUGUI member2;  // 두 번째 사람
    public TextMeshProUGUI member3;  // 세 번째 사람

    [Header("Alarm Spawner")]
    public AlarmSoundSpawn alarmSoundEvent1;  // 첫 번째 알람
    public AlarmSoundSpawn alarmSoundEvent2;  // 두 번째 알람
    public AlarmSoundSpawn alarmSoundEvent3;  // 세 번째 알람

    int InitSetting(int lastNum, List<GameObject> targetObjs)
    {
        int i = lastNum;
        foreach (GameObject obj in targetObjs)
        {
            obj.GetComponent<StuffManager>().uniqueNum = i;
            i++;
        }
        return i;
    }

    void InGameSetting()
    {
        // For Object
        int num = 0;
        num = InitSetting(num, Small);
        num = InitSetting(num, Medium);
        num = InitSetting(num, Large);

        targetCharacters = new List<GameObject>(Characters);
        Shuffle(targetCharacters);

        List<int> numberList = RandomList(Characters.Count, Characters.Count - characterNumber);
        foreach (int i in numberList)
        {
            GameObject character = Characters[i];
            GameObject[] stuffs = character.GetComponent<CharacterManager>().Stuffs;
            foreach (GameObject obj in stuffs)
            {
                int uniqueNum = obj.GetComponent<StuffManager>().uniqueNum;
                obj.SetActive(false);
            }
            character.SetActive(false);
            targetCharacters.RemoveAll(item => item == character);
        }

        int s = 0;
        foreach (GameObject obj in targetCharacters)
        {
            obj.GetComponent<CharacterManager>().stageNum = s;
            s++;
        }

        // For Time
        float startTime = fastForwardClock.getStartTime();
        for (int i = 0; i < characterNumber; i++)
        {
            times[i] = startTime + timeGap * (i + 1);
        }

        UpdateUIElements();
        UpdateAlarmPositions(); // 알람 포지션 업데이트
    }

    void UpdateUIElements()
    {
        if (member1 != null && targetCharacters.Count > 0)
            member1.text = $"{FormatTime(times[0])}   {targetCharacters[0].name}";

        if (member2 != null && targetCharacters.Count > 1)
            member2.text = $"{FormatTime(times[1])}   {targetCharacters[1].name}";

        if (member3 != null && targetCharacters.Count > 2)
            member3.text = $"{FormatTime(times[2])}   {targetCharacters[2].name}";
    }

    string FormatTime(float timeInMinutes)
    {
        int hours = Mathf.FloorToInt((timeInMinutes / 60f) % 24);
        int minutes = Mathf.FloorToInt(timeInMinutes % 60);
        return string.Format("{0:00}:{1:00}", hours, minutes);
    }

    void Shuffle<T>(List<T> list)
    {
        int count = list.Count;
        System.Random ptr = new System.Random();

        for (int i = count - 1; i > 0; i--)
        {
            int swapIndex = ptr.Next(i + 1);
            T temp = list[i];
            list[i] = list[swapIndex];
            list[swapIndex] = temp;
        }
    }

    List<int> RandomList(int length, int limit)
    {
        List<int> numberList = new List<int>(length);

        for (int i = 0; i < length; i++)
        {
            numberList.Add(i);
        }

        Shuffle(numberList);

        numberList = numberList.GetRange(0, limit);
        return numberList;
    }

    void InitObj(Transform[] targetPositions, List<GameObject> targetObjects, bool Character = false)
    {
        List<int> numberlist = RandomList(targetPositions.Length, targetObjects.Count);

        int i = 0;
        foreach (GameObject obj in targetObjects)
        {
            obj.transform.position = targetPositions[numberlist[i]].position;
            if (!Character) { obj.transform.position += obj.GetComponent<StuffManager>().initPosition; }
            obj.transform.rotation = targetPositions[numberlist[i]].rotation;
            i++;
        }
    }

    void Start()
    {
        positions_small = positionSmall.GetComponentsInChildren<Transform>()[1..];
        InitObj(positions_small, Small);
        positions_medium = positionMedium.GetComponentsInChildren<Transform>()[1..];
        InitObj(positions_medium, Medium);
        positions_large = positionLarge.GetComponentsInChildren<Transform>()[1..];
        InitObj(positions_large, Large);
        positions_character = positionCharacters.GetComponentsInChildren<Transform>()[1..];
        InitObj(positions_character, Characters, true);
        InGameSetting();
    }

    void UpdateAlarmPositions()
    {
        if (targetCharacters.Count > 0) alarmSoundEvent1.transform.position = targetCharacters[0].transform.position;
        if (targetCharacters.Count > 1) alarmSoundEvent2.transform.position = targetCharacters[1].transform.position;
        if (targetCharacters.Count > 2) alarmSoundEvent3.transform.position = targetCharacters[2].transform.position;
    }

    void StopAndTurnOffAlarm(int stage)
    {
        switch (stage - 1)
        {
            case 0:
                alarmSoundEvent1.StopAlarm();
                break;
            case 1:
                alarmSoundEvent2.StopAlarm();
                break;
            case 2:
                alarmSoundEvent3.StopAlarm();
                break;
        }

        alarmPlay = false;
    }

    public void PlayerCollidedWithCharacter(CharacterManager characterManager)
    {
        // Debug.Log("Player collided with character: " + characterManager.name);
        if (Input.GetKeyDown(KeyCode.E)) {
            if (characterManager.stageNum == (stage - 1))
            {
                StopAndTurnOffAlarm(stage);
            }
        }
    }


    void AutoAlarm()
    {
        float currentTime = fastForwardClock.getGameTime();
        if (stage == characterNumber)
        {
            EndGame();
        }
        else if (currentTime > times[stage] * fastForwardClock.getTimeMultiplier())
        {
            stage++;
            countdownSlider.StopCountdown();
            countdownSlider.StartCountdown(timeGap);
            alarmPlay = true;
        }

        if (alarmPlay)
        {
            switch (stage - 1)
            {
                case 0:
                    alarmSoundEvent1.StartAlarm();
                    alarmSoundEvent2.StopAlarm();
                    alarmSoundEvent3.StopAlarm();
                    break;
                case 1:
                    alarmSoundEvent1.StopAlarm();
                    alarmSoundEvent2.StartAlarm();
                    alarmSoundEvent3.StopAlarm();
                    break;
                case 2:
                    alarmSoundEvent1.StopAlarm();
                    alarmSoundEvent2.StopAlarm();
                    alarmSoundEvent3.StartAlarm();
                    break;
            }
        }
    }

    // public void SelfAlarm()
    // {
    //     if (Input.GetKeyDown(KeyCode.E))
    //     {
    //         foreach (GameObject triggerCharacter in targetCharacters)
    //         {
    //             if (stage == triggerCharacter.GetComponent<CharacterManager>().stageNum)
    //             {
    //                 StopAndTurnOffAlarm(stage);
    //                 break; // 조건을 만족하면 반복문을 중지합니다.
    //             }
    //         }
    //     }
    // }

    // Update is called once per frame
    void Update()
    {
        AutoAlarm();
        // SelfAlarm();
        UpdateAlarmPositions(); // 알람 포지션 업데이트
    }

    void EndGame()
    {
        #if UNITY_EDITOR
        EditorApplication.isPlaying = false;
        #else
        Application.Quit();
        #endif
    }
}
