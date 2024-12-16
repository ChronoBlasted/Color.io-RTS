using UnityEngine;
using UnityEngine.AI;

public class UnitExpandState : State<Unit>
{
    Unit.UnitAnimationName animationName = Unit.UnitAnimationName.Run;

    Cell cellToReach;

    public override void Enter()
    {
        _owner.PlayAnimation(animationName);

        _owner.SetVelocity(3);

        _owner.SetDestination(FindClosestCellToConquer());
    }

    public override void Exit()
    {
        _owner.SetVelocity(0);
    }

    public override void Update()
    {
        float dist = _owner.Agent.remainingDistance;

        if (_owner.Agent.pathStatus == NavMeshPathStatus.PathComplete)
        {
            _owner.SetDestination(FindClosestCellToConquer());
        }
        else if (_owner.Agent.pathStatus == NavMeshPathStatus.PathPartial)
        {
            Debug.Log("Find New Cell because invalid path");

            FindClosestCellToConquer();
        }
        else if (cellToReach.CellTeam == _owner.PawnController.Team)
        {
            FindClosestCellToConquer();
        }
    }

    Vector3 FindClosestCellToConquer()
    {
        cellToReach = BoardManager.Instance.GetClosestCell(_owner.transform.position, _owner.PawnController.Team);

        return cellToReach.transform.position;
    }
}