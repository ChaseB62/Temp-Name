using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public KeyCode upKey;
    public KeyCode rightKey;
    public KeyCode leftKey;

    public float jumpForce = 10f;
    public float walkSpeed = 10f;

    public Rigidbody2D rb;

    void Update()
    {
        if(Input.GetKey(upKey))
        {
            rb.AddForce(transform.up * jumpForce);
        }
    }
}
