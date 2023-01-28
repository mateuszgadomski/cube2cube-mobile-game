using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI playerPointsText;
    [SerializeField] private Image playerPointsBar;
    [SerializeField] private Material defaultCubeMaterial;
    [SerializeField] private PlayerStats playerStats;

    [Header("Experience Settings")]
    [SerializeField] private int secondLevelScore = 100;

    [SerializeField] private int thirdLevelScore = 300;
    [SerializeField] private int fourthLevelScore = 500;

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
        if (playerStats.playerPoints < secondLevelScore)
        {
            _level = 1;
            ChangeLevelColor(ColorsManager.Instance.lightGreyColor, ColorsManager.Instance.darkGreyColor);
        }
        else if (playerStats.playerPoints >= secondLevelScore && playerStats.playerPoints < thirdLevelScore)
        {
            _level = 2;
            ChangeLevelColor(ColorsManager.Instance.lightGreenColor, ColorsManager.Instance.darkGreenColor);
        }
        else if (playerStats.playerPoints >= thirdLevelScore && playerStats.playerPoints < fourthLevelScore)
        {
            _level = 3;
            ChangeLevelColor(ColorsManager.Instance.lightBlueColor, ColorsManager.Instance.darkBlueColor);
        }
        else if (playerStats.playerPoints >= fourthLevelScore)
        {
            _level = 4;
            ChangeLevelColor(ColorsManager.Instance.lightTurquoiseColor, ColorsManager.Instance.darkTurquoiseColor);
        }
    }

    private void LevelHandler(int level)
    {
        switch (level)
        {
            case 1:
                playerPointsBar.fillAmount = playerStats.playerPoints / secondLevelScore;
                break;

            case 2:
                playerPointsBar.fillAmount = playerStats.playerPoints / thirdLevelScore;
                break;

            case 3:
                playerPointsBar.fillAmount = playerStats.playerPoints / fourthLevelScore;
                break;

            case 4:
                playerPointsBar.fillAmount = 1f;
                break;

            default:
                break;
        }
    }
}