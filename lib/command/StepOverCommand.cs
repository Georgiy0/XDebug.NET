namespace xdebugnet.lib.command;

public class StepOverCommand : CommandBase
{
    public override System.String Name => "step_over";

    protected override System.String SerializeArguments() => System.String.Empty;
}