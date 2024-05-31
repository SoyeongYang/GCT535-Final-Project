using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    private string currentSceneName;

    private void Start() {
     currentSceneName = SceneManager.GetActiveScene().name;   
    }

    public void LevelSceneChange()
    {
        if (currentSceneName == "StartScene")
            SceneManager.LoadScene("LevelScene");
            
        // else if (currentSceneName == "LevelScene")
        //     SceneManager.LoadScene("MainScene");
    }

    public void ExplainSceneChange()
    {
        if (currentSceneName == "StartScene")
            SceneManager.LoadScene("ExplainScene");
    }

    public void IntroSceneChange()
    {
        if (currentSceneName == "ExplainScene")
            SceneManager.LoadScene("IntroScene");
    }

    public void StartSceneChange()
    {
        if (currentSceneName == "IntroScene")
            SceneManager.LoadScene("StartScene");
    }

    public void MainSceneChange()
    {
        if (currentSceneName == "LevelScene")
            SceneManager.LoadScene("MainScene");
    }
}
