using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour
{
    Team cellTeam = Team.None;
    [SerializeField] MeshRenderer _meshRenderer;

    PawnController _pawnInCell;

    public Team CellTeam { get => cellTeam; }
    public PawnController PawnInCell { get => _pawnInCell; set => _pawnInCell = value; }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 10)
        {
            PawnController pawn = other.GetComponent<PawnController>();
            pawn.OnPawnDie += ResetCell;

            if (pawn.Team == cellTeam) return;

            if (_pawnInCell != null && pawn.Team != _pawnInCell.Team)
            {
                _pawnInCell.Unit.StateMachine.SetState<UnitDieState>();
                pawn.Unit.StateMachine.SetState<UnitDieState>();
            }
            else
            {
                _pawnInCell = pawn;

                ScoreManager.Instance.AddScore(cellTeam, -1);

                cellTeam = pawn.Team;

                ScoreManager.Instance.AddScore(cellTeam);

                _meshRenderer.sharedMaterial = ColorManager.Instance.GetMaterialByTeam(cellTeam);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == 10)
        {
            PawnController pawn = other.GetComponent<PawnController>();
            pawn.OnPawnDie -= ResetCell;

            if (_pawnInCell == pawn) _pawnInCell = null;
        }
    }

    void ResetCell(PawnController pawnToAdd)
    {
        _pawnInCell = null;
    }
}
