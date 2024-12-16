using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Unit : MonoBehaviour
{
    public enum UnitAnimationName
    {
        None,
        Idle,
        Walk,
        Run,
        Die,
    }

    [SerializeField] Animator _animator;
    [SerializeField] NavMeshAgent _agent;
    [SerializeField] PawnController _pawnController;

    FiniteStateMachine<Unit> _stateMachine;

    public NavMeshAgent Agent { get => _agent; }
    public PawnController PawnController { get => _pawnController; }

    public void Init()
    {
        _stateMachine = new FiniteStateMachine<Unit>(this);

        _stateMachine.AddState(new UnitIdleState());
        _stateMachine.AddState(new UnitAttackUnitState());
        _stateMachine.AddState(new UnitAttackBaseState());
        _stateMachine.AddState(new UnitExpandState());
        _stateMachine.AddState(new UnitDieState());


        _stateMachine.SetState<UnitExpandState>();
    }

    private void Update()
    {
        _stateMachine.Update();
    }

    public void PlayAnimation(UnitAnimationName animationName)
    {
        _animator.Play(animationName.ToString());
    }

    public void SetVelocity(float newSpeed)
    {
        _agent.speed = newSpeed;
    }

    public void SetDestination(Vector3 newDestination)
    {
        _agent.SetDestination(newDestination);
    }
}
