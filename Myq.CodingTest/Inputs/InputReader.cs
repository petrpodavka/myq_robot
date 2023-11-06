using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.Extensions.Logging;
using Myq.CodingTest.Exceptions;

namespace Myq.CodingTest.Inputs;

internal class InputReader : IInputReader
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