using Netick;
using UnityEngine;

public struct UserInputData : INetworkInput
{
    public int Tick;
    public Vector2 Movement;   
    public Vector2 LookDirection;
    public Vector2 Shoot;
}

