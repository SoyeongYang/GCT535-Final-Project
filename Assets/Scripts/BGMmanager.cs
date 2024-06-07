using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BGMmanager : MonoBehaviour
{
    public AK.Wwise.Event backgroundMusicEvent;
    private static BGMmanager instance;
    public List<string> scenesToDestroyOn;

    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(this.gameObject);  // 씬 전환 시 파괴되지 않도록 설정

        SceneManager.sceneLoaded += OnSceneLoaded;  // 씬 로드 이벤트 구독
    }

    void Start()
    {
        // 배경 음악 시작
        backgroundMusicEvent.Post(gameObject);
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Debug.Log("Scene loaded: " + scene.name);

        if (scenesToDestroyOn.Contains(scene.name))  // 특정 이름을 가진 씬이 로드될 때 이 오브젝트 파괴
        {
            // Debug.Log("Destroying BGMmanager for scene: " + scene.name);
            backgroundMusicEvent.Stop(gameObject);
            Destroy(this.gameObject);
        }
    }

    void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded; // 씬 로드 이벤트 구독 해제
    }
}
