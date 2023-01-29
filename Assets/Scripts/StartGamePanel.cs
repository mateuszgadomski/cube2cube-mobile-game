using TMPro;
using UnityEngine;

public class StartGamePanel : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private TextMeshProUGUI highestScoreText;

    private void Start()
    {
        highestScoreText.text = $"HIGHEST SCORE: {EventManager.LevelEvents.CallOnChangeHighestScore()}";
    }

    public void ClosePanelAnimation()
    {
        animator.SetTrigger("ClosePanel");
        SoundManager.instance.PlaySound("StandardTouch");
    }

    public void StartGamePanelActive()
    {
        gameObject.SetActive(!gameObject.activeSelf);
        GameManager.instance.SetGameTime(1.0f);
    }
}