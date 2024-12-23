using BaseTemplate.Behaviours;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardManager : MonoSingleton<BoardManager>
{
    [SerializeField] List<Cell> cells = new List<Cell>();
    [SerializeField] List<PawnController> pawns = new List<PawnController>();
    public void Init()
    {

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

        return closestPawn;
    }

    void RemovePawnFromList(PawnController pawnToAdd)
    {
        pawns.Remove(pawnToAdd);

        pawnToAdd.OnPawnDie -= RemovePawnFromList;
    }

    public int GetTotalCellCount => cells.Count;
}
