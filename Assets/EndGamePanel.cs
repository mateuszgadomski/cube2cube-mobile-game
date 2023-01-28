using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGamePanel : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private TextMeshProUGUI highestScoreText;

    private void OnEnable()
    {
        highestScoreText.text = $"HIGHEST SCORE: {EventManager.LevelEvents.CallOnChangeHighestScore()}";
    }

    public void Retry()
    {
        SoundManager.instance.PlaySound("StandardTouch");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}