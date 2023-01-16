using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Unity.VisualScripting;

public class LevelManager : MonoBehaviour
{
    public TextMeshProUGUI playerPointsText;
    public Image playerPointsBar;

    public Camera mainCamera;
    public Material defaultCubeMaterial;

    private PlayerStats playerStats;

    private int level;
    public int firstLevelScore = 0;
    public int secondLevelScore = 100;
    public int thirdLevelScore = 1000;
    public int fourthLevelScore = 10000;


    Color32 lightGreyColor = new(130, 130, 130, 255);
    Color32 darkGreyColor = new(96, 96, 96, 255);

    Color32 lightBrownColor = new(217, 181, 150, 255);
    Color32 darkBrownColor = new(115, 62, 32, 255);

    private void Start()
    {
        playerStats = GetComponent<PlayerStats>();

        level = 1;
        ChangeLevelColor(lightGreyColor, darkGreyColor);
    }

    private void Update()
    {
        playerPointsText.text = playerStats.playerPoints.ToString("#");


        LevelConditions();
        LevelHandler(level);
    }

    private void LevelConditions()
    {
        if (playerStats.playerPoints >= secondLevelScore && playerStats.playerPoints < thirdLevelScore)
        {
            level = 2;
            ChangeLevelColor(lightBrownColor, darkBrownColor);
        }
        else if (playerStats.playerPoints >= thirdLevelScore && playerStats.playerPoints < fourthLevelScore)
        {
            level = 3;
        }
        else if (playerStats.playerPoints >= fourthLevelScore)
        {
            level = 4;
        }
    }

    public void ChangeLevelColor(Color32 lightColor, Color32 darkColor)
    {
        mainCamera.backgroundColor = lightColor;
        playerPointsText.color = lightColor;

        playerPointsBar.color = darkColor;
        playerPointsText.outlineColor = darkColor;
        defaultCubeMaterial.color = darkColor;
    }

    private void LevelHandler(int level)
    {
        switch (level)
        {
            case 1:
                playerPointsBar.fillAmount = playerStats.playerPoints / 100;
                break;
            case 2:
                playerPointsBar.fillAmount = playerStats.playerPoints / 1000;
                break;
            case 3:
                playerPointsBar.fillAmount = playerStats.playerPoints / 10000;
                break;
            case 4:
                playerPointsBar.fillAmount = 1f;
                break;
            default:
                break;
        }
    }
}
