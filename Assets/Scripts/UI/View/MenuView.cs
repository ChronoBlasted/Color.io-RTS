using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuView : View
{
    public void PlayGame()
    {
        GameManager.Instance.UpdateStateToGame();
    }

    public void ChangeColorTeam(int indexColor)
    {
        switch (indexColor)
        {
            case 0:
                PlayerManager.Instance.PlayerTeamColor = Team.Blue;
                break;
            case 1:
                PlayerManager.Instance.PlayerTeamColor = Team.Red;
                break;
            case 2:
                PlayerManager.Instance.PlayerTeamColor = Team.Yellow;
                break;
            case 3:
                PlayerManager.Instance.PlayerTeamColor = Team.Green;
                break;
            default:
                PlayerManager.Instance.PlayerTeamColor = Team.Blue;
                break;
        }

        CameraManager.Instance.SetTargetLocation(BoardManager.Instance.GetSpawnByColor(PlayerManager.Instance.PlayerTeamColor).transform);
    }
}