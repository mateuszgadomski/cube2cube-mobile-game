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
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        SetGameTime(0f);
    }

    public void SetGameTime(float time) => Time.timeScale = time;

    public int RandomNumberGenerate(int minValue, int maxValue)
    {
        int _randomNumber = Random.Range(minValue, maxValue);
        return _randomNumber;
    }

    public bool DelayToAction(ref float countdown)
    {
        if (countdown <= 0f)
        {
            return true;
        }

        countdown -= Time.deltaTime;
        countdown = Mathf.Clamp(countdown, 0f, countdown);
        return false;
    }

    public void VibratePhone()
    {
        Handheld.Vibrate();
    }
}