using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SpawnPlayer : MonoBehaviour
{
    [SerializeField] Team _team;
    [SerializeField] Transform _unitSpawn;

    Vector2 _maxMinSpawnRate = new Vector2(1, 5);

    float nextSpawn = 0f;

    int indexUnitSpawn;

    public float SpawnRate { get => GetSpawnRate(); }

    private void Update()
    {
        GetSpawnRate();

        UIManager.Instance.GameView.PlayerInfoLayout.UpdateNewUnitIn(nextSpawn - Time.time);


        if (CanSpawn())
        {
            SpawnUnit();

            nextSpawn = Time.time + SpawnRate;

        }
    }

    private bool CanSpawn()
    {
        return Time.time >= nextSpawn;
    }


    public void SpawnUnit()
    {
        PawnController unitToSpawn = PoolManager.Instance.SpawnFromPool("Pawn", _unitSpawn.transform.position, _unitSpawn.transform.rotation).GetComponent<PawnController>();

        indexUnitSpawn++;
        unitToSpawn.Init(_team, indexUnitSpawn);

        BoardManager.Instance.AddPawn(unitToSpawn);

        if (_team == PlayerManager.Instance.PlayerTeamColor)
        {
            UIManager.Instance.GameView.CreateUnitInfo(indexUnitSpawn, unitToSpawn);
        }
    }

    float GetSpawnRate()
    {
        float ratioRate = (float)ScoreManager.Instance.GetScoreByTeam(_team) / (float)BoardManager.Instance.GetTotalCellCount;

        float y = 4 * ratioRate + 1;

        UIManager.Instance.GameView.PlayerInfoLayout.UpdateUnitPerSecond(y / 5);

        return 5 / y;
    }
}
