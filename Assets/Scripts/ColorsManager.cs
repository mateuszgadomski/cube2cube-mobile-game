using UnityEngine;

public class ColorsManager : MonoBehaviour
{
    public static ColorsManager Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    public Color32 lightGreyColor = new(130, 130, 130, 255);
    public Color32 darkGreyColor = new(96, 96, 96, 255);

    public Color32 lightBrownColor = new(217, 181, 150, 255);
    public Color32 darkBrownColor = new(115, 62, 32, 255);
}