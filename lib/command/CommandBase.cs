namespace xdebugnet.lib.command;

using System.Text;

public abstract class CommandBase
{
    public abstract System.String Name { get; }
    private System.String? _transactionId;
    public System.String TransactionId => _transactionId ?? throw new NotImplementedException("_transactionId was not set");
    public void SetTransactionId(System.String transactionId) => _transactionId = transactionId;
    protected abstract System.String SerializeArguments();
    public System.String Serialize()
    {
        var result = new StringBuilder();
        result.Append($"{Name} -i {TransactionId}");
        var args = SerializeArguments();
        if (args.Length > 0)
        {
            result.Append($" {args}");
        }
        return result.ToString();
    }
}