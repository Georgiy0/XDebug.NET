namespace xdebugnet.lib.server;

using xdebugnet.lib.command;
using xdebugnet.lib.message;

public abstract class TransactionServer
{
    protected static IList<CommandBase> EmptyCommandsList = new List<CommandBase>();
    private System.Int32 _transactionCounter = 0;
    private Dictionary<System.String, Func<ResponseMessage, IList<CommandBase>>> _handlers = new Dictionary<System.String, Func<ResponseMessage, IList<CommandBase>>>();
    public abstract IList<CommandBase> HandleInit(InitMessage initMessage);
    protected void AddTransaction(CommandBase command, Func<ResponseMessage, IList<CommandBase>> handler)
    {
        _transactionCounter++;
        var currentId = _transactionCounter.ToString();
        command.SetTransactionId(currentId);
        _handlers.Add(currentId, handler);
    }
    public IList<CommandBase> HandleResponse(ResponseMessage responseMessage)
    {
        List<CommandBase> commands = new List<CommandBase>();
        if (_handlers.ContainsKey(responseMessage.TransactionId))
        {
            commands.AddRange(_handlers[responseMessage.TransactionId](responseMessage));
            _handlers.Remove(responseMessage.TransactionId);
        }
        return commands;
    }
    public System.Boolean HasHandlers => _handlers.Count() > 0;

    public abstract void OnExit();
}