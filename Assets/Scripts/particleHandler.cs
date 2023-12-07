using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class particleHandler : MonoBehaviour
{
    public Transform player;

    public GameObject jumpParticle;

    public void Jump(){
        Instantiate(jumpParticle, player.transform.position, player.transform.rotation);
    }
}
