using UnityEngine;
//Made by Aiden
public class JumpPadScript : MonoBehaviour
{
    public float jumpVelocity = 10f; 

    public AudioSource jumpSource;
    public AudioClip jumpClip;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerMovement playerMovement = other.GetComponent<PlayerMovement>();

            if (playerMovement != null)
            {
                // Set upward velocity to the player when they touch the jump pad
                playerMovement.rb.velocity = new Vector2(playerMovement.rb.velocity.x, jumpVelocity);
                jumpSource.PlayOneShot(jumpClip);
            }
        }
    }
}
