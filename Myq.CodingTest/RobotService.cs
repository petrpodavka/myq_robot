using Microsoft.Extensions.Logging;
using Myq.CodingTest.Exceptions;
using Myq.CodingTest.Inputs;
using Myq.CodingTest.Maps;
using Myq.CodingTest.Outputs;
using Myq.CodingTest.Robots;

namespace Myq.CodingTest;

internal class RobotService : IRobotService
{
    private readonly ILogger<RobotService> _logger;
    private readonly IInputService _inputService;
    private readonly IOutputService _outputService;
    private readonly IRobot _robot;

    public RobotService(ILogger<RobotService> logger, IInputService inputService, IRobot robot, IOutputService outputService)
    {
        _logger = logger;
        _inputService = inputService;
        _robot = robot;
        _outputService = outputService;
    }

    public async Task ExecuteAsync(FileInfo inputFile, FileInfo outputFile, CancellationToken cancellationToken)
    {
        var input = await ReadInputAsync(inputFile, cancellationToken);
        var (map, robotState) = RunTheRobot(input);
        await WriteOutputAsync(outputFile, map, robotState, cancellationToken);
    }

    private async Task<Input> ReadInputAsync(FileInfo inputFile, CancellationToken cancellationToken)
    {
        _logger.LogDebug("Reading input.");

        return await _inputService.RetrieveInputAsync(inputFile.FullName, cancellationToken);
    }

    private (Map, RobotState) RunTheRobot(Input input)
    {
        _logger.LogDebug("Running the robot.");

        var map = new Map(input.Map);
        var state = new RobotState(input.Battery, input.Start.GetCoordinate(), input.Start.Facing);

        try
        {
            state = _robot.Run(map, state, input.Commands);
        }
        catch (RobotException re)
        {
            _logger.LogWarning(re, "Robot exception occured.");
        }

        return (map, state);
    }

    private async Task WriteOutputAsync(FileInfo outputFile, Map map, RobotState robotState, CancellationToken cancellationToken)
    {
        _logger.LogDebug("Writing output.");

        var output = new Output
        {
            Battery = robotState.Battery,
            Final = new Position(robotState.Coordinate.X, robotState.Coordinate.Y, robotState.Direction),
            Visited = new HashSet<Coordinate>(map.GetVisited()),
            Cleaned = new HashSet<Coordinate>(map.GetCleaned())
        };

        await _outputService.WriteOutputAsync(outputFile.FullName, output, cancellationToken);
    }
}