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

    public delegate void TestDelegate();

    public void Timer(ref float countdown, TestDelegate method)
    {
        if (countdown <= 0f)
        {
            method();
        }
        countdown -= Time.deltaTime;
        countdown = Mathf.Clamp(countdown, 0f, countdown);
    }
}

