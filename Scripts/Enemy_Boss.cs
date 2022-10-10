using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Boss : MonoBehaviour
{
    private Animator anim;
    [SerializeField] private GameObject enemy;
    private bool q1;
    private bool q2;
    private bool bossComplete;
    Questions Questions;
    private void Start()
    {
        //assigning animator component into variable to use
        anim = GetComponent<Animator>();
        Questions = GameObject.FindGameObjectWithTag("GameManager").GetComponent<Questions>();
        q1 = false;
        q2 = false;
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
            anim.SetTrigger("collision");
            //reference question script which shows the questions 
            while (bossComplete == false)
            {
                //loading questions making sure both are true
                //q1 = Questions.loadQuestion();
                //q2 = Questions.loadQuestion();

                if (q1 && q2)
                {
                    bossComplete = true;
                }
            }
        }
    }

    public void enemyExit()
    {
        StartCoroutine("exit");
    }

    IEnumerator exit()
    {
        //starting animation
        anim.SetTrigger("exit");

        //waiting for animation
        yield return new WaitForSeconds(1);

        //disabling current GameObject
        enemy.SetActive(false);
    }
}
