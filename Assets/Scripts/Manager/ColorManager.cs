using BaseTemplate.Behaviours;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorManager : MonoSingleton<ColorManager>
{
    public Color selectedColor;
    [SerializeField] Color blueColor, redColor, yellowColor, greenColor, whiteColor;
    [SerializeField] Material blueMat, redMat, yellowMat, greenMat, whiteMat;

    public Color GetColorByTeam(Team team)
    {
        switch (team)
        {
            case Team.Blue:
                return blueColor;
            case Team.Red:
                return redColor;
            case Team.Yellow:
                return yellowColor;
            case Team.Green:
                return greenColor;
            default:
                return whiteColor;
        }
    }

    public Material GetMaterialByTeam(Team team)
    {
        switch (team)
        {
            case Team.Blue:
                return blueMat;
            case Team.Red:
                return redMat;
            case Team.Yellow:
                return yellowMat;
            case Team.Green:
                return greenMat;
            default:
                return whiteMat;
        }
    }


}
