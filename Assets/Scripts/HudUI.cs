using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HudUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI healthText;
    [SerializeField] private TextMeshProUGUI coinsText;
    [SerializeField] private TextMeshProUGUI pointsText;

    private Image[] backgroundButtons;
    private TextMeshProUGUI[] texts;

    private readonly string _healthTextTitle = "HEALTH";
    private readonly string _coinsTextTitle = "COINS   ";

    private void Start()
    {
        texts = GetComponentsInChildren<TextMeshProUGUI>();
        backgroundButtons = GetComponentsInChildren<Image>();

        EventManager.PlayerEvents.OnPlayerHealthChangeCallback += OnHealthValueChange;
        EventManager.PlayerEvents.OnPlayerCoinsValueChangeCallback += OnCoinsValueChange;
        EventManager.PlayerEvents.OnPlayerPointsValueChangeCallBack += OnPointsValueChange;
        EventManager.LevelEvents.OnLevelChangeLightColorsCallback += ChangeHudTextColors;
        EventManager.LevelEvents.OnlevelChangeDarkColorsCallback += ChangeHudBackgroundColor;
    }

    private void OnDestroy()
    {
        EventManager.PlayerEvents.OnPlayerHealthChangeCallback -= OnHealthValueChange;
        EventManager.PlayerEvents.OnPlayerCoinsValueChangeCallback -= OnCoinsValueChange;
        EventManager.PlayerEvents.OnPlayerPointsValueChangeCallBack -= OnPointsValueChange;
        EventManager.LevelEvents.OnLevelChangeLightColorsCallback -= ChangeHudTextColors;
        EventManager.LevelEvents.OnlevelChangeDarkColorsCallback -= ChangeHudBackgroundColor;
    }

    public void OnHealthValueChange(float playerHealth) => healthText.text = $"{_healthTextTitle} {playerHealth:0}";

    public void OnCoinsValueChange(float playerCoins) => coinsText.text = $"{_coinsTextTitle}  {playerCoins:0}";

    public void OnPointsValueChange(float playerPoints) => pointsText.text = $"{playerPoints:0}";

    public void ChangeHudTextColors(Color32 color)
    {
        foreach (var text in texts)
        {
            text.color = color;
        }
    }

    public void ChangeHudBackgroundColor(Color32 color)
    {
        foreach (var backgroundButton in backgroundButtons)
        {
            backgroundButton.color = color;
        }
    }
}