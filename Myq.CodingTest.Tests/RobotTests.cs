using Microsoft.Extensions.Logging.Abstractions;
using Myq.CodingTest.BackOffStrategies;
using Myq.CodingTest.Commands;
using Myq.CodingTest.Exceptions;
using Myq.CodingTest.Maps;
using Myq.CodingTest.Robots;
using Xunit;

namespace Myq.CodingTest.Tests;

public class RobotTests
{
    [Fact]
    public void ProvidesOutputForZeroCommands()
    {
        // Given
        var robot = InstantiateRobot();

        var map = MapGenerator.Instantiate3X3Map();
        var initialState = new RobotState(99, new Coordinate(1, 1), Direction.E);

        // When
        var state = robot.Run(map, initialState, Array.Empty<CommandType>());

        // Then
        Assert.Equal(initialState, state);
        Assert.Collection(map.GetVisited(), c => Assert.Equal(initialState.Coordinate, c));
        Assert.Empty(map.GetCleaned());
    }

    [Fact]
    public void ThrowsWhenRobotStartsOutsideTheMap()
    {
        // Given
        var robot = InstantiateRobot();

        var map = MapGenerator.Instantiate3X3Map();
        var initialState = new RobotState(99, new Coordinate(10, 10), Direction.E);

        // When
        var exception = Record.Exception(() => robot.Run(map, initialState, Array.Empty<CommandType>()));

        // Then
        Assert.IsType<RobotException>(exception);
    }

    [Fact]
    public void ThrowsWhenRobotStartsInNotAccessibleCell()
    {
        // Given
        var robot = InstantiateRobot();

        var map = MapGenerator.Instantiate3X3Map(MapCellType.C);
        var initialState = new RobotState(99, new Coordinate(1, 1), Direction.E);

        // When
        var exception = Record.Exception(() => robot.Run(map, initialState, Array.Empty<CommandType>()));

        // Then
        Assert.IsType<RobotException>(exception);
    }

    [Fact]
    public void PerformsProvidedCommand()
    {
        // Given
        var stubCommand = new StubCommand { Result = (new RobotState(), obstacleHit: false) };
        var stubCommandProvider = new StubCommandProvider { Command = stubCommand };

        var robot = InstantiateRobot(stubCommandProvider);

        var map = MapGenerator.Instantiate3X3Map();
        var initialState = new RobotState(99, new Coordinate(1, 1), Direction.E);

        // When
        var state = robot.Run(map, initialState, new[] { CommandType.A });

        // Then
        Assert.Equal(stubCommand.Result.state, state);
    }

    [Fact]
    public void PerformsBackOff()
    {
        // Given
        var stubCommand = new StubCommand { Result = (new RobotState(1, new(), Direction.E), obstacleHit: true) };
        var stubCommandProvider = new StubCommandProvider { Command = stubCommand };

        var stubBackOffStrategy = new StubBackOffStrategy { Result = (new RobotState(2, new(), Direction.N), successful: true) };

        var robot = InstantiateRobot(stubCommandProvider, stubBackOffStrategy);

        var map = MapGenerator.Instantiate3X3Map();
        var initialState = new RobotState(99, new Coordinate(1, 1), Direction.E);

        // When
        var state = robot.Run(map, initialState, new[] { CommandType.A });

        // Then
        Assert.Equal(stubBackOffStrategy.Result.robotState, state);
    }

    private static IRobot InstantiateRobot(ICommandProvider? commandProvider = null, IBackOffStrategy? backOffStrategy = null)
    {
        return new Robot(
            NullLogger<Robot>.Instance,
            commandProvider ?? ImpotentCommandProvider,
            backOffStrategy ?? new BackOffStrategy(ImpotentCommandProvider, NullLogger<BackOffStrategy>.Instance));
    }

    private static ICommandProvider ImpotentCommandProvider => new CommandProvider(Array.Empty<ICommand>());

    private class StubCommand : ICommand
    {
        public CommandType ImplementedCommandType => CommandType.TL;

        public int BatteryConsumption => 0;

        public (RobotState state, bool obstacleHit) Result { get; init; }

        public (RobotState robotState, bool obstacleHit) PerformInner(Map map, RobotState robotState)
        {
            return Result;
        }
    }

    private class StubCommandProvider : ICommandProvider
    {
        public ICommand Command { get; init; } = null!;

        public ICommand RetrieveCommand(CommandType commandType)
        {
            return Command;
        }
    }

    private class StubBackOffStrategy : IBackOffStrategy
    {
        public (RobotState robotState, bool successful) Result { get; init; }

        public (RobotState robotState, bool successful) Perform(Map map, RobotState robotState)
        {
            return Result;
        }
    }
}