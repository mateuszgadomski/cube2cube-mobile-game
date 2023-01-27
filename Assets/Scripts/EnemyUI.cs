using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EnemyUI : MonoBehaviour
{
    [SerializeField] private Image countdownBar;
    [SerializeField] private Image healthBar;
    [SerializeField] private TextMeshProUGUI damageAmountText;
    [SerializeField] private Material material;

    private void Start()
    {
        EventManager.LevelEvents.OnlevelChangeDarkColorsCallback += ChangeMaterialColor;
        EventManager.LevelEvents.OnLevelChangeLightColorsCallback += ChangeCountdownBarColor;
    }

    private void OnDestroy()
    {
        EventManager.LevelEvents.OnlevelChangeDarkColorsCallback -= ChangeMaterialColor;
        EventManager.LevelEvents.OnLevelChangeLightColorsCallback -= ChangeCountdownBarColor;
    }

    public void TakeDamageText(float damage)
    {
        TextMeshProUGUI damageText = Instantiate(damageAmountText, transform.position, Quaternion.identity, transform);
        damageText.color = healthBar.color;
        damageText.text = $"-{damage}";
    }

    public void HealthBarChange(float healthValue) => healthBar.fillAmount = healthValue / 100f;

    public void CountdownBarChange(float countdown) => countdownBar.fillAmount = countdown;

    public void ChangeMaterialColor(Color32 color) => material.color = color;

    public void ChangeCountdownBarColor(Color32 color) => countdownBar.color = color;
}