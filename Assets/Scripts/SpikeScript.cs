using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeScript : MonoBehaviour
{

    private int playerHealth = 100;
    public int spikeDamage = 10;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Called when something enters the collider attached to this GameObject
    void OnTriggerEnter(Collider other)
    {
        // Check if the colliding object is the player
        if (other.CompareTag("Player"))
        {
            // Reduce player's health directly
            DecreaseHealth(spikeDamage);
        }
    }

    // Method to decrease player's health
    private void DecreaseHealth(int damage)
    {
        playerHealth -= damage;

        if (playerHealth <= 0)
        {
            // Player is dead
            Debug.Log("Player is dead!");
        }
    }
}
