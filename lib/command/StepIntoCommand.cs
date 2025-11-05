namespace xdebugnet.lib.command;

public class StepIntoCommand : CommandBase
{
    public override System.String Name => "step_into";

    protected override System.String SerializeArguments() => System.String.Empty;
}