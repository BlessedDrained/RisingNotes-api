using System.Diagnostics;
using System.Reflection;
using Serilog;

namespace MainLib.Logging;

public class MethodLog : IDisposable
{
    private readonly string _methodName;
    private readonly ParameterInfo[] _parameterList;
    private object[] _argList;

    public MethodLog(params object[] argList)
    {
        var method = new StackFrame(4).GetMethod();
        var type = method.DeclaringType;

        _parameterList = method.GetParameters();
        _methodName = $"{type}.{method.Name}";

        Log.Information($"Start method {_methodName} with args: {{{string.Join(", ", argList)}}}");
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