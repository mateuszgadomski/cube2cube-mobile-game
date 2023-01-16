using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class EnemyUI : MonoBehaviour
{
    private Image countdownBar;
    private Image healthBar;

    private void Awake()
    {
        countdownBar = GetComponentsInChildren<Image>().ToList().FirstOrDefault(n => n.gameObject.name == "CountdownBar");
        healthBar = GetComponentsInChildren<Image>().ToList().FirstOrDefault(n => n.gameObject.name == "HealthBar");
    }

    public void HealthBarChange(float healthValue) => healthBar.fillAmount = healthValue / 100f;

    public void CountdownBarChange(float countdown) => countdownBar.fillAmount = countdown;
}
   
