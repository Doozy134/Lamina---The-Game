using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedUp_Apple : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private float speedFactor;
    [SerializeField] private int time;

    [SerializeField] private GameObject item;
    private Animator anim;


    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //if collision with player then call function
        if (collision.gameObject.tag == "Player")
        {
            StartCoroutine(fadeAway());
        }
    }

    private IEnumerator fadeAway()
    {
        //setting animation
        anim.SetTrigger("exit");

        FindObjectOfType<AudioManager>().Play("Item");

        //waiting for animation to finish
        yield return new WaitForSeconds(1);

        //calling function changing speed
        player.GetComponent<playerMovement>().changeMovmentSpeed(speedFactor, time);

        //disabling item
        item.SetActive(false);
    }
}
