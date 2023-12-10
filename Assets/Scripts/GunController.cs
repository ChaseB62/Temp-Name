using UnityEngine;

public class GunController : MonoBehaviour
{
    public Transform playerHand;
    public GameObject gunHolder;
    public LayerMask gunLayer;
    private GameObject currentGun;
    private GameObject originalGunOnGround;
    private Rigidbody2D originalRigidbody; // Store the original Rigidbody2D
    public float pickupRadius = 2f;
    private bool isHoldingGun = false;
    private string lastPickedGunType = ""; // Store the type of the last picked up gun

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (!isHoldingGun)
            {
                PickUpGun();
            }
            else
            {
                DropGun();
            }
        }
    }

    void PickUpGun()
    {
        if (isHoldingGun)
        {
            Debug.Log("Cannot pick up another gun while already holding one.");
            return;
        }

        Collider2D[] colliders = Physics2D.OverlapCircleAll(playerHand.position, pickupRadius, gunLayer);

        foreach (Collider2D collider in colliders)
        {
            if (collider.CompareTag("Pistol") || collider.CompareTag("SMG") ||
                collider.CompareTag("AssaultRifle") || collider.CompareTag("Shotgun"))
            {
                Debug.Log("Picking up " + collider.name);

                // Destroy the original gun on the ground if there is one
                if (originalGunOnGround != null && lastPickedGunType == collider.tag)
                {
                    Destroy(originalGunOnGround);
                }

                // Create an empty GameObject to represent the player's hand
                gunHolder = new GameObject("GunHolder");
                gunHolder.transform.position = playerHand.position;
                gunHolder.transform.parent = playerHand;

                // Instantiate the gun relative to the gunHolder
                currentGun = Instantiate(collider.gameObject, gunHolder.transform);
                currentGun.transform.localPosition = Vector3.zero;
                currentGun.transform.localRotation = Quaternion.identity;

                // Store the original Rigidbody2D component
                originalRigidbody = collider.GetComponent<Rigidbody2D>();

                // Disable the original gun while the gun is in the player's hand
                collider.gameObject.SetActive(false);

                // Disable the original Rigidbody2D while the gun is in the player's hand
                if (originalRigidbody != null)
                {
                    originalRigidbody.simulated = false;
                }

                // Check if the instantiated gun has a Rigidbody2D and disable it
                Rigidbody2D gunRigidbody = currentGun.GetComponent<Rigidbody2D>();
                if (gunRigidbody != null)
                {
                    gunRigidbody.simulated = false;
                }

                currentGun.GetComponent<Collider2D>().enabled = false;

                // Log the scale information
                Debug.Log("Gun Scale: " + currentGun.transform.localScale);
                Debug.Log("GunHolder Scale: " + gunHolder.transform.localScale);
                Debug.Log("PlayerHand Scale: " + playerHand.localScale);

                // Update the originalGunOnGround reference and lastPickedGunType
                originalGunOnGround = currentGun;
                lastPickedGunType = collider.tag;

                isHoldingGun = true;
                break;
            }
        }
    }

    void DropGun()
    {
        if (currentGun != null)
        {
            Debug.Log("Dropping " + currentGun.name);

            currentGun.GetComponent<Collider2D>().enabled = true;
            currentGun.transform.parent = null;

            currentGun.transform.position = playerHand.position + playerHand.right * 2f;
            currentGun.transform.rotation = Quaternion.identity;

            // Re-enable the original gun on the ground if it exists
            if (originalGunOnGround != null)
            {
                originalGunOnGround.SetActive(true);
            }

            // Re-enable the original Rigidbody2D component if it exists
            if (originalRigidbody != null)
            {
                originalRigidbody.simulated = true;
            }

            // Check if the instantiated gun has a Rigidbody2D and re-enable it
            Rigidbody2D gunRigidbody = currentGun.GetComponent<Rigidbody2D>();
            if (gunRigidbody != null)
            {
                gunRigidbody.simulated = true;
            }

            // Destroy the current gun if it's different from the original gun on the ground
            if (currentGun != originalGunOnGround)
            {
                Destroy(currentGun);
            }

            currentGun = null;

            // Destroy the empty GameObject representing the player's hand
            Destroy(gunHolder);

            isHoldingGun = false;
        }
    }
}
