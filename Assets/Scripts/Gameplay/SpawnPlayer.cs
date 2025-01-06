using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpawnPlayer : MonoBehaviour
{
    public List<FlockingAgent> flockMates;

    [SerializeField] Team _team;
    [SerializeField] List<Transform> _unitSpawns;

    [SerializeField] GameObject _spawnNice;
    [SerializeField] GameObject _spawnBreak;


    Vector2 _maxMinSpawnRate = new Vector2(1, 5);

    float nextSpawn = 0f;
    bool isCastleAlive = true;

    int indexUnitSpawn;

    public float SpawnRate { get => GetSpawnRate(); }
    public Team Team { get => _team; }
    public bool IsCastleAlive { get => isCastleAlive; }

    private void Update()
    {
        GetSpawnRate();

        UIManager.Instance.GameView.PlayerInfoLayout.UpdateNewUnitIn(nextSpawn - Time.time);


        if (CanSpawn() && isCastleAlive)
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
        Transform spawn = _unitSpawns[Random.Range(0, _unitSpawns.Count)];

        PawnController unitToSpawn = PoolManager.Instance.SpawnFromPool("Pawn", spawn.transform.position, spawn.transform.rotation).GetComponent<PawnController>();

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

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 10 && isCastleAlive)
        {
            PawnController pawn = other.GetComponent<PawnController>();

            if (pawn.Team == _team) return;

            DestroySpawn();
        }
    }

    void DestroySpawn()
    {
        _spawnNice.SetActive(false);
        _spawnBreak.SetActive(true);

        isCastleAlive = false;

        BoardManager.Instance.ResetColorByColor(_team);

        if (_team == PlayerManager.Instance.PlayerTeamColor) GameManager.Instance.UpdateStateToEnd(false);
    }




}
