using System;
using Cysharp.Threading.Tasks;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class TankSkinRenderer : MonoBehaviour
{
    [SerializeField] private TanksConfig _tanksConfig;
    [SerializeField] private Transform _visualRoot;

    public async UniTask SetupSkin(TankType tankType)
    {
        var tankConfig = _tanksConfig.GetTankConfig(tankType);
        var tankSkinAssetRef = tankConfig.TankRendererConfig.SkinAssetRef;

        var tankSkinGoPrefab =
            await AddressablesHelper.GetAssetAsync<GameObject>(tankSkinAssetRef, AddressablesFeatureName.InGame);

        if (tankSkinGoPrefab == null)
            return; 
        var tankSkinGo = Instantiate(tankSkinGoPrefab, _visualRoot, true);
        tankSkinGo.transform.localPosition = Vector3.zero;
        tankSkinGo.transform.localRotation = Quaternion.identity;
        tankSkinGo.transform.forward = _visualRoot.forward;
        tankSkinGo.SetActive(true);
    }

#if UNITY_EDITOR
    private void OnValidate()
    {
        if (_tanksConfig == null)
        {
            // Load all TankConfig assets in the project
            var tankConfigs = AssetDatabase.FindAssets("t:tanksconfig");
            var configs = new TankConfig[tankConfigs.Length];
            if (configs == null) throw new ArgumentNullException(nameof(configs));

            for (var i = 0; i < configs.Length; i++)
            {
                var path = AssetDatabase.GUIDToAssetPath(tankConfigs[i]);
                _tanksConfig = AssetDatabase.LoadAssetAtPath<TanksConfig>(path);
            }

            // Mark the ScriptableObject as dirty to ensure changes are saved
            EditorUtility.SetDirty(this);
        }
    }
#endif
}