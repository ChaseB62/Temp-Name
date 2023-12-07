using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //this epic movement script was made by Chase
    public KeyCode upKey;
    public KeyCode rightKey;
    public KeyCode leftKey;

    public LayerMask ground;

    public bool isGrounded;
    public bool isMoving;

    public Collider2D groundDetector;

    public float jumpForce = 100f;
    public float walkSpeed = 10f;

    public float maxSpeed = 10f;

    public float airWalkSpeed = 3f;

    public Rigidbody2D rb;

    private particleHandler particleHandler;

    void Start(){
        particleHandler = GetComponent<particleHandler>();
    }

    void Update()
    {
        if(groundDetector.IsTouchingLayers(ground)){
            isGrounded = true;
            Debug.Log("touching ground");
        } else {
            isGrounded = false;
            Debug.Log("not touching ground");
        }

        if(Input.GetKeyDown(upKey) && isGrounded)
        {
            Debug.Log("jamped");
            particleHandler.Jump();
            rb.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
        }

        if(Input.GetKey(rightKey) && isGrounded){
            rb.AddForce(transform.right * walkSpeed);
        } else if(Input.GetKey(rightKey) && !isGrounded)
        {
            rb.AddForce(transform.right * airWalkSpeed);
        }

        if(Input.GetKey(leftKey)){
            rb.AddForce(transform.right * (walkSpeed * -1));
        } else if(Input.GetKey(leftKey) && !isGrounded)
        {
            rb.AddForce(transform.right * (airWalkSpeed * -1));
        }

        if(rb.velocity.magnitude > maxSpeed){
            rb.velocity = rb.velocity.normalized * maxSpeed;
        }
    }
}
