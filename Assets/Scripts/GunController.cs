using UnityEngine;

public class GunController : MonoBehaviour
{
    public bool hasGun = false;
    public GameObject gunPrefab;
    private GameObject currentGun;
    private GameObject pickedUpGun; // To store the picked-up gun object
    public float pickupRange = 1.5f; // Adjust this value as needed
    public float rotationSpeed = 5f; // Adjust this value to control rotation speed
    public float gunDistance = 1.5f; // Adjust this value to control the distance between player and gun

    public playerHealth playerHealthScript;

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

        
        if (playerHealthScript.health <= 0 && hasGun)
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
                if (!hasGun)
                {
                    PickUpGun(collider.gameObject);
                    break;
                }
            }
        }
    }

    void PickUpGun(GameObject gunObject)
    {
        // Deactivate the gun object on the floor
        gunObject.SetActive(false);
        pickedUpGun = gunObject;

        // Remove Rigidbody component from the picked-up gun
        Rigidbody2D rb = pickedUpGun.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            Destroy(rb);
        }

        // Drop the current gun if there is one
        if (hasGun)
        {
            DropGun();
        }

        // Instantiate the new gun prefab and set it at an offset from the player
        currentGun = Instantiate(gunPrefab, transform.position + new Vector3(gunDistance, 0f, 0f), Quaternion.identity);
        currentGun.transform.parent = transform;

        // Set the boolean variable to true
        hasGun = true;
    }

    void DropGun()
    {
        if (hasGun && currentGun != null)
        {
            // Activate the gun object on the floor
            pickedUpGun.transform.position = currentGun.transform.position; // Set position to the current gun's position
            pickedUpGun.SetActive(true);

            // Add Rigidbody component to the dropped gun
            Rigidbody2D rb = pickedUpGun.AddComponent<Rigidbody2D>();
            rb.gravityScale = 1; // Set gravity scale to enable falling

            // Destroy the current gun and set the boolean variable to false
            Destroy(currentGun);
            currentGun = null; // Set currentGun to null after destroying it
            hasGun = false;
        }
        else
        {
            Debug.LogWarning("Trying to drop gun, but currentGun is null or hasGun is false.");
        }
    }

    void Shoot()
    {
        // Implement shooting functionality here
        // For example: Instantiate bullets, apply force, etc.
        Debug.Log("Bang! Bang!");
    }
}
