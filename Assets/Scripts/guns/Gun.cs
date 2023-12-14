using System.Collections;
using UnityEngine;
using Photon.Pun;

public class Gun : MonoBehaviour
{
    public KeyCode shootKey;
    public GameObject bulletObject;
    public Transform bulletSpawn;
    public float shootCooldown;

    private bool canShoot = true;

    private void Update()
    {
        if (Input.GetKey(shootKey) && canShoot)
        {
            Debug.Log("got key");
            Shoot();
        }
    }

    private void Shoot()
    {
        Debug.Log("shot");

        // Use PhotonNetwork.Instantiate if working in a multiplayer environment
        Instantiate(bulletObject, bulletSpawn.position, bulletSpawn.rotation);

        StartCoroutine(StartCooldown());
    }

    private IEnumerator StartCooldown()
    {
        Debug.Log("Start cooldown");
        canShoot = false;

        // Adding a log here to check if the coroutine is reached
        Debug.Log("Coroutine started");

        yield return new WaitForSeconds(shootCooldown);
        
        // Adding a log here to check if WaitForSeconds is working
        Debug.Log("Coroutine finished waiting");

        Debug.Log("end cooldown");
        canShoot = true;
    }
}
