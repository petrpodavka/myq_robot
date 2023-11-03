using Microsoft.Extensions.DependencyInjection;
using Myq.CodingTest.Robot.Inputs;

namespace Myq.CodingTest.Robot;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection ConfigureRobot(this IServiceCollection services)
    {
        return services
            .AddSingleton<RobotService>()
            .AddTransient<IInputService, InputService>()
            .AddTransient<IInputReader, InputReader>()
            .AddTransient<IInputValidator, InputValidator>();
    }
}