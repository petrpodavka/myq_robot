namespace Myq.CodingTest.Inputs;

internal interface IInputService
{
    Task<Input> RetrieveInputAsync(string path, CancellationToken cancellationToken);
}