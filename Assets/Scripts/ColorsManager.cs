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

    [HideInInspector] public Color32 lightGreyColor = new(130, 130, 130, 255);
    [HideInInspector] public Color32 darkGreyColor = new(96, 96, 96, 255);

    [HideInInspector] public Color32 newCC = new(217, 187, 169, 255);
    [HideInInspector] public Color32 newBB = new(166, 86, 86, 255);
}