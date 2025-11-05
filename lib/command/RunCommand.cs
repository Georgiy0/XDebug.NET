namespace xdebugnet.lib.command;

public class RunCommand : CommandBase
{
    public override System.String Name => "run";

    protected override System.String SerializeArguments() => System.String.Empty;
}