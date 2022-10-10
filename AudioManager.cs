using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using System;

public class AudioManager : MonoBehaviour
{
    //setting variables
    public Sound[] sounds;

    public static AudioManager instance;

    private void Awake()
    {
        //checking if this is only audio manager in scene, if not destroy this one
        if (instance == null)
            instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }


        foreach (Sound s in sounds)
        { //looping through the sounds in the array and assigning the values
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
    }

    private void Start()
    {
        //seeing which music to play when scene loaded
        if (SceneManager.GetActiveScene().name == "mainmenu" || SceneManager.GetActiveScene().name == "levelselection")
            Play("Menu");
        else
            Play("Theme");
    }

    public void Play(string name)
    {
        foreach (Sound s in sounds)
        { //looping through the sounds and finding the referenced sound and playing it.
            if (s.name == name)
            {
                if (s == null)
                {
                    Debug.LogWarning("Sound: " + name + " not found!");
                    return;
                }
                //null check then play
                else
                    s.source.Play();
            }
        }
    }

    public void Stop(string name)
    {
        foreach (Sound s in sounds)
        { //looping checking if null then stopping
            if (s.name == name)
            {
                if (s == null)
                {
                    Debug.LogWarning("Sound: " + name + " not found!");
                    return;
                }
                else
                    s.source.Stop();
            }
        }

    }
}
