using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupManager : MonoBehaviour
{
   public Transform pickupLocation;
   public Collider2D pickupCollider;

   public KeyCode pickupKey;

    public void OnTriggerStay2D(Collider2D other){
        if(other.CompareTag("Gun") && Input.GetKeyDown(pickupKey)){
            // other.Get
        }
    }
    
    //HOW THE FUCK DO YOU DO THIS???????????????????????????? I CANT DO SHIT
}
