namespace Myq.CodingTest.Outputs;

internal interface IOutputService
{
    Task WriteOutputAsync(string path, Output output, CancellationToken cancellationToken);
}