using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public float weight = 10f;
    public GameObject bullet;

    public Transform playerTransform;

    public float timeInBetweenShots = 1f;

    public void Shoot()
    {
        Instantiate(bullet, playerTransform.transform.position, transform.rotation);
    }

    public void Update(){
        Vector3 mousePosition = Input.mousePosition;

        Vector3 targetPosition = Camera.main.ScreenToWorldPoint(new Vector3(mousePosition.x, mousePosition.y, transform.position.z - Camera.main.transform.position.z));

        transform.right = -(targetPosition - transform.position);
    }
}
