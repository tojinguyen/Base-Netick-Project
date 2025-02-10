using System;
using Netick;
using UnityEngine.SceneManagement;

public struct StartGameRequestPayload
{
    public int Port;
    public NetworkArrayStruct4<UserMatchInfo> Players;
    public int GameMode;
}

public struct ServerMatchClearPayload
{
    public Scene Scene;
    public int Port;
}

public struct GameServerShutdownPayload{
}


[Serializable]
public struct UserMatchInfo
{
    public NetworkString32 UserID;
    public NetworkString32 UserName;
    public TeamSide TeamSide;
    public NetworkBool IsBot;
    public TankType TankType;
}