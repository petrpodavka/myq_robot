using Microsoft.Extensions.Logging;
using Myq.CodingTest.Exceptions;

namespace Myq.CodingTest.Inputs;

internal class InputValidator : IInputValidator
{
    private readonly ILogger<InputValidator> _logger;

    public InputValidator(ILogger<InputValidator> logger)
    {
        _logger = logger;
    }

    public void Validate(Input input)
    {
        try
        {
            if (input == null)
            {
                throw new RobotException("Input cannot be null");
            }

            // TODO more validations
        }
        catch (RobotException e)
        {
            _logger.LogCritical(e, "Input validation failed.");
            throw;
        }
    }
}