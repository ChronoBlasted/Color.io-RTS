using BaseTemplate.Behaviours;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoSingleton<CameraManager>
{
    [SerializeField] Transform cameraTarget;

    public void Init()
    {
        GameManager.Instance.OnGameStateChanged += HandleStateChange;

        SetTargetLocation(BoardManager.Instance.GetSpawnByColor(Team.Blue).transform);
    }

    void HandleStateChange(GameState newState)
    {
        switch (newState)
        {
            case GameState.Menu:
                HandleMenu();
                break;
            case GameState.Game:
                HandleGame();
                break;
            case GameState.End:
                HandleEnd();
                break;
            case GameState.Pause:
                HandlePause();
                break;
            default:
                break;
        }
    }

    void HandleMenu()
    {
        cameraTarget.DORotate(new Vector3(0, 90, 0), 5).SetEase(Ease.Linear).SetLoops(-1, LoopType.Incremental);
    }

    void HandleGame()
    {
        DOTween.Kill(cameraTarget.transform, true);

        cameraTarget.DORotate(new Vector3(0, 0, 0), .5f);
    }
    void HandleEnd()
    {

    }
    void HandlePause()
    {
    }

    public void SetTargetLocation(Transform target)
    {
        cameraTarget.transform.DOMove(target.position, 1f);
    }

}
