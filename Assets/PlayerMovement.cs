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

    public GameObject leftParticle;
    public GameObject rightParticle;

    public GameObject jumpParticle;

    public Rigidbody2D rb;

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
            rb.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
            Instantiate(jumpParticle, transform.position, transform.rotation);
        }

        if(Input.GetKey(rightKey) && isGrounded){
            rb.AddForce(transform.right * walkSpeed);
            rightParticle.SetActive(true);
            leftParticle.SetActive(false);
        } else if(Input.GetKey(rightKey) && !isGrounded)
        {
            rb.AddForce(transform.right * airWalkSpeed);
        }

        if(Input.GetKey(leftKey)){
            rb.AddForce(transform.right * (walkSpeed * -1));
            rightParticle.SetActive(false);
            leftParticle.SetActive(true);
        } else if(Input.GetKey(leftKey) && !isGrounded)
        {
            rb.AddForce(transform.right * (airWalkSpeed * -1));
        }

        if(rb.velocity.magnitude > maxSpeed){
            rb.velocity = rb.velocity.normalized * maxSpeed;
        }

        if(!Input.GetKeyDown(leftKey) && !Input.GetKeyDown(rightKey)){
            rightParticle.SetActive(false);
            leftParticle.SetActive(false);
        }
    }
}
