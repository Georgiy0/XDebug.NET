namespace xdebugnet.lib.command;

public class StackGetCommand : CommandBase
{
    public override System.String Name => "stack_get";
    protected override System.String SerializeArguments() => System.String.Empty;
}