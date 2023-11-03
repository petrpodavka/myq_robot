using Microsoft.Extensions.Logging;
using Myq.CodingTest.Robot.Inputs;

namespace Myq.CodingTest.Robot;

public class RobotService
{
    private readonly ILogger<RobotService> _logger;
    private readonly IInputService _inputService;

    public RobotService(ILogger<RobotService> logger, IInputService inputService)
    {
        _logger = logger;
        _inputService = inputService;
    }

    public async Task ExecuteAsync(FileInfo inputFile, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Robot service executing.");

        var input = await _inputService.RetrieveInputAsync(inputFile.FullName, cancellationToken);
    }
}