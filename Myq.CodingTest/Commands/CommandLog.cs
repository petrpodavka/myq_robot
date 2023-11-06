using Microsoft.Extensions.Logging;

namespace Myq.CodingTest.Commands;

internal static partial class CommandLog
{
    [LoggerMessage(EventId = 0, Level = LogLevel.Information, Message = "Performing command '{CommandType}'.")]
    public static partial void Performing(ILogger logger, CommandType commandType);
}