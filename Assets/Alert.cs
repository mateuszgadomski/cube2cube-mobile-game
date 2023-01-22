using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Alert : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI alertText;

    private void Start()
    {
        EventManager.LevelEvents.OnlevelChangeDarkColorsCallback += ChangeAlertTextColor;
    }

    private void OnDestroy()
    {
        EventManager.LevelEvents.OnlevelChangeDarkColorsCallback -= ChangeAlertTextColor;
    }

    public void AlertDestroy()
    {
        Destroy(gameObject);
        transform.parent.gameObject.SetActive(false);
    }

    public void ChangeAlertTextColor(Color32 color) => alertText.color = color;
}