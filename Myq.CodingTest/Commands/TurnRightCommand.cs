using Microsoft.Extensions.Logging;
using Myq.CodingTest.Maps;
using Myq.CodingTest.Robots;

namespace Myq.CodingTest.Commands;

internal class TurnRightCommand : ICommand
{
    public CommandType ImplementedCommandType => CommandType.TR;
    public int BatteryConsumption => 1;

    private readonly ILogger<TurnRightCommand> _logger;

    public TurnRightCommand(ILogger<TurnRightCommand> logger)
    {
        _logger = logger;
    }

    public (RobotState robotState, bool obstacleHit) PerformInner(Map map, RobotState robotState)
    {
        CommandLog.Performing(_logger, ImplementedCommandType);

        return (robotState with { Direction = TurnRight(robotState.Direction) }, false);
    }

    private static Direction TurnRight(Direction direction)
    {
        return direction switch
        {
            Direction.N => Direction.E,
            Direction.S => Direction.W,
            Direction.W => Direction.N,
            Direction.E => Direction.S,
            _ => throw new ArgumentOutOfRangeException(nameof(direction), direction, null)
        };
    }
}