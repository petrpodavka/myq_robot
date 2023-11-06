namespace Myq.CodingTest.Commands;

/// <summary>
/// Maps command types to commands.
/// </summary>
internal interface ICommandProvider
{
    /// <summary>
    /// Provides command implementation for given command type.
    /// </summary>
    /// <param name="commandType">Command type the command should implement.</param>
    /// <returns>Instance of command implementing give command type.</returns>
    ICommand RetrieveCommand(CommandType commandType);
}