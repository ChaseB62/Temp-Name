using UnityEngine;

public class GunController : MonoBehaviour
{
    public bool hasGun = false;
    public GameObject gunPrefab;
    private GameObject currentGun;
    public float pickupRange = 1.5f; // Adjust this value as needed

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && !hasGun)
        {
            TryPickUpGun();
        }
        else if (Input.GetKeyDown(KeyCode.E) && hasGun)
        {
            DropGun();
        }

        if (hasGun)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Shoot();
            }
        }
    }

    void TryPickUpGun()
    {
        // Check if there's a gun within pickupRange
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, pickupRange);

        foreach (var collider in colliders)
        {
            if (collider.gameObject.CompareTag("Gun"))
            {
                PickUpGun();
                break;
            }
        }
    }

    void PickUpGun()
    {
        // Instantiate the gun prefab and set it as a child of the player
        currentGun = Instantiate(gunPrefab, transform.position, Quaternion.identity);
        currentGun.transform.parent = transform;

        // Set the boolean variable to true
        hasGun = true;
    }

    void DropGun()
    {
        // Destroy the current gun and set the boolean variable to false
        Destroy(currentGun);
        hasGun = false;
    }

    void Shoot()
    {
        // Implement shooting functionality here
        // For example: Instantiate bullets, apply force, etc.
        Debug.Log("Bang! Bang!");
    }
}
