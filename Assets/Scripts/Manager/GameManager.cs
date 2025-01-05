using BaseTemplate.Behaviours;
using DG.Tweening;
using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public enum GameState { None, Menu, Pause, Game, End }
public enum Team { None, Blue, Red, Yellow, Green }

public class GameManager : MonoSingleton<GameManager>
{
    public event Action<GameState> OnGameStateChanged;
    GameState _gameState;

    public GameState GameState { get => _gameState; }


    private void Awake()
    {
        PoolManager.Instance.Init();

        CameraController.Instance.Init();

        CameraManager.Instance.Init();

        UIManager.Instance.Init();

        BoardManager.Instance.Init();

        TimeManager.Instance.Init();

        UpdateStateToMenu();
    }

    public void UpdateGameState(GameState newState)
    {
        _gameState = newState;

        //Debug.LogWarning("New GameState : " + _gameState);

        switch (_gameState)
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
            default:
                break;
        }

        OnGameStateChanged?.Invoke(_gameState);
    }

    void HandleMenu()
    {
    }

    void HandleGame()
    {
    }

    void HandleEnd()
    {
    }

    void HandleWait()
    {
    }

    public void UpdateStateToMenu() => UpdateGameState(GameState.Menu);
    public void UpdateStateToGame() => UpdateGameState(GameState.Game);
    public void UpdateStateToEnd(bool playerWin)
    {
        UIManager.Instance.EndGameView.UpdateEnd(playerWin);
        UpdateGameState(GameState.End);
    }


    private void Update()
    {
        if (Keyboard.current.rKey.isPressed) ReloadScene();
    }

    public void ReloadScene()
    {
        DOTween.KillAll();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void QuitApp() => Application.Quit();
}