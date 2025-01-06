using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class UnitAttackBaseState : State<Unit>
{
    Unit.UnitAnimationName animationName = Unit.UnitAnimationName.Run;

    Transform spawnToReach;

    public override void Enter()
    {
        _owner.PlayAnimation(animationName);

        _owner.SetVelocity(2);

        _owner.SetDestination(FincClosestBase());
    }

    public override void Exit()
    {
        _owner.SetVelocity(0);
    }

    public override void Update()
    {
        if (_owner.Agent.pathStatus == NavMeshPathStatus.PathComplete)
        {
            _owner.SetDestination(FincClosestBase());
        }
        else if (_owner.Agent.pathStatus == NavMeshPathStatus.PathPartial)
        {
            Debug.Log("Find New Base because invalid path");

            _owner.SetDestination(FincClosestBase());
        }
        else if (spawnToReach.GetComponent<SpawnPlayer>().IsCastleAlive == false)
        {
            Debug.Log("Base is dead, changing target...");

            _owner.SetDestination(FincClosestBase());
        }
    }

    Vector3 FincClosestBase()
    {
        spawnToReach = BoardManager.Instance.GetClosestSpawn(_owner.transform.position, _owner.PawnController.Team);

        if (spawnToReach == null)
        {
            _stateMachine.SetState<UnitDefendState>();

            return Vector3.zero;
        }

        return spawnToReach.transform.position;
    }
}
