using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class playerHealth : MonoBehaviour
{
    public int health = 100;

    public Slider healthSlider;

    public PlayerMovement playerMovement;

    public Rigidbody2D rb;
    
    private bool isDead = false;

    public float deathFlingForce = 1000f;
    public float deathRotateForce = 100f;
    

    public void Update()
    {
        healthSlider.value = health;
        if(health <= 0){
            Die();
        }
    }

    public void TakeDamage(int damage){
        health -= damage;
        
    }

    public void Die()
    {
        rb.freezeRotation = false;
        if(isDead == false)
        {
            rb.AddForce(Random.insideUnitCircle * Random.Range(deathFlingForce, deathFlingForce * 2), ForceMode2D.Impulse);
            rb.AddTorque(Random.Range((deathRotateForce * -1), deathRotateForce), ForceMode2D.Impulse);
        }
        isDead = true;
    }

    public void Revive(){
        isDead = false;
        transform.rotation = Quaternion.Euler(0, 0, 0);
        rb.freezeRotation = true;
    }
}
