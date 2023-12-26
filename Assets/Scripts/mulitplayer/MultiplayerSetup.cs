using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Pun;

public class MultiplayerSetup : MonoBehaviour
{
    public PlayerMovement playerMovement;
    public GameObject camera;
    //public GunController gunController;


    public string nickname;

    public GameObject[] objectsToEnable;
    public GameObject[] objectsToDisable;


    public TextMeshProUGUI nicknameText;

    public void IsLocalPlayer()
    {
        playerMovement.enabled = true;
        //gunController.enabled = true;
        camera.SetActive(true);
        SetObjectsActive(objectsToEnable, true);
        SetObjectsActive(objectsToDisable, false);
    }

    void SetObjectsActive(GameObject[] objects, bool isActive)
    {
        foreach (GameObject obj in objects)
        {
            if (obj != null)
            {
                obj.SetActive(isActive);
            }
        }
    }

    [PunRPC]
    public void SetNickname(string _name)
    {
        nickname = _name;

        nicknameText.text = nickname;
    }
}
