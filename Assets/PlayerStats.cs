using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerStats : MonoBehaviour
{
    public TextMeshProUGUI playerPointsText;

    private float playerPoints;


    private void Update()
    {
        playerPointsText.text = $"Points: {playerPoints.ToString("#")}";
    }

    public void addPoints(float addPointsValue)
    {
        playerPoints += addPointsValue;
    }
}
