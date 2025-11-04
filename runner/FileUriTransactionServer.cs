namespace xdebugnet.runner;

using xdebugnet.lib.command;
using xdebugnet.lib.message;
using xdebugnet.lib.server;

internal class FileUriTransactionServer : TransactionServer
{
    private DataCollector _dataCollector;
    private System.String _correlator;
    private System.String _fileUri = "";
    private System.String _uri = "";
    private System.String _correlatorValue = "";
    public FileUriTransactionServer(DataCollector dataCollector, System.String correlator)
    {
        _dataCollector = dataCollector;
        _correlator = correlator;
    }
    public override IList<CommandBase> HandleInit(InitMessage initMessage)
    {
        _fileUri = initMessage.FileUri; // save file uri from Init message

        var evalUri = new EvalCommand("$_SERVER['REQUEST_URI'];");
        var evalCorrelator = new EvalCommand("$_REQUEST['" + _correlator + "'];"); // NOTE: escape correlator value
        AddTransaction(evalUri, HandleEvalUri); // send eval command to collect HTTP URL
        AddTransaction(evalCorrelator, HandleEvalCorrelator); // send eval command to collect additional correlactor GET/POST parameter
        return new List<CommandBase>() { evalUri, evalCorrelator };
    }

    public IList<CommandBase> HandleEvalUri(ResponseMessage responseMessage)
    {
        var evalResponse = responseMessage.Reserialize<EvalResponseMessage>();
        _uri = evalResponse.Property.GetData();
        return EmptyCommandsList;
    }

    public IList<CommandBase> HandleEvalCorrelator(ResponseMessage responseMessage)
    {
        var evalResponse = responseMessage.Reserialize<EvalResponseMessage>();
        _correlatorValue = evalResponse.Property.GetData();
        return EmptyCommandsList;
    }

    public override void OnExit()
    {
        _dataCollector.AddData(new DataEntry
        {
            FileUri = _fileUri,
            Uri = _uri,
            Correlator = _correlatorValue
        });
    }
}