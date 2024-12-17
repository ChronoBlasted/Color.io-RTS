using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class UnitAttackUnitState : State<Unit>
{
    Unit.UnitAnimationName animationName = Unit.UnitAnimationName.Run;

    PawnController pawnToReach;

    public override void Enter()
    {
        _owner.PlayAnimation(animationName);

        _owner.SetVelocity(2);

        _owner.SetDestination(FincClosestEnemy());

    }

    public override void Exit()
    {
        _owner.SetVelocity(0);
    }

    public override void Update()
    {
        if (_owner.Agent.pathStatus == NavMeshPathStatus.PathComplete)
        {
            _owner.SetDestination(FincClosestEnemy());
        }
        else if (_owner.Agent.pathStatus == NavMeshPathStatus.PathPartial)
        {
            Debug.Log("Find New Cell because invalid path");

            _owner.SetDestination(FincClosestEnemy());
        }
        else if (pawnToReach.gameObject.activeSelf == false)
        {
            Debug.Log("Enemy is dead changing target");

            _owner.SetDestination(FincClosestEnemy());
        }
    }

    Vector3 FincClosestEnemy()
    {
        pawnToReach = BoardManager.Instance.GetClosestPawn(_owner.transform.position, _owner.PawnController.Team);

        return pawnToReach.transform.position;
    }
}
