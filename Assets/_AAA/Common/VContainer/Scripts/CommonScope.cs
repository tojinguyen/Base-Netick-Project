using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using VContainer;
using VContainer.Unity;

[DisallowMultipleComponent]
public class CommonScope : LifetimeScope
{
    [SerializeField] private MonoBehaviour[] _injectableMonoBehaviour;

    protected override void Awake()
    {
        base.Awake();
        if (Container == null)
            return;
        foreach (var behaviour in _injectableMonoBehaviour)
            if (behaviour)
                Container.Inject(behaviour);
    }

    public void ReplaceMonoBehavioursInject(List<MonoBehaviour> monoBehaviours) =>
        _injectableMonoBehaviour = monoBehaviours.ToArray();
}

#region Editor
#if UNITY_EDITOR
[UnityEditor.CustomEditor(typeof(CommonScope))]
public class LifetimeScopeRefInjectEditor : UnityEditor.Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        if (!GUILayout.Button(("Get MonoBehaviours Injected"))) return;
        GetMonoBehaviourInjected(target);
    }

    private static void GetMonoBehaviourInjected(UnityEngine.Object target)
    {
        var commonScope = (CommonScope)target;
        var monoBehaviours = commonScope.GetComponentsInChildren<MonoBehaviour>(true);
        var listInjected = new List<MonoBehaviour>();
        foreach (var mono in monoBehaviours)
        {
            if (!mono)
                continue;
            var monoType = mono.GetType();
            List<MethodInfo> methods = new();
            List<FieldInfo> fields = new();

            const BindingFlags bindingFlags = BindingFlags.Instance | BindingFlags.Static |
                                              BindingFlags.Public | BindingFlags.NonPublic |
                                              BindingFlags.DeclaredOnly;

            methods.AddRange(mono.GetType().GetMethods(bindingFlags));
            fields.AddRange(monoType.GetFields(bindingFlags));

            while (monoType.BaseType != null)
            {
                fields.AddRange(monoType.BaseType.GetFields(BindingFlags.Instance | BindingFlags.Static |
                                                           BindingFlags.Public | BindingFlags.NonPublic |
                                                           BindingFlags.DeclaredOnly));
                methods.AddRange(monoType.BaseType.GetMethods(BindingFlags.Instance | BindingFlags.Static |
                                                             BindingFlags.Public | BindingFlags.NonPublic |
                                                             BindingFlags.DeclaredOnly));
                monoType = monoType.BaseType;
            }

            foreach (var method in methods)
            {
                if (!HasInjectAttribute(method)) continue;
                listInjected.Add(mono);
                break;
            }
            foreach (var field in fields)
            {
                if (!HasInjectAttribute(field)) continue;
                listInjected.Add(mono);
                break;
            }
        }

        commonScope.ReplaceMonoBehavioursInject(listInjected);
        UnityEditor.EditorUtility.SetDirty(commonScope);
        UnityEditor.AssetDatabase.SaveAssets();
    }

    private static bool HasInjectAttribute(MethodInfo methodInfo)
    {
        var hasInjectAttribute = false;
        var attributes = Attribute.GetCustomAttributes(methodInfo);
        foreach (var attribute in attributes)
        {
            if (attribute.GetType() != typeof(InjectAttribute)) continue;
            hasInjectAttribute = true;
            break;
        }

        return hasInjectAttribute;
    }

    private static bool HasInjectAttribute(FieldInfo methodInfo)
    {
        var hasInjectAttribute = false;
        var attributes = Attribute.GetCustomAttributes(methodInfo);
        foreach (var attribute in attributes)
        {
            if (attribute.GetType() != typeof(InjectAttribute)) continue;
            hasInjectAttribute = true;
            break;
        }

        return hasInjectAttribute;
    }

    private void OnValidate()
    {
        GetMonoBehaviourInjected(target);
    }
}

#endif
#endregion