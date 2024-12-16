using BaseTemplate.Behaviours;
using DG.Tweening;
using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public enum GameState { Menu, Pause, Game, End }

public class GameManager : MonoSingleton<GameManager>
{
    public event Action<GameState> OnGameStateChanged;
    GameState _gameState;

    public GameState GameState { get => _gameState; }

    private void Awake()
    {
        CameraController.Instance.Init();

        UpdateStateToGame();
    }

    public void UpdateGameState(GameState newState)
    {
        _gameState = newState;

        Debug.LogWarning("New GameState : " + _gameState);

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
    public void UpdateStateToEnd() => UpdateGameState(GameState.End);

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