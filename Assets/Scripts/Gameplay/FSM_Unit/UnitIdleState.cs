using UnityEngine;

public class UnitIdleState : State<Unit>
{
    Unit.UnitAnimationName animationName = Unit.UnitAnimationName.Idle;

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
