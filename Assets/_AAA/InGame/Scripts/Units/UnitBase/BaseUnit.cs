using MessagePipe;
using Netick;
using Netick.Unity;
using UnityEngine;
using VContainer;
#if UNITY_EDITOR
using System.Reflection;
#endif

/// <summary>
/// All units in the game must inherit from this class.
/// </summary>
[DisallowMultipleComponent]
[RequireComponent(typeof(NetworkObject))]
public class BaseUnit : NetworkBehaviour
{
    [SerializeField] protected UnitStates unitStates;
    [SerializeField] protected UnitRuntimeStats unitRuntimeStats;
    [SerializeField] protected UnitMovement unitMovement;
    
    [Networked] private TeamSide _teamSide { get; set; }

    public UnitStates UnitStates => unitStates;
    public UnitRuntimeStats UnitRuntimeStats => unitRuntimeStats;
    public UnitMovement UnitMovement => unitMovement;

    public virtual void SetupUnit()
    {
        unitRuntimeStats.InitData();
    }

    protected virtual void OnValidate()
    {
        unitStates ??= GetComponentInChildren<UnitStates>();
        unitRuntimeStats ??= GetComponentInChildren<UnitRuntimeStats>();
        unitMovement ??= GetComponentInChildren<UnitMovement>();
    }
}