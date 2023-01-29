using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HudUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _healthText;
    [SerializeField] private TextMeshProUGUI _coinsText;
    [SerializeField] private TextMeshProUGUI _pointsText;
    [SerializeField] private TextMeshProUGUI _alertPrefab;
    [SerializeField] private GameObject _endGamePanel;
    [SerializeField] private GameObject _notification;

    private Image[] _backgroundButtons;
    private TextMeshProUGUI[] _texts;

    private readonly string _healthTextTitle = "HEALTH";
    private readonly string _coinsTextTitle = "COINS   ";

    private void Start()
    {
        _texts = GetComponentsInChildren<TextMeshProUGUI>();
        _backgroundButtons = GetComponentsInChildren<Image>();

        EventManager.PlayerEvents.OnPlayerHealthChangeCallback += OnHealthValueChange;
        EventManager.PlayerEvents.OnPlayerCoinsValueChangeCallback += OnCoinsValueChange;
        EventManager.PlayerEvents.OnPlayerPointsValueChangeCallBack += OnPointsValueChange;
        EventManager.LevelEvents.OnLevelChangeLightColorsCallback += ChangeHudTextColors;
        EventManager.LevelEvents.OnlevelChangeDarkColorsCallback += ChangeHudBackgroundColor;
        EventManager.LevelEvents.OnNotificationInSceneCallback += OnNotificationTextChange;
        EventManager.LevelEvents.OnEndGameStateCallback += GetActiveEndGamePanel;
    }

    private void OnDestroy()
    {
        EventManager.PlayerEvents.OnPlayerHealthChangeCallback -= OnHealthValueChange;
        EventManager.PlayerEvents.OnPlayerCoinsValueChangeCallback -= OnCoinsValueChange;
        EventManager.PlayerEvents.OnPlayerPointsValueChangeCallBack -= OnPointsValueChange;
        EventManager.LevelEvents.OnLevelChangeLightColorsCallback -= ChangeHudTextColors;
        EventManager.LevelEvents.OnlevelChangeDarkColorsCallback -= ChangeHudBackgroundColor;
        EventManager.LevelEvents.OnNotificationInSceneCallback -= OnNotificationTextChange;
        EventManager.LevelEvents.OnEndGameStateCallback -= GetActiveEndGamePanel;
    }

    public void OnHealthValueChange(float playerHealth) => _healthText.text = $"{_healthTextTitle} {playerHealth:0}";

    public void OnCoinsValueChange(float playerCoins) => _coinsText.text = $"{_coinsTextTitle}  {playerCoins:0}";

    public void OnPointsValueChange(float playerPoints) => _pointsText.text = $"{playerPoints:0}";

    public void OnNotificationTextChange(string alertText)
    {
        if (!_notification.activeSelf)
        {
            _notification.SetActive(true);
            var alert = Instantiate(_alertPrefab, _notification.transform.position, Quaternion.identity, _notification.transform);
            alert.text = alertText.ToUpper();
        }
    }

    public void ChangeHudTextColors(Color32 color)
    {
        foreach (var text in _texts)
        {
            text.color = color;
        }
    }

    public void ChangeHudBackgroundColor(Color32 color)
    {
        foreach (var backgroundButton in _backgroundButtons)
        {
            backgroundButton.color = color;
        }
    }

    public void GetActiveEndGamePanel()
    {
        if (!_endGamePanel.activeSelf)
        {
            StartCoroutine(Countdown());
        }
        _endGamePanel.SetActive(true);
    }

    private IEnumerator Countdown()
    {
        yield return new WaitForSeconds(2f);
        GameManager.Instance.SetGameTime(0f);
    }
}