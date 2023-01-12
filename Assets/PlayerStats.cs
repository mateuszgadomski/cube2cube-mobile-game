using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    public TextMeshProUGUI playerPointsText;
    public Image playerPointsBar;

    private float playerPoints;


    private void Update()
    {
        playerPointsText.text = playerPoints.ToString("#");
        playerPointsBar.fillAmount = playerPoints / 1000;
    }

    public void addPoints(float addPointsValue)
    {
        playerPoints += addPointsValue;
    }
}
