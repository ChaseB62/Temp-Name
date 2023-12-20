using UnityEngine;
using Photon.Pun;

public class GunController : MonoBehaviour
{
    public Transform playerHand;
    public LayerMask gunLayer;
    private GameObject currentGun;
    private GameObject originalGunOnGround;
    private Rigidbody2D originalRigidbody; // Store the original Rigidbody2D
    public float pickupRadius = 2f;

    public float chuckSpeed = 10f;
    private bool isHoldingGun = false;
    private string lastPickedGunType = ""; // Store the type of the last picked up gun

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            PhotonView photonView = PhotonView.Get(this);

            if (!isHoldingGun)
            {
                photonView.RPC("PickUpGun", RpcTarget.AllBuffered);
            }
            else
            {
                photonView.RPC("DropGun", RpcTarget.AllBuffered);
            }
        }
    }

    [PunRPC]
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
                //aiden wrote the majority of this but since hes stupid and dumb he had to make a new tag for each individual gun so i got rid of that and shunned him.
                if (collider.CompareTag("Grab"))
                {
                    Debug.Log("Picking up " + collider.name);

                    originalGunOnGround = collider.gameObject;
                    currentGun = originalGunOnGround;

                    originalGunOnGround.transform.parent = playerHand;

                    originalGunOnGround.transform.localPosition = Vector3.zero;
                    originalGunOnGround.transform.localEulerAngles = new Vector3(0,0,0);
                    currentGun.transform.localScale = new Vector3(1f, 1f, 1f);

                    Gun gun = currentGun.GetComponent<Gun>();
                    gun.enabled = true;

                    // Store the original Rigidbody2D component
                    originalRigidbody = collider.GetComponent<Rigidbody2D>();

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

                    // Update the originalGunOnGround reference and lastPickedGunType
                    originalGunOnGround = currentGun;
                    lastPickedGunType = collider.tag;

                    isHoldingGun = true;
                    break;
                }
            }
        }


    [PunRPC]
    void DropGun()
    {
        if (currentGun != null)
        {
            
            Debug.Log("Dropping " + currentGun.name);

            currentGun.GetComponent<Collider2D>().enabled = true;
            currentGun.transform.parent = null;
            currentGun.transform.localScale = new Vector3(1f, 1f, 1f);

            Gun gun = currentGun.GetComponent<Gun>();
            gun.enabled = false;


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
                gunRigidbody.AddForce(currentGun.transform.forward * chuckSpeed, ForceMode2D.Impulse);
            }

            

            // Destroy the current gun if it's different from the original gun on the ground
            if (currentGun != originalGunOnGround)
            {
                Destroy(currentGun);
            }

            currentGun = null;

            isHoldingGun = false;
        }
    }
}
//homer