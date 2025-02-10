using Cysharp.Threading.Tasks;
using Netick;
using UnityEngine;

public class TankUnit : BaseUnit
{
    [SerializeField] protected UserInputHandler userInputHandler;

#if CLIENT_BUILD
    [Space(8), Header("Client")] [SerializeField]
    protected TankSkinRenderer tankSkinRenderer;
#endif

    [Networked] public UserMatchInfo UserMatchInfo { get; set; }

    public UserInputHandler UserInputHandler => userInputHandler;

    public override void NetworkStart()
    {
        base.NetworkStart();
#if CLIENT_BUILD
        SetupTankRenderer();
#endif
    }

    private void SetupTankRenderer()
    {
#if CLIENT_BUILD
        if (Sandbox.IsServer)
            return;

        tankSkinRenderer.SetupSkin(TankType.IronMan).Forget();
#endif
    }

    protected override void OnValidate()
    {
        base.OnValidate();
        userInputHandler ??= GetComponent<UserInputHandler>();
    }
}