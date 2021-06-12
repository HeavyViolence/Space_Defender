using System.Collections.Generic;
using UnityEngine;
using System;

public abstract class BaseFSM : MonoBehaviour
{
    private IState _currentState = null;
    private List<Transition> _currentStateTransitions = new List<Transition>();

    private IState _defaultState = null;
    private readonly List<Transition> _defaultStateTransitions = new List<Transition>();

    private readonly Dictionary<string, List<Transition>> _transitions = new Dictionary<string, List<Transition>>();

    private void Awake()
    {
        OnSetup();
    }

    private void Update()
    {
        UpdateCurrentState();
    }

    private void FixedUpdate()
    {
        UpdateCurrentStatePhysics();
    }

    protected abstract void OnSetup();

    public void SetInitialState(IState state) => SetCurrentState(state);

    public void SetDefaultState(IState state) => _defaultState = state;

    public void AddDefaultStateTransition(Func<bool> condition) =>
        _defaultStateTransitions.Add(new Transition(_defaultState, condition));

    public void AddTransition(IState from, IState to, Func<bool> condition)
    {
        if (_transitions.TryGetValue(from.GetType().ToString(), out var t))
        {
            t.Add(new Transition(to, condition));
        }
        else
        {
            List<Transition> transitions = new List<Transition>
            {
                new Transition(to, condition)
            };

            _transitions.Add(from.GetType().ToString(), transitions);
        }
    }

    private void UpdateCurrentState()
    {
        Transition t = GetTransition();

        if (t != null) SetCurrentState(t.To);

        _currentState?.OnStateUpdate();
    }

    private void UpdateCurrentStatePhysics()
    {
        _currentState?.OnStatePhysicsUpdate();
    }

    private Transition GetTransition()
    {
        foreach (var t in _currentStateTransitions) if (t.Condition()) return t;
        foreach (var t in _defaultStateTransitions) if (t.Condition()) return t;

        return null;
    }

    private void SetCurrentState(IState state)
    {
        if (_currentState == state) return;

        _currentState?.OnStateExit();
        _currentState = state;

        if (_transitions.TryGetValue(_currentState.GetType().ToString(), out var t))
            _currentStateTransitions = t;

        else _currentStateTransitions = new List<Transition>(0);

        _currentState.OnStateEnter();
    }
}
