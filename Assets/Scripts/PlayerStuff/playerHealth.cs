using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Photon.Pun;

public class playerHealth : MonoBehaviour
{
    public Health healthScript;

    public float healthPlayer = 100f;

    public Slider healthSlider;

    public PlayerMovement playerMovement;

    public Rigidbody2D rb;
    
    private bool isDead = false;

    public float deathFlingForce = 1000f;
    public float deathRotateForce = 100f;

    public particleHandler particleHandler;
    public GoToPlayer goToPlayer;
    

    public void Update()
    {
        PhotonView photonView = PhotonView.Get(this);

        healthPlayer = healthScript.startHealth;
        healthSlider.value = healthPlayer;
        if(healthPlayer <= 0){
            photonView.RPC("Die", RpcTarget.AllBuffered);
        } else if(healthPlayer > 0){
            photonView.RPC("Revive", RpcTarget.AllBuffered);
        }
    }

    [PunRPC]
    public void Die()
    {
        playerMovement.enabled = false;
        rb.freezeRotation = false;
        if(isDead == false)
        {
            goToPlayer.Zoom = 7.5f;
            goToPlayer.lerpSpeed = 10f;
            rb.AddForce(Random.insideUnitCircle * Random.Range(deathFlingForce, deathFlingForce + (deathFlingForce / 4)), ForceMode2D.Impulse);
            rb.AddTorque(Random.Range((deathRotateForce * -1), deathRotateForce), ForceMode2D.Impulse);
            particleHandler.Die();
        }
        isDead = true;
    }
    [PunRPC]
    public void Revive(){
        playerMovement.enabled = true;
        isDead = false;
        goToPlayer.Zoom = 10f;
        goToPlayer.lerpSpeed = 10f;
        transform.rotation = Quaternion.Euler(0, 0, 0);
        rb.freezeRotation = true;
    }
}
