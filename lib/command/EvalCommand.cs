namespace xdebugnet.lib.command;

using System.Text;

public class EvalCommand : CommandBase
{
    private System.String _toEval;
    public EvalCommand(System.String toEval)
    {
        _toEval = toEval;
    }

    public override System.String Name => "eval";

    protected override System.String SerializeArguments()
    {
        return $"-- {Convert.ToBase64String(Encoding.ASCII.GetBytes(_toEval))}";
    }
}