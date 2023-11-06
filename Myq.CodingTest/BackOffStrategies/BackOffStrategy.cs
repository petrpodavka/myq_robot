using Microsoft.Extensions.Logging;
using Myq.CodingTest.Commands;
using Myq.CodingTest.Maps;
using Myq.CodingTest.Robots;

namespace Myq.CodingTest.BackOffStrategies;

internal class BackOffStrategy : IBackOffStrategy
{
    private readonly ILogger<BackOffStrategy> _logger;
    private readonly ICommandProvider _commandProvider;

    public BackOffStrategy(ICommandProvider commandProvider, ILogger<BackOffStrategy> logger)
    {
        _commandProvider = commandProvider;
        _logger = logger;
    }

    public (RobotState robotState, bool successful) Perform(Map map, RobotState robotState)
    {
        var sequences = new[]
        {
            new[] { CommandType.TR, CommandType.A, CommandType.TL },
            new[] { CommandType.TR, CommandType.A, CommandType.TR },
            new[] { CommandType.TR, CommandType.A, CommandType.TR },
            new[] { CommandType.TR, CommandType.B, CommandType.TR, CommandType.A },
            new[] { CommandType.TL, CommandType.TL, CommandType.A },
        };

        _logger.LogInformation("Starting back off strategy");

        foreach (var sequence in sequences)
        {
            _logger.LogInformation("Performing sequence [{Sequence}]", string.Join(", ", sequence));

            (robotState, var obstacleHit) = PerformSequence(sequence, map, robotState);

            if (!obstacleHit)
            {
                _logger.LogInformation("Back off strategy successful.");
                return (robotState, successful: true);
            }
        }

        return (robotState, successful: false);
    }

    private (RobotState robotState, bool obstacleHit) PerformSequence(IEnumerable<CommandType> sequence, Map map, RobotState robotState)
    {
        foreach (var commandType in sequence)
        {
            var command = _commandProvider.RetrieveCommand(commandType);
            (robotState, var obstacleHit) = command.Perform(map, robotState);

            if (obstacleHit)
            {
                return (robotState, obstacleHit);
            }
        }

        return (robotState, obstacleHit: false);
    }
}