using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class playerHealth : MonoBehaviour
{
    public float health = 100f;

    public Slider healthSlider;

    public void Update()
    {
        healthSlider.value = health;
    }
}
