using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    public int RandomNumberGenerate(int minValue, int maxValue)
    {
        int randomNumber = Random.Range(minValue, maxValue);
        return randomNumber;
    }
}

