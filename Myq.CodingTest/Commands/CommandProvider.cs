namespace Myq.CodingTest.Commands;

internal class CommandProvider : ICommandProvider
{
    private readonly Dictionary<CommandType, ICommand> _commandsByType;

    public CommandProvider(IEnumerable<ICommand> commands)
    {
        _commandsByType = commands.ToDictionary(c => c.ImplementedCommandType, c => c);
    }

    public ICommand RetrieveCommand(CommandType commandType)
    {
        return _commandsByType[commandType];
    }
}