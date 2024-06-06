using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
    public List<GameObject> Small= new List<GameObject>();
    public List<GameObject> Medium= new List<GameObject>();
    public List<GameObject> Large= new List<GameObject>();
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

        List<int> numberList = RandomList(Characters.Count, Characters.Count-characterNumber);
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
        alarmPlay = true;
        float startTime = fastForwardClock.getStartTime();
        for (int i = 0; i < characterNumber; i++)
        {
            times[i] = startTime + timeGap * (i+1);
        }
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
    
    void InitObj(Transform[] targetPositions, List<GameObject> targetObjects, bool Character=false)
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
            countdownSlider.Start();
            alarmPlay = true;
        }
    }

    // public void SelfAlarm()
    // {
    //     GameObject triggerCharacter;
    //     if (Input.GetKeyDown(KeyCode.R) & (stage == triggerCharacter.GetComponent<CharacterManager>().stageNum))
    //     {
    //         // 캐릭터와 trigger 생겼을 경우 
    //         alarmPlay = false;
    //     }
    // }

    // Update is called once per frame
    void Update()
    {
        AutoAlarm();
        // SelfAlarm();
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
