using UnityEngine;

public interface IDamageable
{
    public void TakeDamage(DamageDetail damageDetail);
}


public struct DamageDetail
{
    public BaseUnit Attacker;
    public BaseUnit Target;
    public int HealthDamage;
    public int StaminaDamage;
    public Vector3 HitPoint;
    public Vector3 HitDirection;
    public bool IsCritical;
    
    public Vector3 ConvertToLocalHitDirection(Transform transform) => transform.InverseTransformDirection(HitDirection).normalized;
}