using Microsoft.Extensions.Logging;
using Myq.CodingTest.Maps;
using Myq.CodingTest.Robots;

namespace Myq.CodingTest.Commands;

internal class TurnLeftCommand : ICommand
{
    public CommandType ImplementedCommandType => CommandType.TL;
    public int BatteryConsumption => 1;

    private readonly ILogger<TurnLeftCommand> _logger;

    public TurnLeftCommand(ILogger<TurnLeftCommand> logger)
    {
        _logger = logger;
    }

    public (RobotState robotState, bool obstacleHit) PerformInner(Map map, RobotState robotState)
    {
        CommandLog.Performing(_logger, ImplementedCommandType);

        return (robotState with { Direction = TurnLeft(robotState.Direction) }, false);
    }

    private static Direction TurnLeft(Direction direction)
    {
        return direction switch
        {
            Direction.N => Direction.W,
            Direction.S => Direction.E,
            Direction.W => Direction.S,
            Direction.E => Direction.N,
            _ => throw new ArgumentOutOfRangeException(nameof(direction), direction, null)
        };
    }
}