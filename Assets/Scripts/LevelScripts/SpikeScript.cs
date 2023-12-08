using UnityEngine;
//made by Aidem
public class SpikeScript : MonoBehaviour
{
    public int spikeDamage = 10;
    public playerHealth playerHealthScript; // Reference to playerHealth script

    // Called when something enters the collider attached to this GameObject
    void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the colliding object is the player
        if (other.CompareTag("Player"))
        {
            Debug.Log("collided");
            playerHealthScript = other.GetComponent<playerHealth>();
            // Reduce player's health directly
            DecreaseHealth();
        }
    }

    // Method to decrease player's health
    private void DecreaseHealth()
    {
        if (playerHealthScript != null)
        {
            playerHealthScript.TakeDamage(spikeDamage);
            Debug.Log("Player health decreased by " + spikeDamage + ". Current health: " + playerHealthScript.health);
        }
        else
        {
            Debug.LogError("PlayerHealth script not assigned to SpikeScript!");
        }
    }
}
