using Microsoft.Extensions.Logging;
using Myq.CodingTest.BackOffStrategies;
using Myq.CodingTest.Commands;
using Myq.CodingTest.Exceptions;
using Myq.CodingTest.Maps;

namespace Myq.CodingTest.Robots;

internal class Robot : IRobot
{
    private readonly ILogger<Robot> _logger;
    private readonly ICommandProvider _commandProvider;
    private readonly IBackOffStrategy _backOffStrategy;

    public Robot(ILogger<Robot> logger, ICommandProvider commandProvider, IBackOffStrategy backOffStrategy)
    {
        _logger = logger;
        _commandProvider = commandProvider;
        _backOffStrategy = backOffStrategy;
    }

    public RobotState Run(Map map, RobotState state, IReadOnlyCollection<CommandType> commandTypes)
    {
        if (!map.TryVisit(state.Coordinate))
        {
            throw new RobotException("Robot cannot start cell that cannot be occupied.");
        }

        foreach (var commandType in commandTypes)
        {
            try
            {
                var command = _commandProvider.RetrieveCommand(commandType);
                (state, var obstacleHit) = command.Perform(map, state);

                if (!obstacleHit)
                {
                    continue;
                }

                (state, var successful) = _backOffStrategy.Perform(map, state);

                if (successful)
                {
                    continue;
                }

                _logger.LogWarning("Robot stuck during back off.");
                return state;
            }
            catch (InsufficientBatteryException ibe)
            {
                _logger.LogWarning(ibe, "Robot is stopped due to insufficient battery.");
                return state;
            }
        }

        return state;
    }
}