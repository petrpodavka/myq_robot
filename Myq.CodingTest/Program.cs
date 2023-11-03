using System.CommandLine;
using System.CommandLine.Builder;
using System.CommandLine.Hosting;
using System.CommandLine.Invocation;
using System.CommandLine.Parsing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Myq.CodingTest.Robot;

var fileArgument = new Argument<FileInfo>("source-json", "File to server as an input to the program.");
var rootCommand = new RootCommand("MyQ Unattended Coding Test 2.5");
rootCommand.AddArgument(fileArgument);

var parser = new CommandLineBuilder(rootCommand)
    .UseHelp()
    .UseHost(host => host
        .ConfigureServices((_, services) => services
            .AddLogging(builder => builder.AddConsole())
            .ConfigureRobot())
        .UseCommandHandler<RootCommand, RootCommandHandler>())
    .Build();

return await parser.InvokeAsync(args);

public class RootCommandHandler : ICommandHandler
{
    private readonly RobotService _robotService;

    public FileInfo SourceJson { get; set; } = null!;

    public RootCommandHandler(RobotService robotService)
    {
        _robotService = robotService;
    }

    public int Invoke(InvocationContext context) => InvokeAsync(context).GetAwaiter().GetResult();

    public async Task<int> InvokeAsync(InvocationContext context)
    {
        try
        {
            await _robotService.ExecuteAsync(SourceJson, context.GetCancellationToken());
            return 0;
        }
        catch (RobotException)
        {
            // This type of exception has been handled already
            return -1;
        }
    }
}
