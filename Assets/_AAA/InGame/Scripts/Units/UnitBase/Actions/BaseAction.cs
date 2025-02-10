using Netick.Unity;
using UnityEngine;

public abstract class BaseAction : NetworkBehaviour
{
    [SerializeField] protected BaseUnit baseUnit;
    
    public abstract bool CanExecute();

    protected virtual void Execute()
    {
    }
    
    internal virtual void ResetAction()
    {
    }
    
    protected virtual void OnValidate()
    {
        baseUnit ??= GetComponentInParent<BaseUnit>();
    }
}
