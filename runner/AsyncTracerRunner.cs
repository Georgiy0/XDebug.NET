namespace xdebugnet.runner;

using xdebugnet.lib.server;

public static class AsyncTracerRunner
{
    private static Listener? _listener;
    public static async Task Run(System.String[] args)
    {
        if (args.Length != 1)
        {
            Console.WriteLine($"Usgae: <port>");
            return;
        }
        System.Int32 port;
        if (!System.Int32.TryParse(args[0], out port) || port < 1 || port > 65000)
        {
            Console.WriteLine($"Invalid port number");
            return;
        }
        var transactionServerFactory = new TracerTransactionServerFactory("__do_trace");
        _listener = new Listener(port, transactionServerFactory);

        Console.CancelKeyPress += delegate (System.Object? sender, ConsoleCancelEventArgs e)
        {
            if (_listener != null)
            {
                Console.WriteLine("Stopping server...");
                _listener.StopAsync().GetAwaiter().GetResult();
            }
        };

        await _listener.StartAsync();
        Console.WriteLine("Exiting...");
    }
}