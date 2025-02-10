using Netick;
using UnityEngine;

public abstract class UnitStat : MonoBehaviour
{
    [SerializeField] protected BaseUnit baseUnit;
    [SerializeField] protected UnitStatType unitStatType;
    [SerializeField] protected UnitRuntimeStats unitRuntimeStats;

    [Networked] private float _currentValue { get; set; }

    protected float CurrentValue
    {
        get => _currentValue;
        set
        {
            if (!baseUnit.IsServer)
                return;
            _currentValue = value;
        }
    }

    public virtual void InitValue()
    {
        _currentValue = unitRuntimeStats.GetStatValue(unitStatType);
    }

    protected virtual void OnValidate()
    {
        var root = transform.root;
        unitRuntimeStats ??= GetComponent<UnitRuntimeStats>();
        baseUnit ??= root.GetComponent<BaseUnit>();
    }
}
