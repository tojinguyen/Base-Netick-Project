using UnityEngine;

[CreateAssetMenu(fileName = "TankConfig", menuName = "AAA/InGame/Unit/Tanks/TankConfig")]
public class TankConfig : ScriptableObject
{
   [SerializeField] private TankType _tankType;
   [SerializeField] private UnitStatConfig _tankStatConfig;
   [SerializeField] private TankRendererConfig _tankRendererConfig;
   
   public TankType TankType => _tankType;
   public UnitStatConfig TankStatConfig => _tankStatConfig;
   public TankRendererConfig TankRendererConfig => _tankRendererConfig;
}
