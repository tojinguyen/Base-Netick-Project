using UnityEngine;

[CreateAssetMenu(fileName = "UnitStatConfig", menuName = "AAA/InGame/Unit/UnitStatConfig")]
public class UnitStatConfig : ScriptableObject
{
    [System.Serializable]
    public class UnitStat
    {
        public UnitStatType StatType; 
        public float Value; 
    }

    [SerializeField] private UnitStat[] _stats; 
}
