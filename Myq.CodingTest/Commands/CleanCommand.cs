using Microsoft.Extensions.Logging;
using Myq.CodingTest.Maps;
using Myq.CodingTest.Robots;

namespace Myq.CodingTest.Commands;

internal class CleanCommand : ICommand
{
    public CommandType ImplementedCommandType => CommandType.C;
    public int BatteryConsumption => 5;

    private readonly ILogger<CleanCommand> _logger;

    public CleanCommand(ILogger<CleanCommand> logger)
    {
        _logger = logger;
    }

    public (RobotState robotState, bool obstacleHit) PerformInner(Map map, RobotState robotState)
    {
        CommandLog.Performing(_logger, ImplementedCommandType);

        map.Clean(robotState.Coordinate);
        return (robotState, false);
    }
}