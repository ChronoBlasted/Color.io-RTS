using UnityEngine;

public class UnitDefendState : State<Unit>
{
    Unit.UnitAnimationName animationName = Unit.UnitAnimationName.Walk;

    public override void Enter()
    {
        _owner.PlayAnimation(animationName);

        _owner.SetVelocity(1);

        BoardManager.Instance.GetSpawnByColor(_owner.PawnController.Team).flockMates.Add(_owner.PawnController.FlockingAgent);

        EnableFlocking(true);
    }

    public override void Exit()
    {
        EnableFlocking(false);

        BoardManager.Instance.GetSpawnByColor(_owner.PawnController.Team).flockMates.Remove(_owner.PawnController.FlockingAgent);

        _owner.SetVelocity(0);
    }

    public override void Update()
    {
    }

    private void EnableFlocking(bool enable)
    {
        _owner.PawnController.FlockingAgent.enabled = enable;
    }
}
