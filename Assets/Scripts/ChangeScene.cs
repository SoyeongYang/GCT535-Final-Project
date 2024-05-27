using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    public void SceneChange()
    {
        string currentSceneName = SceneManager.GetActiveScene().name;

        if (currentSceneName == "StartScene")
            SceneManager.LoadScene("LevelScene");
            
        else if (currentSceneName == "LevelScene")
            SceneManager.LoadScene("MainScene");
    }
}
