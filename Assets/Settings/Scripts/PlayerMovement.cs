using System;
using System.Numerics;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private UnityEngine.Vector2 movement;
    public Rigidbody2D rb;
    public float moveSpeed = 50f;

    public bool isGroundedVar;
    public bool isDashing;
    public float dashSpeed = 50f;

    public float dashCool = 2f;
    public UnityEngine.Vector2 boxSize;
    public float castDistance;
    public LayerMask groundLayer;

    
    void Start()
    {
        
    }
    // void OnCollisionEnter2D(Collision2D collision)
    // {
    //     if(collision.gameObject.tag == "Floor")
    //     {
    //         isGrounded = true;
    //         Debug.Log("Grounded");
    //     }
    //     else{
    //         Debug.Log(collision.gameObject.tag);
    //     }
        
        
    // }
    // void OnCollisionExit2D(Collision2D collision)
    // {
    //     if(collision.gameObject.tag == "Floor")
    //     {
    //         isGrounded = false;
    //         Debug.Log("Not Grounded");
    //     }
        
    // }

    public bool isGrounded() //called when trying to jump
    {
        if(Physics2D.BoxCast(transform.position, boxSize, 0f, -transform.up, castDistance, groundLayer))
        {//position and boxSize are just the parameters of cast,  groundLayer is the mask that the cast looks for to see if grounded
            //this means that you can only jump if on groundLayer, not sure how i like that, may change
            return true;
        }
        else
        {
            Debug.Log("Not Grounded");
            return false;
        }
    }

    private void OnDrawGizmos() //this just visualizes the boxcast in editoor
    {
        Gizmos.DrawWireCube(transform.position-transform.up * castDistance, boxSize);
    }

    // Update is called once per frame
    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal"); //this just reads a/d or left/right arrow keys for moving
        if(Input.GetKeyDown(KeyCode.Space) && isGrounded()) //when player hits space, checks if grounded
        {
            Jump(); //if grounded, jump
        }
        else if(Input.GetKeyDown(KeyCode.Space) && !isGrounded())
        {
            Debug.Log("Can't jump");
        }

        
        if (Input.GetKeyDown(KeyCode.LeftShift))// this is the dash
        {//i made it nested because timing shift and l/r was hard but this is kind of clunky
            if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
            {
                Dash("left"); //calls the actual dash function
            }
            else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
            {
                Dash("right");
            }
        }

        
       

        
    }
    void Dash(string direction)
    {
        isDashing = true; //need this to be able to stop dashing later
        // rb.AddForce(new UnityEngine.Vector2(50f, 0), ForceMode2D.Impulse);
        if(direction == "left")
        {
            rb.AddForce(new UnityEngine.Vector2(-dashSpeed, 0), ForceMode2D.Impulse);//dash left
            Debug.Log("Dash Left");
        }
        else if(direction == "right")
        {
            rb.AddForce(new UnityEngine.Vector2(dashSpeed, 0), ForceMode2D.Impulse); //dash riht
        }

        
        Invoke("stopDashing", 0.1f); //invokes so it has a delay, just calles the stopDashing func, the longer the delay, longer the dash
        
        
    }
    void stopDashing()
    {
        isDashing = false;
    }
    void Jump()
    {
        rb.AddForce(new UnityEngine.Vector2(0, 20f), ForceMode2D.Impulse); //actually juumps, will make the second parameter a variable later so its easier to change
        Debug.Log("Jump");
    }
    void FixedUpdate()
    {
        if(!isDashing) //i kinda forgot what the purpose of this was 
        {
            rb.linearVelocity = new UnityEngine.Vector2(movement.x * moveSpeed, rb.linearVelocity.y);
        }
        // rb.linearVelocity = new UnityEngine.Vector2(movement.x * moveSpeed, rb.linearVelocity.y);
        
    }
}
