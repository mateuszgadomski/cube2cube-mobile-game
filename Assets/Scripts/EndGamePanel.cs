using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGamePanel : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private TextMeshProUGUI _highestScoreText;

    private readonly string _touchSoundName = "StandardTouch";

    private void OnEnable()
    {
        _highestScoreText.text = $"HIGHEST SCORE: {EventManager.LevelEvents.CallOnChangeHighestScore()}";
    }

    public void Retry()
    {
        SoundManager.instance.PlaySound(_touchSoundName);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}