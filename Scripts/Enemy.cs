using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Animator anim;
    [SerializeField] private GameObject enemy;

    Questions Questions;

    private void Start()
    {
        //assigning animator component into variable to use
        anim = GetComponent<Animator>();
        Questions = GameObject.FindGameObjectWithTag("GameManager").GetComponent<Questions>();
    }

    private void Update()
    {
        //using a keybind for development
        if (Input.GetKeyDown(KeyCode.G))
        {
            enemyExit();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //checking for collision with player then setting animations
        if (collision.gameObject.tag == "Player")
        {
            FindObjectOfType<AudioManager>().Play("Collision");
            anim.SetTrigger("collision");
            //reference question script which shows the questions 
            Questions.loadQuestion(gameObject);
        }
    }

    public void enemyExit()
    {
        StartCoroutine ("exit");
    }

    IEnumerator exit()
    {
        //starting animation
        anim.SetTrigger("exit");

        //waiting for animation
        yield return new WaitForSeconds(1);

        //disabling current GameObject
        gameObject.SetActive(false);
    }
}
