using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EnemyUI : MonoBehaviour
{
    [SerializeField] private Image _countdownBar;
    [SerializeField] private Image _healthBar;
    [SerializeField] private TextMeshProUGUI _damageAmountText;
    [SerializeField] private Material _material;

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

    public void HealthBarChange(float healthValue) => _healthBar.fillAmount = healthValue / 100f;

    public void CountdownBarChange(float countdown) => _countdownBar.fillAmount = countdown;

    public void ChangeMaterialColor(Color32 color) => _material.color = color;

    public void ChangeCountdownBarColor(Color32 color) => _countdownBar.color = color;

    public void TakeDamageText(float damage)
    {
        TextMeshProUGUI damageText = Instantiate(_damageAmountText, transform.position, Quaternion.identity, transform);
        damageText.color = _healthBar.color;
        damageText.text = $"-{damage}";
    }
}