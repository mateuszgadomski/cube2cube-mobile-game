using UnityEngine;
using UnityEngine.UI;

public class EnemyChangeColorSettings : MonoBehaviour
{
    [SerializeField] private Image countdownBar;
    [SerializeField] private Image healthBar;
    [SerializeField] private Material material;

    private void Start()
    {
        EventManager.LevelEvents.OnlevelChangeDarkColorsCallback += ChangeMaterialColor;
        EventManager.LevelEvents.OnLevelChangeLightColorsCallback += ChangeCountdownBarColor;
    }

    private void OnDestroy()
    {
        EventManager.LevelEvents.OnlevelChangeDarkColorsCallback -= ChangeMaterialColor;
        EventManager.LevelEvents.OnLevelChangeLightColorsCallback -= ChangeCountdownBarColor;
    }

    public void ChangeMaterialColor(Color32 color) => material.color = color;

    public void ChangeCountdownBarColor(Color32 color) => countdownBar.color = color;
}