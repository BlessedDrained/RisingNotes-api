using System.Diagnostics;
using Serilog;

namespace MainLib.Logging;

public class MethodLog : IDisposable
{
    private readonly string _methodName;
    private object[] _argList;

    public MethodLog(params object[] argList)
    {
        var method = new StackFrame(4).GetMethod();
        var type = method.DeclaringType;

        method.GetParameters();
        _methodName = $"{type}.{method.Name}";

        var loggableArgList = argList.Where(x =>
        {
            var type = x.GetType();
            if (type.IsAssignableTo(typeof(Stream)))
            {
                return false;
            }

            if (type.IsAssignableTo(typeof(byte[])))
            {
                return false;
            }

            return true;
        });
        
        Log.Information($"Start method {_methodName} with args: {{{string.Join(", ", loggableArgList)}}}");
    }

    public void ReturnsValue(params object[] argList)
    {
        _argList = argList;
    }

    /// <inheritdoc />
    public void Dispose()
    {
        if (_argList == null)
        {
            Log.Information($"End method {_methodName}");
            return;
        }

        Log.Information($"End method {_methodName} with args: {{{string.Join(", ", _argList)}}}");
    }
}