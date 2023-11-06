namespace Myq.CodingTest.Inputs;

/// <summary>
/// Provides capabilities to read program input.
/// </summary>
internal interface IInputReader
{
    /// <summary>
    /// Reads the input from a file.
    /// </summary>
    /// <param name="path">Path to the file.</param>
    /// <param name="cancellationToken"><see cref="CancellationToken"/> observed during reading.</param>
    /// <returns>Program input.</returns>
    Task<Input> ReadFromFileAsync(string path, CancellationToken cancellationToken);
}