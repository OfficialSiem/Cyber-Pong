using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[DefaultExecutionOrder(-98)]
public class GameScenesManager : SingletonPersistent<GameScenesManager>
{
    public delegate void WhenSceneChangesTo(int number);
    public static event WhenSceneChangesTo OnSceneChange;
    public enum Scene
    {
        MainMenu,
        PongMatch,
        Leaderboard
    }

    public override void Awake()
    {
        base.Awake();
        /*if(SystemInfo.deviceType == DeviceType.Handheld)
        {
            //This is a touch screen device
        }
        else
        {
            //We are on a computer
        }*/
    }

    public int sceneNumber = 0;

    public void LoadScene(Scene scene)
    {
        SceneManager.LoadScene(scene.ToString());
    }

    public void LoadScene(int sceneNumber)
    {
        SceneManager.LoadScene(sceneNumber);
    }

    public void PlayPongAgain()
    {
        SceneManager.LoadScene(Scene.PongMatch.ToString());
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene(Scene.MainMenu.ToString());
    }

    public void LoadLeaderboard()
    {
        SceneManager.LoadScene(Scene.Leaderboard.ToString());
    }

    public void LoadNextScene()
    {
        int currentScene = SceneManager.GetActiveScene().buildIndex;
        sceneNumber++;
        SceneManager.LoadScene(currentScene + 1);
        OnSceneChange(currentScene + 1);
    }
    public void LoadPreviousScene()
    {
        int currentScene = SceneManager.GetActiveScene().buildIndex;
        sceneNumber--;
        SceneManager.LoadScene(currentScene - 1);
        OnSceneChange(currentScene - 1);
    }

}
