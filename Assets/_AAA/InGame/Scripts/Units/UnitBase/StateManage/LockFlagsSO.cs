using UnityEngine;

[CreateAssetMenu(fileName = "LockFlagsSO", menuName = "AAA/InGame/Unit/LockFlagsSO")]
public class LockFlagsSO : ScriptableObject
{
    public StateType[] LockFlags;

    public bool IsContains(StateType stateType) => System.Array.Exists(LockFlags, x => x == stateType);

#if UNITY_EDITOR
    [ContextMenu("Get All State")]
    private void GetAllState()
    {
        var stateTypes = System.Enum.GetValues(typeof(StateType));
        LockFlags = new StateType[stateTypes.Length];
        for (var i = 0; i < stateTypes.Length; i++)
        {
            LockFlags[i] = (StateType)stateTypes.GetValue(i);
        }
    }
#endif
}
