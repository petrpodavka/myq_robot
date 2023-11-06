using System.Text.Json;
using System.Text.Json.Serialization;

namespace Myq.CodingTest.Outputs;

internal class OutputService : IOutputService
{
    public async Task WriteOutputAsync(string path, Output output, CancellationToken cancellationToken)
    {
        await using var createStream = File.Create(path);
        var jsonSerializerOptions = new JsonSerializerOptions
        {
            Converters = { new JsonStringEnumConverter() },
            PropertyNameCaseInsensitive = true,
            WriteIndented = true
        };

        await JsonSerializer.SerializeAsync(createStream, output, jsonSerializerOptions, cancellationToken);
    }
}