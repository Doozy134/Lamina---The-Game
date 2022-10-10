using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovement : MonoBehaviour
{
    //setting appropriate variables at beginning
    [SerializeField] private float movementSpeed;
    [SerializeField] private float jumpSpeed;

    private bool groundContact;
    private Rigidbody2D body;
    private BoxCollider2D boxCollider;
    private Animator anim;

    [SerializeField] private float movePlayerDistance;
    private float tempSpeed;

    void Start()
    {
        // getting references to components of character
        body = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        /* ATTEMPT at integrating own algorithm
        pos_x = body.velocity.x;
        pos_y = body.velocity.y;

        // || meaning or for conditions
        if ((Input.GetKey("d")) || (Input.GetKey("right"))){
            body.velocity = (new Vector2(movementSpeed, pos_y) * Time.deltaTime);
        }
        if ((Input.GetKey("a")) || (Input.GetKey("left"))){
            body.velocity = (new Vector2(-movementSpeed, pos_y) * Time.deltaTime);
        }
        if ((Input.GetKey("w")) || (Input.GetKey("up"))){
            body.velocity = (new Vector2(pos_x,jumpSpeed) * Time.deltaTime);
        }*/

        float horizontalInput = Input.GetAxis("Horizontal");
        //Getting the horizontal input (between 1 or -1) and setting a new velocity vector
        body.velocity = new Vector2(horizontalInput * movementSpeed, body.velocity.y);

        //setting the character to face direction of travel using my scale factor for the character
        if (horizontalInput > 0.01f)
            transform.localScale = new Vector3(6,6,6);
        else if (horizontalInput < -0.01f)
            transform.localScale = new Vector3(-6,6,6);

        //now checking for jumping condition
        if ((Input.GetKeyDown(KeyCode.UpArrow)) || (Input.GetKeyDown("w")))
            jump();

        //setting animation parameters
        anim.SetBool("run", horizontalInput != 0);
        anim.SetBool("grounded", groundContact);
        
        if (Input.GetKeyDown(KeyCode.P))
        {
            collisionMovePlayer();
        }
    }

    private void jump()
    {
        //setting new velocity vector for jumping and checking that character is touching ground
        if (groundContact == true){
            body.velocity = new Vector2(body.velocity.x, jumpSpeed);
            anim.SetTrigger("jump"); //setting jump animation
        }
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "GroundObject")
        {
            groundContact = true;
            anim.SetBool("grounded", true);
        }
            
        //checking if character is colliding with ground
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "GroundObject")
            groundContact= false;
        //detecting when the character is no longer grounded
    }

    public void collisionMovePlayer()
    {
        //move the player slightly to the left by translating it
        body.transform.Translate(movePlayerDistance,0,0);
    }

    public void changeMovmentSpeed(float speed, int time)
    {
        StartCoroutine(changeSpeed(speed, time));
    }
    IEnumerator changeSpeed(float speedFactor, int speedTime)
    {
        //keep current value in temporary variable
        tempSpeed = movementSpeed;

        //set new movement speed
        movementSpeed *= speedFactor;

        //wait the given time
        yield return new WaitForSeconds(speedTime);

        //return speed back to original level
        movementSpeed = tempSpeed;
    }
}
