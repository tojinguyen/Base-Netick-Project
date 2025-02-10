using MessagePipe;
using VContainer;

public class RootContainer : CommonScope
{
    protected override void Configure(IContainerBuilder builder)
    {
        base.Configure(builder);
        var options = builder.RegisterMessagePipe();
        builder.RegisterBuildCallback(c => GlobalMessagePipe.SetProvider(c.AsServiceProvider()));
    }
}
