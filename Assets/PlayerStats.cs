using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Unity.VisualScripting;

public class PlayerStats : MonoBehaviour
{
    public TextMeshProUGUI playerPointsText;
    public Image playerPointsBar;

    public Camera mainCamera;
    public Material defaultCubeMaterial;

    public float playerPoints; // test

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
        level = 1;
        ChangeLevelColor(lightGreyColor, darkGreyColor);
    }

    private void Update()
    {
        playerPointsText.text = playerPoints.ToString("#");


        LevelConditions();
        LevelHandler(level);
    }

    public void addPoints(float addPointsValue)
    {
        playerPoints += addPointsValue;
    }

    private void LevelConditions()
    {
        if (playerPoints >= secondLevelScore && playerPoints < thirdLevelScore)
        {
            level = 2;
            ChangeLevelColor(lightBrownColor, darkBrownColor);
        }
        else if (playerPoints >= thirdLevelScore && playerPoints < fourthLevelScore)
        {
            level = 3;
        }
        else if (playerPoints >= fourthLevelScore)
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
                playerPointsBar.fillAmount = playerPoints / 100;
                break;
            case 2:
                playerPointsBar.fillAmount = playerPoints / 1000;
                break;
            case 3:
                playerPointsBar.fillAmount = playerPoints / 10000;
                break;
            case 4:
                playerPointsBar.fillAmount = 1f;
                break;
            default:
                break;
        }
    }
}
