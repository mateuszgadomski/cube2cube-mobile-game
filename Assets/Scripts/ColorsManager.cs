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

    [HideInInspector] public Color32 lightGreenColor = new(163, 177, 138, 255);
    [HideInInspector] public Color32 darkGreenColor = new(88, 129, 87, 255);

    [HideInInspector] public Color32 lightBlueColor = new(119, 141, 169, 255);
    [HideInInspector] public Color32 darkBlueColor = new(65, 90, 119, 255);

    [HideInInspector] public Color32 lightTurquoiseColor = new(120, 198, 163, 255);
    [HideInInspector] public Color32 darkTurquoiseColor = new(86, 171, 145, 255);
}