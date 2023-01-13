using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Unity.VisualScripting;

public class PlayerStats : MonoBehaviour
{
    public float playerHealth = 100f;
    public float playerPoints; //
    public void addPoints(float addPointsValue)
    {
        playerPoints += addPointsValue;
    }


}
