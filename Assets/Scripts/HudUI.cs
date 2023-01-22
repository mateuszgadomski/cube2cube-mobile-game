using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HudUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI healthText;
    [SerializeField] private TextMeshProUGUI coinsText;
    [SerializeField] private TextMeshProUGUI pointsText;
    [SerializeField] private GameObject notification;

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
        EventManager.LevelEvents.OnNotificationInSceneCallback += OnNotificationTextChange;

        OnNotificationTextChange("TEST TEST TEST");
    }

    private void OnDestroy()
    {
        EventManager.PlayerEvents.OnPlayerHealthChangeCallback -= OnHealthValueChange;
        EventManager.PlayerEvents.OnPlayerCoinsValueChangeCallback -= OnCoinsValueChange;
        EventManager.PlayerEvents.OnPlayerPointsValueChangeCallBack -= OnPointsValueChange;
        EventManager.LevelEvents.OnLevelChangeLightColorsCallback -= ChangeHudTextColors;
        EventManager.LevelEvents.OnlevelChangeDarkColorsCallback -= ChangeHudBackgroundColor;
        EventManager.LevelEvents.OnNotificationInSceneCallback -= OnNotificationTextChange;
    }

    public void OnHealthValueChange(float playerHealth) => healthText.text = $"{_healthTextTitle} {playerHealth:0}";

    public void OnCoinsValueChange(float playerCoins) => coinsText.text = $"{_coinsTextTitle}  {playerCoins:0}";

    public void OnPointsValueChange(float playerPoints) => pointsText.text = $"{playerPoints:0}";

    public void OnNotificationTextChange(string alertText)
    {
        Vector3 _posFromCenter = new(0, -15f, 0);
        var _notification = Instantiate(notification, transform.position + _posFromCenter, Quaternion.identity, transform.parent);
        var _alertText = _notification.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        _alertText.text = alertText.ToUpper();
    }

    public void DestroyNotification() => Destroy(notification);

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