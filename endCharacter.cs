using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class endCharacter : MonoBehaviour
{
    public LevelLoader LevelLoader;
    private string trigger = "start";

    public void changeScene(string sceneName)
    {
        // loading the new scene with the parameter passed in 
        // and setting animation trigger
        LevelLoader.callLoadScene(trigger, sceneName);

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        changeScene("mainmenu");
    }
}
