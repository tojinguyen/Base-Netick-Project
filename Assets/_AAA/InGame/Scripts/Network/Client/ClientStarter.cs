using Netick;
using Netick.Samples;
using Netick.Unity;
using UnityEngine;

public class ClientStarter : GameStarter
{
    public override void OnConnectFailed(NetworkSandbox sandbox, ConnectionFailedReason reason)
    {
        base.OnConnectFailed(sandbox, reason);
        Debug.Log($"Connection failed: {reason}");
    }

    public override void OnConnectedToServer(NetworkSandbox sandbox, NetworkConnection server)
    {
        base.OnConnectedToServer(sandbox, server);
        Debug.Log($"Connected to server {server.Id}");
    }
}
