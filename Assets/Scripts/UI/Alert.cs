using TMPro;
using UnityEngine;

public class Alert : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _alertText;

    private void Start()
    {
        EventManager.LevelEvents.OnlevelChangeDarkColorsCallback += ChangeAlertTextColor;
    }

    private void OnDestroy()
    {
        EventManager.LevelEvents.OnlevelChangeDarkColorsCallback -= ChangeAlertTextColor;
    }

    public void ChangeAlertTextColor(Color32 color) => _alertText.color = color;

    public void AlertDestroy()
    {
        Destroy(this.gameObject);
        transform.parent.gameObject.SetActive(false);
    }
}