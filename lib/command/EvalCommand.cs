using System.Text;

namespace xdebugnet.lib.command;

public class EvalCommand : CommandBase
{
    private System.String _toEval;
    public EvalCommand(System.String toEval)
    {
        _toEval = toEval;
    }

    public override string Name => "eval";

    protected override string SerializeArguments()
    {
        return $"-- {Convert.ToBase64String(Encoding.ASCII.GetBytes(_toEval))}";
    }
}