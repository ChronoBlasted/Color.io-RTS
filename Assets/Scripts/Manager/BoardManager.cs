using BaseTemplate.Behaviours;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardManager : MonoSingleton<BoardManager>
{
    [SerializeField] List<Cell> cells = new List<Cell>();
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
}
