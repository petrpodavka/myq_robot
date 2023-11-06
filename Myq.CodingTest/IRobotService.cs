namespace Myq.CodingTest;

public interface IRobotService
{
    /// <summary>
    /// Execute instructions from input file and write the result to output file.
    /// </summary>
    /// <param name="inputFile">File with instructions.</param>
    /// <param name="outputFile">File for output.</param>
    /// <param name="cancellationToken">Observed during the operation.</param>
    Task ExecuteAsync(FileInfo inputFile, FileInfo outputFile, CancellationToken cancellationToken);
}