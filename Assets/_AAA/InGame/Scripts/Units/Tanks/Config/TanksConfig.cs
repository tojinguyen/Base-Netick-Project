using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

[CreateAssetMenu(fileName = "TanksConfig", menuName = "AAA/InGame/Unit/Tanks/TanksConfig")]
public class TanksConfig : ScriptableObject
{
    [SerializeField] private TankConfig[] _tanks;

    public TankConfig GetTankConfig(TankType tankType)
    {
        foreach (var tank in _tanks)
        {
            if (tank.TankType == tankType)
            {
                return tank;
            }
        }

        return null;
    }

#if UNITY_EDITOR
    [ContextMenu("Load All TankConfigs")]
    private void LoadAllTankConfigs()
    {
        // Load all TankConfig assets in the project
        var tankConfigs = AssetDatabase.FindAssets("t:tankconfig");
        _tanks = new TankConfig[tankConfigs.Length];

        for (var i = 0; i < tankConfigs.Length; i++)
        {
            var path = AssetDatabase.GUIDToAssetPath(tankConfigs[i]);
            _tanks[i] = AssetDatabase.LoadAssetAtPath<TankConfig>(path);
        }

        // Mark the ScriptableObject as dirty to ensure changes are saved
        EditorUtility.SetDirty(this);
    }
#endif
}