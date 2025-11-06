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
    /// <summary>
    /// Creates EvalCommand for reading PHP file for given fileUri
    /// </summary>
    /// <param name="fileUri">file URI that includes line position</param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public static EvalCommand GetPhpFileLineEval(System.String fileUri)
    {
        var lineNumberSeparatorIndex = fileUri.LastIndexOf(':');
        if (lineNumberSeparatorIndex == -1)
        {
            throw new NotImplementedException($"file uri without lineno {fileUri}");
        }
        System.Int32 lineNumber;
        if (!System.Int32.TryParse(fileUri.Substring(lineNumberSeparatorIndex + 1), out lineNumber))
        {
            throw new NotImplementedException($"couldn't parse lineno from file uri {fileUri}");
        }
        return GetPhpFileLineEval(fileUri.Substring(0, lineNumberSeparatorIndex), lineNumber);
    }
    /// <summary>
    /// Creates EvalCommand for reading PHP file for given fileUri and lineno
    /// </summary>
    /// <param name="fileUri">file URI without lineno</param>
    /// <param name="lineNumber">lineno in file</param>
    /// <returns></returns>
    public static EvalCommand GetPhpFileLineEval(System.String fileUri, System.Int32 lineNumber)
    {
        // uses PHP snippet "explode(PHP_EOL, file_get_contents('file_uri'))[lineno - 1]" to read PHP code line
        return new EvalCommand($"explode(PHP_EOL, file_get_contents('{fileUri}'))[{lineNumber - 1}];");
    }
}