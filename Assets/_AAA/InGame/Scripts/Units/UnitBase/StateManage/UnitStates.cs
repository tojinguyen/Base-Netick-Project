using System;
using Netick;
using Netick.Unity;
using UnityEngine;

[DisallowMultipleComponent]
public class UnitStates : MonoBehaviour
{
    private const int MAX_STATE_COUNT = 20;

    [Networked(size:MAX_STATE_COUNT)] public NetworkArray<StateType> States { get; set; } = new (MAX_STATE_COUNT);

    public Action<StateType> OnStateAdded;
    public Action<StateType> OnStateRemoved;

    private void OnEnable()
    {
        States.Clear(MAX_STATE_COUNT);
    }

    public void AddState(StateType state)
    {
        if (States.Contains(state))
            return;

        States.Add(state);
        OnStateAdded?.Invoke(state);
    }

    public void RemoveState(StateType state)
    {
        if (!States.Contains(state))
            return;

        States.Remove(state);
        OnStateRemoved?.Invoke(state);
    }

    public bool HasState(StateType state) => States.Contains(state);
}
