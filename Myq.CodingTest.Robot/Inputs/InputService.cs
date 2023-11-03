using Microsoft.Extensions.Logging;

namespace Myq.CodingTest.Robot.Inputs;

public class InputService : IInputService
{
    private readonly ILogger<InputService> _logger;
    private readonly IInputReader _inputReader;
    private readonly IInputValidator _inputValidator;

    public InputService(ILogger<InputService> logger, IInputReader inputReader, IInputValidator inputValidator)
    {
        _logger = logger;
        _inputReader = inputReader;
        _inputValidator = inputValidator;
    }

    public async Task<Input> RetrieveInputAsync(string path, CancellationToken cancellationToken)
    {
        var input = await _inputReader.ReadFromFileAsync(path, cancellationToken);
        _inputValidator.Validate(input);

        _logger.LogInformation("Valid input obtained.");

        return input;
    }
}