using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Animator animator;
    Rigidbody2D rb2d;
    SpriteRenderer spriteRenderer;
    //SurfaceEffector2D surfaceEffector2D;


    [Header("Movement Settings")]
    [SerializeField] float torqueAmount = 10f;
    [SerializeField] float jumpForce = 12f;
    [SerializeField] float walkSpeed = 5f;
    [SerializeField] float slideForce = 50f;

    [Header("Shooting")]
    [SerializeField] GameObject snowball;
    [SerializeField] Transform SpawnPlace;


    [Header("GroundCheck")]
    [SerializeField] Transform groundCheck;
    [SerializeField] float groundCheckRadius = 0.3f;
    [SerializeField] LayerMask groundLayer;
    bool isGrounded = false;

    bool isSliding = false;
    bool isJumping = false;
    bool canMove = true;


    void Start()
    {
        animator = GetComponent<Animator>();
        rb2d = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        //surfaceEffector2D = FindObjectOfType<SurfaceEffector2D>();
    }

    void Update()
    {
        if(canMove)
        {
            movePlayer();
            shoot();
        }
    }


    /* void OnCollisionEnter2D(Collision2D other) 
    {
        if(other.gameObject.tag == "Ground")
        {
            surfaceEffector2D = other.gameObject.GetComponent<SurfaceEffector2D>();
        }
        
    } */

    public void DisableControls()
    {
        canMove = false;
    }

    
        
    void movePlayer()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) && !isJumping)
        {
            isSliding = !isSliding;
            animator.SetBool("isSliding", isSliding);
            animator.SetBool("isWalking", false);
        }

        if (isSliding)
        {
            Slide();
            Rotate();
            RespondToSlideJump();
            
        }
        else
        {
            Walk();
            RespondToJump();
        }
    }

    void Walk()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
            if(horizontalInput != 0 && !isJumping)
            {
                animator.SetBool("isWalking", true);

                // Move the penguin left or right
                rb2d.velocity = new Vector2(horizontalInput * walkSpeed, rb2d.velocity.y);
                
                // Flip the sprite if moving left
                if (horizontalInput < 0f)
                {
                    spriteRenderer.flipX = true;
                }
                // Flip the sprite if moving right
                else if (horizontalInput > 0f)
                {
                    spriteRenderer.flipX = false;
                }
            }
            else
            {
                animator.SetBool("isWalking", false);
            }
    }

    void RespondToJump()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        if (isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            rb2d.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
            animator.SetTrigger("takeOff");
            isJumping = true;
            animator.SetBool("isJumping", false);
            animator.SetBool("isJumping", true);
        }
        else if(isGrounded)
        {
            animator.SetBool("isJumping", rb2d.velocity.y > 0f);
            isJumping = false;
        }
             
    }

    void Slide()
    {
        float verticalInput = Input.GetAxis("Vertical");
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        //Debug.Log(Time.fixedDeltaTime * slideForce * verticalInput+ "  "+ Time.fixedDeltaTime + "  " + verticalInput + " "+ slideForce * verticalInput);

        if(verticalInput != 0 && isGrounded)
        {
            rb2d.AddForce(new Vector2(slideForce * verticalInput * Time.deltaTime , 0f), ForceMode2D.Impulse);
        }
    }

    void RespondToSlideJump()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
        if (isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            rb2d.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
        }
    }

    void Rotate()
    {
        if(Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            rb2d.AddTorque(torqueAmount);
        }
        else if(Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            rb2d.AddTorque(-torqueAmount);
        }
    }


    void shoot()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Instantiate(snowball, SpawnPlace.position, Quaternion.identity);
        }
        
    }
    
}
