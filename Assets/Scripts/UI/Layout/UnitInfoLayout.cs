using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UnitInfoLayout : MonoBehaviour
{
    [SerializeField] TMP_Text _title;
    [SerializeField] PawnController _controller;

    public void Init(int index, PawnController controller)
    {
        _title.text = "#" + index;
        _controller = controller;

        _controller.OnPawnDie += Die;
    }

    public void HandleOnIdle()
    {
        _controller.Unit.StateMachine.SetState<UnitIdleState>();
    }

    public void HandleOnAttackBase()
    {
        _controller.Unit.StateMachine.SetState<UnitAttackBaseState>();
    }

    public void HandleOnAttackUnit()
    {
        _controller.Unit.StateMachine.SetState<UnitAttackUnitState>();
    }

    public void HandleOnDefend()
    {
        _controller.Unit.StateMachine.SetState<UnitDefendState>();
    }

    public void HandleOnExpand()
    {
        _controller.Unit.StateMachine.SetState<UnitExpandState>();
    }

    void Die(PawnController pawnToAdd)
    {
        Destroy(gameObject);
    }
}
