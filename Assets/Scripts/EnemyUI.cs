using UnityEngine;
using UnityEngine.UI;

public class EnemyUI : MonoBehaviour
{
    [SerializeField] private Image countdownBar;
    [SerializeField] private Image healthBar;

    public void HealthBarChange(float healthValue) => healthBar.fillAmount = healthValue / 100f;

    public void CountdownBarChange(float countdown) => countdownBar.fillAmount = countdown;
}