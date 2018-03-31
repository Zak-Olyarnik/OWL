using UnityEngine;
using System;
using UnityEngine.UI;
using System.Collections.Generic;

public class UIController : MonoBehaviour
{

    public Image HealthBar;
    public int lastHealth;
    public int healthMax = 200;
    private PlayerController pc;

    private void Start() {
       pc = PlayerController.GetInstance();
    }
    private void Update() {
        lastHealth = pc.GetHealth();
        healthMax = pc.GetHealthMax();
        HealthBarUpdate(lastHealth, healthMax);
    }

    public void HealthBarUpdate(int currentHealth, int startHealth)
    {
        HealthBar.fillAmount = (float)currentHealth / startHealth;
    }


}
