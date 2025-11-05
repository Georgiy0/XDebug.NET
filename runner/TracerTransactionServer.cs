namespace xdebugnet.runner;

using System.Text;
using xdebugnet.lib.command;
using xdebugnet.lib.message;
using xdebugnet.lib.server;

internal class TracerTransactionServerFactory : ITransactionServerFactory
{
    private System.String _correlator;
    public TracerTransactionServerFactory(System.String correlator)
    {
        _correlator = correlator;
    }
    public TransactionServer Create() => new TracerTransactionServer(_correlator);
}

internal class TracerTransactionServer : TransactionServer
{
    private System.String _correlator;
    private Queue<TraceMessage> _trace = new Queue<TraceMessage>();
    private System.Int32 _stepCnt = 0;
    public TracerTransactionServer(System.String correlator)
    {
        _correlator = correlator;
    }
    public override IList<CommandBase> HandleInit(InitMessage initMessage)
    {
        var evalCorrelator = new EvalCommand("$_REQUEST['__correlator'];"); // NOTE: escape correlator value
        AddTransaction(evalCorrelator, HandleEvalCorrelator); // send eval command to collect additional correlactor GET/POST parameter

        return new List<CommandBase>() { evalCorrelator };
    }


    public IList<CommandBase> HandleEvalCorrelator(ResponseMessage responseMessage)
    {
        var evalResponse = responseMessage.Reserialize<EvalResponseMessage>();
        if (_correlator != evalResponse.Property.GetData())
        {
            return EmptyCommandsList;
        }
        else
        {
            var stepInto = new StepIntoCommand();
            AddTransaction(stepInto, TraceHandler);
            return [stepInto];
        }
    }

    public IList<CommandBase> TraceHandler(ResponseMessage responseMessage)
    {
        var stepResponse = responseMessage.Reserialize<StepResponseMessage>();
        if (stepResponse.Status == "stopping")
        {
            var stop = new StopCommand();
            AddTransaction(stop, _ => EmptyCommandsList);
            return [stop];
        }
        else
        {
            _stepCnt++;
            Console.WriteLine($"[{_stepCnt}]: {stepResponse.Message.FileUri}:{stepResponse.Message.LineNumber}");
            _trace.Enqueue(stepResponse.Message);
            var stepInto = new StepIntoCommand();
            AddTransaction(stepInto, TraceHandler);
            return [stepInto];
        }
    }

    public override void OnExit()
    {
        Console.WriteLine("[*] End of trace");
        Console.WriteLine("Saving trace to trace.log");
        var traceBuilder = new StringBuilder();
        TraceMessage? prev = null;
        System.Int32 cnt = 0;
        while (_trace.Count() > 0)
        {
            var current = _trace.Dequeue();
            if (prev == null || prev.FileUri != current.FileUri || prev.LineNumber != current.LineNumber)
            {
                cnt++;
                traceBuilder.Append($"{cnt}: {current.FileUri}:{current.LineNumber}\n");
                prev = current;
            }
        }
        File.WriteAllText("trace.log", traceBuilder.ToString());
        Console.WriteLine("Saved trace.");
    }
}