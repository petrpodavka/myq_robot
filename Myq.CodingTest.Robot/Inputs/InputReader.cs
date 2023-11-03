using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.Extensions.Logging;

namespace Myq.CodingTest.Robot.Inputs;

public class InputReader : IInputReader
{
    private readonly ILogger<InputReader> _logger;

    public InputReader(ILogger<InputReader> logger)
    {
        _logger = logger;
    }

    public async Task<Input> ReadFromFileAsync(string path, CancellationToken cancellationToken)
    {
        try
        {
            await using var openStream = File.OpenRead(path);

            var jsonSerializerOptions = new JsonSerializerOptions
            {
                Converters = { new JsonStringEnumConverter() },
                PropertyNameCaseInsensitive = true
            };

            return await JsonSerializer.DeserializeAsync<Input>(openStream, jsonSerializerOptions, cancellationToken)
                ?? throw new RobotException("Deserialized input cannot be null.");
        }
        catch (RobotException)
        {
            throw;
        }
        catch (Exception e)
        {
            _logger.LogCritical(e, "Input could not be read or deserialized.");
            throw new RobotException(innerException: e);
        }
    }
}