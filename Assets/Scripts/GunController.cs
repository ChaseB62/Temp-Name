using UnityEngine;

namespace YourNamespace
{
    [System.Serializable]
    public class Gun
    {
        public string gunName;
        public GameObject gunPrefab;
        public float damage = 10f;
        // Add more properties as needed

        public Gun(string name, GameObject prefab, float dmg)
        {
            gunName = name;
            gunPrefab = prefab;
            damage = dmg;
        }
    }

    public class GunController : MonoBehaviour
    {
    public bool hasGun = false;
    public float pickupRange = 1.5f;
    public float rotationSpeed = 5f;
    public float gunDistance = 1.5f;
    public playerHealth playerHealthScript;

    public Gun[] availableGuns; // Array to store different types of guns

    private GameObject currentGun;
    private GameObject pickedUpGun;

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

        // Randomly select a gun from availableGuns array
        Gun selectedGun = availableGuns[Random.Range(0, availableGuns.Length)];

        // Instantiate the new gun prefab and set it at an offset from the player
        currentGun = Instantiate(selectedGun.gunPrefab, transform.position + new Vector3(gunDistance, 0f, 0f), Quaternion.identity);
        currentGun.transform.parent = transform;

        // Set the boolean variable to true
        hasGun = true;

        Debug.Log("Picked up: " + selectedGun.gunName);
    }

    void DropGun()
    {
        if (hasGun && currentGun != null)
        {
            // Activate the gun object on the floor
            pickedUpGun.transform.position = currentGun.transform.position;
            pickedUpGun.SetActive(true);

            // Add Rigidbody component to the dropped gun
            Rigidbody2D rb = pickedUpGun.AddComponent<Rigidbody2D>();
            rb.gravityScale = 1;

            // Destroy the current gun and set the boolean variable to false
            Destroy(currentGun);
            currentGun = null;
            hasGun = false;
        }
        else
        {
            Debug.LogWarning("Trying to drop gun, but currentGun is null or hasGun is false.");
        }
    }

    void Shoot()
    {
        // Implement shooting functionality based on the currentGun's properties
        // For example: Instantiate bullets, apply force, etc.
        Debug.Log("Bang! Bang! Damage: " + currentGun.GetComponent<Gun>().damage);
    }
    }
}
