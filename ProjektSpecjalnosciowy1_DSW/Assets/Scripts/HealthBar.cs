using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Health playerHealth;
    [SerializeField] private Slider currentHealthBar;

    private void Start()
    {
        currentHealthBar.value = playerHealth.CurrentHealth;
    }

    private void Update()
    {
        Debug.Log(playerHealth.CurrentHealth);
        currentHealthBar.value = playerHealth.CurrentHealth;
    }
}
