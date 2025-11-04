namespace xdebugnet.lib.server;

using System.Net;
using System.Net.Sockets;
using System.Text;
using xdebugnet.lib.command;
using xdebugnet.lib.message;

public class Listener
{
    private TcpListener _tcpListener;
    private ITransactionServerFactory _transactionServerFactory;
    private System.Int32 _port;
    private System.Boolean _running = true;

    public Listener(System.Int32 port, ITransactionServerFactory transactionServerFactory)
    {
        _port = port;
        _transactionServerFactory = transactionServerFactory;
    }

    public async Task StartAsync()
    {
        _tcpListener = new TcpListener(IPAddress.Any, _port);
        _tcpListener.Start();

        while (_running)
        {
            var client = await _tcpListener.AcceptTcpClientAsync();
            var transactionServer = _transactionServerFactory.Create();
            _ = HandleClientAsync(client, transactionServer);
        }
    }
    public async Task StopAsync()
    {
        _tcpListener.Stop();
        _running = false;
    }

    private System.Int32? ReadSize(Stream clientStream)
    {
        System.Byte[] sizeData = new System.Byte[64];
        System.Int32 index = 0;
        System.Int32 readByte = 0;
        while ((readByte = clientStream.ReadByte()) != -1 && index < sizeData.Length)
        {
            sizeData[index++] = (System.Byte)readByte;
            if (readByte == 0)
            {
                break;
            }
        }
        if (readByte == -1 || index == sizeData.Length)
        {
            return null;
        }

        System.String sizeStr = Encoding.ASCII.GetString(sizeData);
        System.Int32 result = 0;
        if (System.Int32.TryParse(sizeStr, out result))
        {
            return result;
        }
        else
        {
            return null;
        }
    }

    private async Task SendCommands(IList<CommandBase> commands, Stream stream)
    {
        foreach (var command in commands)
        {
            var serialized = command.Serialize();
            var commandBuffer = new System.Byte[Encoding.ASCII.GetByteCount(serialized) + 1];
            Encoding.ASCII.GetBytes(serialized, commandBuffer);
            commandBuffer[commandBuffer.Length - 1] = 0x00;
            await stream.WriteAsync(commandBuffer, 0, commandBuffer.Length);
        }
    }

    private async Task HandleClientAsync(TcpClient client, TransactionServer transactionServer)
    {
        try
        {
            var stream = client.GetStream();
            var size = ReadSize(stream);

            if (size == null)
            {
                throw new NotImplementedException("couldn't parse size");
            }

            System.Byte[] data = new System.Byte[size.Value + 1]; // additional byte for 0 char
            var read = await stream.ReadAsync(data, 0, data.Length);

            if (read != data.Length)
            {
                throw new NotImplementedException();
            }

            var message = Encoding.ASCII.GetString(data, 0, size.Value);

            InitMessage initMessage = MessageSerializeHelper.Deserialize<InitMessage>(message);

            var commands = transactionServer.HandleInit(initMessage);

            // send init commands
            await SendCommands(commands, stream);

            // read responses
            while (true)
            {
                size = ReadSize(stream);
                if (size == null)
                {
                    throw new NotImplementedException("couldn't parse size");
                }

                data = new System.Byte[size.Value + 1]; // additional byte for 0 char
                read = await stream.ReadAsync(data, 0, data.Length);

                message = Encoding.ASCII.GetString(data, 0, size.Value);

                ResponseMessage responseMessage = MessageSerializeHelper.Deserialize<ResponseMessage>(message);
                responseMessage.SetRawResponse(message);
                var additionalCommands = transactionServer.HandleResponse(responseMessage);

                if (additionalCommands.Count() > 0)
                {
                    await SendCommands(additionalCommands, stream);
                }
                else
                {
                    if (!transactionServer.HasHandlers)
                    {
                        transactionServer.OnExit();
                        return;
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Exception:\n{ex}");
        }
        finally
        {
            client.Close();
        }
    }
}