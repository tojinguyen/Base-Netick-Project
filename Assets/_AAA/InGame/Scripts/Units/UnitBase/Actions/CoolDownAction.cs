using Netick;
using UnityEngine;

public abstract class CoolDownAction : UnitActionLockByState
{
    [SerializeField] protected float cooldownTime;

    [Networked] protected float CurrentCooldownTime { get; set; }

    public override bool CanExecute() => base.CanExecute() && CurrentCooldownTime <= 0;

    protected override void Execute()
    {
        if(!CanExecute())
            return;
        CurrentCooldownTime = cooldownTime;
    }

    public override void NetworkFixedUpdate()
    {
        base.NetworkFixedUpdate();
        if (CurrentCooldownTime >= 0)
        {
            CurrentCooldownTime -= Time.fixedDeltaTime;
        }
    }
}
