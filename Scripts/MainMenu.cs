using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public LevelLoader LevelLoader;
    private string trigger = "start";

    public void changeScene(string sceneName)
    {
        // loading the new scene with the parameter passed in 
        // and setting animation trigger
        LevelLoader.callLoadScene(trigger, sceneName);

    }

    public void quit()
    {
        // when clicked the game will close
        Debug.Log("Quitting the game...");
        Application.Quit();
    }

}
