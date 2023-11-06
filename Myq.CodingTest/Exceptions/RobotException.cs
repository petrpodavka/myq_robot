namespace Myq.CodingTest.Exceptions;

/// <summary>
/// Custom exception not treated as unhandled in program entrypoint.
/// </summary>
public class RobotException : InvalidOperationException
{
    public RobotException(string? message) : base(message)
    {
    }

    public RobotException(Exception? innerException) : base(message: null, innerException)
    {
    }

    public RobotException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}