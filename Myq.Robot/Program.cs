using System.CommandLine;
using System.CommandLine.Builder;
using System.CommandLine.Hosting;
using System.CommandLine.Invocation;
using System.CommandLine.Parsing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Myq.CodingTest;

var inputArg = new Argument<FileInfo>("source-json", "File to serve as an input to the program.");
var outputArg = new Argument<FileInfo>("target-json", "File into which output will be written.");
var rootCommand = new RootCommand("MyQ Unattended Coding Test 2.5");
rootCommand.AddArgument(inputArg);
rootCommand.AddArgument(outputArg);

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
    private readonly IRobotService _robotService;

    public FileInfo SourceJson { get; set; } = null!;
    public FileInfo TargetJson { get; set; } = null!;

    public RootCommandHandler(IRobotService robotService)
    {
        _robotService = robotService;
    }

    public int Invoke(InvocationContext context) => InvokeAsync(context).GetAwaiter().GetResult();

    public async Task<int> InvokeAsync(InvocationContext context)
    {
        await _robotService.ExecuteAsync(SourceJson, TargetJson, context.GetCancellationToken());

        // Unhandled exception means critical failure that leads to output not being written, anything else is success.
        return 0;
    }
}
