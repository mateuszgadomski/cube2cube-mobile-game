using TMPro;
using UnityEngine;

public class StartGamePanel : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private TextMeshProUGUI _highestScoreText;

    private void Start()
    {
        _highestScoreText.text = $"HIGHEST SCORE: {EventManager.LevelEvents.CallOnChangeHighestScore()}";
    }

    public void ClosePanelAnimation()
    {
        _animator.SetTrigger("ClosePanel");
        SoundManager.instance.PlaySound("StandardTouch");
    }

    public void StartGamePanelActive()
    {
        gameObject.SetActive(!gameObject.activeSelf);
        GameManager.Instance.SetGameTime(1.0f);
    }
}