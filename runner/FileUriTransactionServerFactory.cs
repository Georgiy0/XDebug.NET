namespace xdebugnet.runner;

using xdebugnet.lib.server;

internal class FileUriTransactionServerFactory : ITransactionServerFactory
{
    private DataCollector _dataCollector;
    private System.String _correlator;
    public FileUriTransactionServerFactory(DataCollector dataCollector, System.String correlator)
    {
        _dataCollector = dataCollector;
        _correlator = correlator;
    }
    public TransactionServer Create() => new FileUriTransactionServer(_dataCollector, _correlator);
}