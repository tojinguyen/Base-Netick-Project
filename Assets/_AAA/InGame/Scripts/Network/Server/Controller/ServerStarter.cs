using Netick;
using Netick.Samples;
using Netick.Unity;
using UnityEngine;

public class ServerStarter : GameStarter
{
    public override void OnClientConnected(NetworkSandbox sandbox, NetworkConnection client)
    {
        base.OnClientConnected(sandbox, client);
        Debug.Log($"Client {client.Id} connected!");
    }
}
