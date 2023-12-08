using UnityEngine;

public class JumpPadScript : MonoBehaviour
{
    public float jumpForce = 15f;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerMovement playerMovement = other.GetComponent<PlayerMovement>();

            if (playerMovement != null)
            {
                // Apply upward force to the player when they touch the jump pad
                playerMovement.rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            }
        }
    }
}
