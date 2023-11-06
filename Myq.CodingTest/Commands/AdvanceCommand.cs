using Microsoft.Extensions.Logging;
using Myq.CodingTest.Maps;
using Myq.CodingTest.Robots;

namespace Myq.CodingTest.Commands;

internal class AdvanceCommand : ICommand
{
    public CommandType ImplementedCommandType => CommandType.A;
    public int BatteryConsumption => 2;

    private readonly ILogger<AdvanceCommand> _logger;

    public AdvanceCommand(ILogger<AdvanceCommand> logger)
    {
        _logger = logger;
    }

    public (RobotState robotState, bool obstacleHit) PerformInner(Map map, RobotState robotState)
    {
        CommandLog.Performing(_logger, ImplementedCommandType);

        var newCoordinate = CalculateNewCoordinate(robotState);

        return map.TryVisit(newCoordinate)
            ? (robotState with { Coordinate = newCoordinate }, false)
            : (robotState, true);
    }

    private static Coordinate CalculateNewCoordinate(RobotState robotState)
    {
        return robotState.Direction switch
        {
            Direction.N => robotState.Coordinate with { Y = robotState.Coordinate.Y - 1 },
            Direction.S => robotState.Coordinate with { Y = robotState.Coordinate.Y + 1 },
            Direction.W => robotState.Coordinate with { X = robotState.Coordinate.X - 1 },
            Direction.E => robotState.Coordinate with { X = robotState.Coordinate.X + 1 },
            _ => throw new ArgumentOutOfRangeException(nameof(robotState.Direction), robotState.Direction, null)
        };
    }
}