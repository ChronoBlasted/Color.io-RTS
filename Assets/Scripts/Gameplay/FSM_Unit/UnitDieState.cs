public class UnitDieState : State<Unit>
{
    Unit.UnitAnimationName animationName = Unit.UnitAnimationName.Die;

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
