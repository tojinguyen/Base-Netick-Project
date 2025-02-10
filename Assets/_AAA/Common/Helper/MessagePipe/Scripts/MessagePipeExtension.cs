using Cysharp.Threading.Tasks;
using System;
using UnityEngine;

public static class MessagePipeExtension
{
    public static void DisposeOnDestroy<T>(this T disposable, MonoBehaviour comp) where T : IDisposable
    {
        comp.GetCancellationTokenOnDestroy().Register(obj => ((T)obj).Dispose(), disposable);
    }
}
