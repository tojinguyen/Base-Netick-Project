using Netick.Unity;

#if CLIENT_BUILD || HOST_BUILD
public class ClientInputSender : NetworkEventsListener
{
    private int _tick;
    private UserInputData _inputData;
    
    public override void OnInput(NetworkSandbox sandbox)
    {
        base.OnInput(sandbox);
        if (sandbox.Tick.TickValue > _tick)
        {
            _tick = sandbox.Tick.TickValue;
            _inputData = default;
        }
            
        _inputData = UserInputDataHolder.UserInput;
        _inputData.Tick = sandbox.Tick;
        Sandbox.SetInput(_inputData);
    }
}

public static class UserInputDataHolder
{
    public static UserInputData UserInput;
}
#endif
