using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    public Animator anim;
    [SerializeField] private string testTrigger = "start";
    [SerializeField] private string testLevel = "mainmenu";
    private int waitingTime;

    private GameObject score;

    private void Start()
    {
        score = GameObject.FindGameObjectWithTag("Score");
        if (score == null)
        {

        }
        else
            score = score.gameObject;
    }
    
    void Update()
    {
        //a test button for development only to go back to mainmenu
        if (Input.GetKeyDown(KeyCode.F))
        {
            StartCoroutine(LoadLevel(testTrigger, testLevel, 2));
        }
    }

    public void callLoadScene(string newTrigger, string newLevel)
    {
        //checking the name of current level to decide waiting time for transition
        if (SceneManager.GetActiveScene().name == "mainmenu" || SceneManager.GetActiveScene().name == "levelselection")
            waitingTime = 1;
        else
            waitingTime = 2;
            StartCoroutine(LoadLevel(newTrigger, newLevel, waitingTime));
        //calling coroutine passing in parameters
    }

    IEnumerator LoadLevel(string trigger, string level, int time)
    {
        //starting animation
        anim.SetTrigger(trigger);
        if (!(SceneManager.GetActiveScene().name == "mainmenu" || SceneManager.GetActiveScene().name == "levelselection"))
            score.SetActive(false);
        //waiting given amount of time
        yield return new WaitForSeconds(time);
        //changing the scene
        SceneManager.LoadScene(level);
        
        if (SceneManager.GetActiveScene().name == "mainmenu" || SceneManager.GetActiveScene().name == "levelselection")
        {   //setting music when switching scenes
            FindObjectOfType<AudioManager>().Stop("Theme");
            FindObjectOfType<AudioManager>().Play("Menu");
        }
        
        else
        {
            if (!(FindObjectOfType<AudioManager>() == null))
            {   //changing music and showing score - checking for null
                score.SetActive(true);
                FindObjectOfType<AudioManager>().Play("Theme");
                FindObjectOfType<AudioManager>().Stop("Menu");
            }

        }
            
    }
}
