# XDebugNet server

Implements TCP-server that implements XDebug protocol and provides automation interface via:
- `TransactionServer` abstract class
- `ITransactionServerFactory` factory

## TransactionServer

`TransactionServer` provides interface for XDebug debug session automation:
1. HandleInit method implementation should creates initial commands to execute in debug session and register handlers for their responses
2. Response handlers may register additional commands with handlers to implement command chains
3. OnExit callback is executed when debug session terminates

Debug session terminates when no more command handlers are registered and all previous handlers have already been executed.