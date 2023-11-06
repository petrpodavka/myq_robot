using Microsoft.Extensions.Logging;
using Myq.CodingTest.Maps;
using Myq.CodingTest.Robots;

namespace Myq.CodingTest.Commands;

internal class BackCommand : ICommand
{
    public CommandType ImplementedCommandType => CommandType.B;
    public int BatteryConsumption => 3;

    private readonly ILogger<BackCommand> _logger;

    public BackCommand(ILogger<BackCommand> logger)
    {
        _logger = logger;
    }

    public (RobotState robotState, bool obstacleHit) PerformInner(Map map, RobotState robotState)
    {
        CommandLog.Performing(_logger, ImplementedCommandType);

        var newCoordinate = CalculateNewCoordinate(robotState);

        if (map.TryVisit(newCoordinate))
        {
            return (robotState with { Coordinate = newCoordinate }, false);
        }
        else
        {
            return (robotState, true);
        }
    }

    private static Coordinate CalculateNewCoordinate(RobotState robotState)
    {
        return robotState.Direction switch
        {
            Direction.N => robotState.Coordinate with { X = robotState.Coordinate.X + 1 },
            Direction.S => robotState.Coordinate with { X = robotState.Coordinate.X - 1 },
            Direction.W => robotState.Coordinate with { Y = robotState.Coordinate.Y + 1 },
            Direction.E => robotState.Coordinate with { Y = robotState.Coordinate.Y - 1 },
            _ => throw new ArgumentOutOfRangeException(nameof(robotState.Direction), robotState.Direction, $"Cannot advance in direction {robotState.Direction}.")
        };
    }
}