using UnityEngine;

public class CameraSettings : MonoBehaviour
{
    [SerializeField] private Camera mainCamera;

    private void Start()
    {
        EventManager.LevelEvents.OnLevelChangeLightColorsCallback += ChangeBackgroundColor;
    }

    private void OnDestroy()
    {
        EventManager.LevelEvents.OnLevelChangeLightColorsCallback -= ChangeBackgroundColor;
    }

    public void ChangeBackgroundColor(Color32 color) => mainCamera.backgroundColor = color;
}