using UnityEngine;

public abstract class State<T>
{
    protected T _owner;
    protected FiniteStateMachine<T> _stateMachine;

    public virtual State<T> SetState(FiniteStateMachine<T> sm, T owner)
    {
        _stateMachine = sm;
        _owner = owner;
        return this;
    }


    public abstract void Enter();
    public abstract void Update();
    public abstract void Exit();
}