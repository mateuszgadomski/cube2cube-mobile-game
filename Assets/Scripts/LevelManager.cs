using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _playerPointsText;
    [SerializeField] private Image _playerPointsBar;
    [SerializeField] private Material _defaultCubeMaterial;
    [SerializeField] private PlayerStats _playerStats;

    [Header("Experience Settings")]
    [SerializeField] private int _secondLevelScore = 100;

    [SerializeField] private int _thirdLevelScore = 300;
    [SerializeField] private int _fourthLevelScore = 500;

    private int _level;

    private void Update()
    {
        LevelConditions();
        LevelHandler(_level);
    }

    public void ChangeLevelColor(Color32 lightColor, Color32 darkColor)
    {
        EventManager.LevelEvents.CallOnLevelChangeLightColors(lightColor);
        EventManager.LevelEvents.CallOnLevelChangeDarkColors(darkColor);
    }

    private void LevelConditions()
    {
        if (_playerStats.PlayerPoints < _secondLevelScore)
        {
            _level = 1;
            ChangeLevelColor(ColorsManager.Instance.LightGreyColor, ColorsManager.Instance.DarkGreyColor);
        }
        else if (_playerStats.PlayerPoints >= _secondLevelScore && _playerStats.PlayerPoints < _thirdLevelScore)
        {
            _level = 2;
            ChangeLevelColor(ColorsManager.Instance.LightGreenColor, ColorsManager.Instance.DarkGreenColor);
        }
        else if (_playerStats.PlayerPoints >= _thirdLevelScore && _playerStats.PlayerPoints < _fourthLevelScore)
        {
            _level = 3;
            ChangeLevelColor(ColorsManager.Instance.LightBlueColor, ColorsManager.Instance.DarkBlueColor);
        }
        else if (_playerStats.PlayerPoints >= _fourthLevelScore)
        {
            _level = 4;
            ChangeLevelColor(ColorsManager.Instance.LightTurquoiseColor, ColorsManager.Instance.DarkTurquoiseColor);
        }
    }

    private void LevelHandler(int level)
    {
        switch (level)
        {
            case 1:
                _playerPointsBar.fillAmount = _playerStats.PlayerPoints / _secondLevelScore;
                break;

            case 2:
                _playerPointsBar.fillAmount = _playerStats.PlayerPoints / _thirdLevelScore;
                break;

            case 3:
                _playerPointsBar.fillAmount = _playerStats.PlayerPoints / _fourthLevelScore;
                break;

            case 4:
                _playerPointsBar.fillAmount = 1f;
                break;

            default:
                break;
        }
    }
}