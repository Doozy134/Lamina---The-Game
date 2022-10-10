using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreChange : MonoBehaviour
{
    [SerializeField] private GameObject manager;
    [SerializeField] private GameObject item;

    [SerializeField] private int lowerScoreBound;
    [SerializeField] private int upperScoreBound;

    private Animator anim;
    private int randomScore;

    private Score scoreScript;
    private void Start()
    {
        //get references
        anim = GetComponent<Animator>();
        scoreScript = GameObject.FindGameObjectWithTag("GameManager").GetComponent<Score>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //if collision with player call function
        if (collision.gameObject.tag == "Player")
        {
            //generating random score to be added/subtracted
            randomScore = Random.Range(lowerScoreBound, upperScoreBound);
            StartCoroutine(fadeAway(randomScore));
        }
    }

    private IEnumerator fadeAway(int randomNum)
    {
        //setting animation
        anim.SetTrigger("exit");

        FindObjectOfType<AudioManager>().Play("Item");

        //waiting for animation to finish
        yield return new WaitForSeconds(1);

        //calling function adding score
        scoreScript.addScore(randomNum);
        Debug.Log("Adding score:");
        Debug.Log(randomNum);

        //disabling item
        item.SetActive(false);
    }
}
