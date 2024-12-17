using System.Collections;
using UnityEngine;
using static UnityEngine.UI.GridLayoutGroup;

public class UnitDieState : State<Unit>
{
    Unit.UnitAnimationName animationName = Unit.UnitAnimationName.Die;

    public override void Enter()
    {
        _owner.PlayAnimation(animationName);

        _owner.PawnController.OnPawnDie?.Invoke(_owner.PawnController);

        _owner.ReleaseInPool();
    }

    public override void Exit()
    {
    }

    public override void Update()
    {

    }
}
