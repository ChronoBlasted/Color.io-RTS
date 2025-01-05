using BaseTemplate.Behaviours;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardManager : MonoSingleton<BoardManager>
{
    [SerializeField] List<Cell> cells = new List<Cell>();
    [SerializeField] List<PawnController> pawns = new List<PawnController>();
    [SerializeField] List<SpawnPlayer> spawnPlayers = new List<SpawnPlayer>();

    public void Init()
    {
        GameManager.Instance.OnGameStateChanged += HandleStateChange;
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
        foreach (var spawn in spawnPlayers)
        {
            spawn.enabled = false;
        }
    }
    void HandleGame()
    {
        foreach (var spawn in spawnPlayers)
        {
            spawn.enabled = true;
        }
    }
    void HandleEnd()
    {
        foreach (var spawn in spawnPlayers)
        {
            spawn.enabled = false;
        }
    }
    void HandlePause()
    {
    }

    public SpawnPlayer GetSpawnByColor(Team color)
    {
        SpawnPlayer spawn = null;

        foreach (SpawnPlayer s in spawnPlayers)
        {
            if (s.Team == color)
            {
                spawn = s;
                break;
            }
        }

        return spawn;
    }

    public Cell GetClosestCell(Vector3 position, Team colorToIgnore)
    {
        Cell closestCell = null;

        float closestDist = Mathf.Infinity;

        foreach (Cell cell in cells)
        {
            if (cell.CellTeam == colorToIgnore) continue;

            var dist = Vector3.Distance(position, cell.transform.position);

            if (dist < closestDist)
            {
                closestCell = cell;
                closestDist = dist;
            }
        }

        return closestCell;
    }

    public void AddPawn(PawnController pawnToAdd)
    {
        pawns.Add(pawnToAdd);

        var spawn = GetSpawnByColor(pawnToAdd.Team);

        pawnToAdd.OnPawnDie += RemovePawnFromList;
    }


    public PawnController GetClosestPawn(Vector3 position, Team colorToIgnore)
    {
        PawnController closestPawn = null;

        float closestDist = Mathf.Infinity;

        foreach (PawnController pawn in pawns)
        {
            if (pawn.Team == colorToIgnore) continue;

            var dist = Vector3.Distance(position, pawn.transform.position);

            if (dist < closestDist)
            {
                closestPawn = pawn;
                closestDist = dist;
            }
        }

        if (closestPawn == null)
        {
            GameManager.Instance.UpdateStateToEnd(colorToIgnore == PlayerManager.Instance.PlayerTeamColor);
            return null;
        }

        return closestPawn;
    }

    void RemovePawnFromList(PawnController pawnToAdd)
    {
        pawns.Remove(pawnToAdd);

        var spawn = GetSpawnByColor(pawnToAdd.Team);

        pawnToAdd.OnPawnDie -= RemovePawnFromList;
    }

    public void ResetColorByColor(Team team)
    {
        foreach (Cell cell in cells)
        {
            if (cell.CellTeam == team) cell.ResetColor();
        }

        var tempPawnList = new List<PawnController>();

        foreach (PawnController pawn in pawns)
        {
            tempPawnList.Add(pawn);
        }

        foreach (PawnController pawn in tempPawnList)
        {
            if (pawn.Team == team) pawn.Die();
        }
    }

    public int GetTotalCellCount => cells.Count;
}
