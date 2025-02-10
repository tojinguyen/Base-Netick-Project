using Netick;
using Netick.Unity;

public class UserInputHandler : NetworkBehaviour
{
    public readonly static int TickDelayScale30Fps = 3;
    
    [Networked] private NetworkArrayStruct16<UserInputData> _queueInput { get; set; }
    [Networked] private int _index { get; set; }
    [Networked] public UserInputData InputData { get; set; }

    private int _tickDelay;

    public override void NetworkStart()
    {
        base.NetworkStart();
        _tickDelay = TickDelayScale30Fps * (int)Sandbox.Config.TickRate / 30;
    }

    public override void NetworkFixedUpdate()
    {
        base.NetworkFixedUpdate();
        _index++;

        if (_queueInput.Length <= _index)
            _index = 0;

        _queueInput = _queueInput.Set(_index, FetchInput(out UserInputData input)
            ? input
            : new UserInputData()
            {
                Tick = Sandbox.Tick.TickValue,
                Movement = InputData.Movement,
                LookDirection = InputData.LookDirection,
                Shoot = InputData.Shoot
            });
        
        for (var index = 0; index < _queueInput.Length; index++)
        {
            var i = _queueInput[index];
            if (Sandbox.Tick.TickValue - i.Tick != _tickDelay)
                continue;
            InputData = i;
            break;
        }
    }
}
