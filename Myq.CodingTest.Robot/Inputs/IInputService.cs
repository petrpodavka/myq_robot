namespace Myq.CodingTest.Robot.Inputs;

public interface IInputService
{
    Task<Input> RetrieveInputAsync(string path, CancellationToken cancellationToken);
}