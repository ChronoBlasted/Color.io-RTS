using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitAttackUnitState : State<Unit>
{
    Unit.UnitAnimationName animationName = Unit.UnitAnimationName.Run;

    public override void Enter()
    {
        _owner.PlayAnimation(animationName);

        _owner.SetVelocity(1);
    }

    public override void Exit()
    {
        _owner.SetVelocity(0);
    }

    public override void Update()
    {
        // MOVE PLAYER
    }
}
