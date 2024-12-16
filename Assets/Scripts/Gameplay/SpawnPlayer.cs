using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPlayer : MonoBehaviour
{
    [SerializeField] Team _team;
    [SerializeField] Transform _unitSpawn;

    private void Start()
    {
        StartCoroutine(SpawnUnits());
    }

    IEnumerator SpawnUnits()
    {
        while (true)
        {
            SpawnUnit();

            yield return new WaitForSeconds(5f);
        }
    }

    public void SpawnUnit()
    {
        PawnController unitToSpawn = PoolManager.Instance.SpawnFromPool("Pawn", _unitSpawn.transform.position, _unitSpawn.transform.rotation).GetComponent<PawnController>();

        unitToSpawn.Init(_team);
    }
}
