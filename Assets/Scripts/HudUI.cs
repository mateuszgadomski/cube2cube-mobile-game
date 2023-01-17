using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HudUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI healthText;
    [SerializeField] private TextMeshProUGUI coinsText;
    [SerializeField] private TextMeshProUGUI shopText;
    [SerializeField] private TextMeshProUGUI pointsText;
    [SerializeField] private GameObject statsPanel;
    [SerializeField] private PlayerStats playerStats;

    public Image[] backgroundButtons;

    private readonly string healthTextTitle = "HEALTH";
    private readonly string coinsTextTitle = "COINS";

    private void Awake()
    {
        statsPanel.GetComponentsInChildren<Image>();
    }

    private void Start()
    {
        EventManager.PlayerEvents.OnPlayerHealthChangeCallback += OnHealthValueChange;
        EventManager.PlayerEvents.OnPlayerCoinsValueChangeCallback += OnCoinsValueChange;
        EventManager.LevelEvents.OnLevelChangeLightColorsCallback += ChangeHudTextColors;
        EventManager.LevelEvents.OnlevelChangeDarkColorsCallback += ChangeColorBackgroundButtons;
    }
    private void OnDestroy()
    {
        EventManager.PlayerEvents.OnPlayerHealthChangeCallback -= OnHealthValueChange;
        EventManager.PlayerEvents.OnPlayerCoinsValueChangeCallback -= OnCoinsValueChange;
        EventManager.LevelEvents.OnLevelChangeLightColorsCallback -= ChangeHudTextColors;
        EventManager.LevelEvents.OnlevelChangeDarkColorsCallback -= ChangeColorBackgroundButtons;
    }

    public void OnHealthValueChange(float playerHealth) => healthText.text = $"{healthTextTitle} {playerHealth}";

    public void OnCoinsValueChange(float playerCoins) => coinsText.text = $"{coinsTextTitle} {playerCoins}";

    private void ChangeTextColor(TextMeshProUGUI textName, Color32 color) =>  textName.color = color;

    public void ChangeHudTextColors(Color32 color)
    {
        ChangeTextColor(healthText, color);
        ChangeTextColor(coinsText, color);
        ChangeTextColor(shopText, color);
        ChangeTextColor(pointsText, color);
    }

    public void ChangeColorBackgroundButtons(Color32 color)
    {
        Debug.Log(color);
        foreach (Image backgroundButton in backgroundButtons)
        {
            Debug.Log(backgroundButton.gameObject);
            backgroundButton.color = color;
        }
    }


}