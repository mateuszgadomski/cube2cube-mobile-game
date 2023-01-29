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

    public Color32 LightGreyColor = new(130, 130, 130, 255);
    public Color32 DarkGreyColor = new(96, 96, 96, 255);

    public Color32 LightGreenColor = new(163, 177, 138, 255);
    public Color32 DarkGreenColor = new(88, 129, 87, 255);

    public Color32 LightBlueColor = new(119, 141, 169, 255);
    public Color32 DarkBlueColor = new(65, 90, 119, 255);

    public Color32 LightTurquoiseColor = new(120, 198, 163, 255);
    public Color32 DarkTurquoiseColor = new(86, 171, 145, 255);
}