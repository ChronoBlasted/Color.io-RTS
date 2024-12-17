using UnityEngine;

public class UnitDefendState : State<Unit>
{
    Unit.UnitAnimationName animationName = Unit.UnitAnimationName.Defend;

    public override void Enter()
    {
        _owner.PlayAnimation(animationName);
    }

    public override void Exit()
    {
    }

    public override void Update()
    {
    }
}
