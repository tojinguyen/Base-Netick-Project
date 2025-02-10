using System.Collections.Generic;
using System.Text;
using MessagePipe;
using Netick;
using Netick.Unity;
using UnityEngine;
using VContainer;

public class PlayerSpawnerController : NetworkEventsListener
{
    [SerializeField] private NetworkObject _tankPrefab;
    
    [SerializeField] private MapSpawnPosition _mapSpawnPosition;

    private readonly Dictionary<IEndPoint, NetworkString32> _playerIDDic = new(10);
    private readonly List<TankUnit> _tanks = new(10);
    private StartGameRequestPayload _matchInfo;

    [Inject] private ISubscriber<ServerMatchClearPayload> _matchClearPayload;
    [Inject] private ISubscriber<StartGameRequestPayload> _startGamePayload;

    // private void OnEnable()
    // {
    //     _startGamePayload.Subscribe(OnStartGameServerPayload, IsValidPayloadRequestStartGame).DisposeOnDestroy(this);
    //     _matchClearPayload.Subscribe(OnMatchClear, IsValidPayloadMatchClear).DisposeOnDestroy(this);
    // }

    public override void OnConnectRequest(NetworkSandbox sandbox, NetworkConnectionRequest request)
    {
        base.OnConnectRequest(sandbox, request);
        var userID = Encoding.ASCII.GetString(request.Data);
        if (_playerIDDic.ContainsKey(request.Source))
        {
            Debug.LogError($"User {userID} already send request connect");
            return;
        }

        ConsoleLogger.Log($"User {userID} OnConnectRequest");
        _playerIDDic[request.Source] = userID;
    }

    public override void OnClientConnected(NetworkSandbox sandbox, NetworkConnection client)
    {
        base.OnClientConnected(sandbox, client);
        // SetupTankForClient(_playerIDDic[client.EndPoint], client);
        
        // Test
        var pos = _mapSpawnPosition.GetSpawnPosition(TeamSide.Team1);
        var playerTank = Sandbox.NetworkInstantiate(_tankPrefab, pos, Quaternion.identity, client);
    }

    public override void OnClientDisconnected(NetworkSandbox sandbox, NetworkConnection client,
        TransportDisconnectReason transportDisconnectReason) => _playerIDDic.Remove(client.EndPoint);

    private void SetupTankForClient(NetworkString32 userId, NetworkConnection client)
    {
        foreach (var character in _tanks)
        {
            if (character.UserMatchInfo.UserID != userId)
                continue;
            character.InputSource = client;
            client.PlayerObject = character.gameObject;
            return;
        }
    }

    private void OnStartGameServerPayload(StartGameRequestPayload payload)
    {
        _matchInfo = payload;
        foreach (var userMatchInfo in payload.Players)
        {
            SpawnPlayer(userMatchInfo);
        }
    }

    private void SpawnPlayer(UserMatchInfo userMatchInfo)
    {
        // Spawn network Tank here
        var spawnPos = _mapSpawnPosition.GetSpawnPosition(userMatchInfo.TeamSide);
        var playerTank = Sandbox.NetworkInstantiate(_tankPrefab, spawnPos, Quaternion.identity);
        if (playerTank == null)
        {
            ConsoleLogger.LogError("Failed to spawn player tank");
            return;
        }

        if (!playerTank.TryGetComponent(out TankUnit tankUnit))
        {
            ConsoleLogger.LogError("Failed to get TankUnit component");
            return;
        }

        // Init data for tank here
        _tanks.Add(tankUnit);
    }

    private void OnMatchClear(ServerMatchClearPayload payload)
    {
        _tanks.Clear();
        _playerIDDic.Clear();
    }

    private bool IsValidPayloadRequestStartGame(StartGameRequestPayload payload) => Sandbox.Engine.Port == payload.Port;

    private bool IsValidPayloadMatchClear(ServerMatchClearPayload payload) =>
        gameObject.scene.handle == payload.Scene.handle;
}