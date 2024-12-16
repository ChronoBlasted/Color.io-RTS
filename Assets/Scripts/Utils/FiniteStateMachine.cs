using UnityEngine;
using System.Collections.Generic;

public class FiniteStateMachine<T>
{
    private T _owner;
    private Dictionary<System.Type, State<T>> _states;
    private State<T> _currentState;

    public FiniteStateMachine(T owner)
    {
        _owner = owner;
        _states = new Dictionary<System.Type, State<T>>();
    }

    public State<T> CurrentState { get => _currentState; set => _currentState = value; }

    public void AddState(State<T> state)
    {
        state.SetState(this, _owner);
        _states[state.GetType()] = state;
    }

    public void SetState<TS>() where TS : State<T>
    {
        if (_currentState != null)
        {
            _currentState.Exit();
        }

        if (_states.ContainsKey(typeof(TS)))
        {
            _currentState = _states[typeof(TS)];
            _currentState.Enter();
        }
    }

    public void Update()
    {
        if (_currentState != null)
            _currentState.Update();
    }


}