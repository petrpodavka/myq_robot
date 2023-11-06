using Microsoft.Extensions.DependencyInjection;
using Myq.CodingTest.BackOffStrategies;
using Myq.CodingTest.Commands;
using Myq.CodingTest.Inputs;
using Myq.CodingTest.Outputs;
using Myq.CodingTest.Robots;

namespace Myq.CodingTest;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection ConfigureRobot(this IServiceCollection services)
    {
        return services
            .AddSingleton<IRobotService, RobotService>()
            .AddTransient<IInputService, InputService>()
            .AddTransient<IInputReader, InputReader>()
            .AddTransient<IInputValidator, InputValidator>()
            .AddTransient<IOutputService, OutputService>()
            .AddTransient<IRobot, Robot>()
            .AddTransient<ICommandProvider, CommandProvider>()
            .AddTransient<IBackOffStrategy, BackOffStrategy>()
            .AddTransient<ICommand, AdvanceCommand>()
            .AddTransient<ICommand, BackCommand>()
            .AddTransient<ICommand, CleanCommand>()
            .AddTransient<ICommand, TurnLeftCommand>()
            .AddTransient<ICommand, TurnRightCommand>();
    }
}