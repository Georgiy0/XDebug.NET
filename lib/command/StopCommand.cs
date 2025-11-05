namespace xdebugnet.lib.command;

public class StopCommand : CommandBase
{
    public override System.String Name => "stop";

    protected override System.String SerializeArguments() => System.String.Empty;
}