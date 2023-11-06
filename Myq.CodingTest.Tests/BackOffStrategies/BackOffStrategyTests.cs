using Microsoft.Extensions.Logging.Abstractions;
using Myq.CodingTest.BackOffStrategies;
using Myq.CodingTest.Commands;
using Myq.CodingTest.Maps;
using Myq.CodingTest.Robots;
using Xunit;

namespace Myq.CodingTest.Tests.BackOffStrategies;

public class BackOffStrategyTests
{
    [Fact]
    public void PerformsOnlyFirstSequenceWhenPossible()
    {
        // Given
        var map = new Map(
            new[]
            {
                new[] { MapCellType.S, MapCellType.S, MapCellType.S },
                new[] { MapCellType.S, MapCellType.S, MapCellType.S },
                new[] { MapCellType.S, MapCellType.S, MapCellType.S }
            });
        var initialState = new RobotState(Battery: 99, Coordinate: new(1, 1), Direction.N);
        var backOffStrategy = InstantiateBackOffStrategy();

        // When
        var (resultingState, successful) = backOffStrategy.Perform(map, initialState);

        // Then
        Assert.True(successful);
        Assert.Equal(initialState.Battery - 4, resultingState.Battery);
        Assert.Equal(new Coordinate(2, 1), resultingState.Coordinate);
        Assert.Equal(Direction.N, resultingState.Direction);
        Assert.Empty(map.GetCleaned());
        Assert.Collection(map.GetVisited(), c => Assert.Equal(new Coordinate(2, 1), c));
    }

    [Fact]
    public void PerformsSecondSequenceWhenFirstEncountersObstacle()
    {
        var map = new Map(
            new[]
            {
                new[] { MapCellType.S, MapCellType.S, MapCellType.S },
                new[] { MapCellType.S, MapCellType.S, MapCellType.C },
                new[] { MapCellType.S, MapCellType.S, MapCellType.S }
            });
        var initialState = new RobotState(Battery: 99, Coordinate: new(1, 1), Direction.N);
        var backOffStrategy = InstantiateBackOffStrategy();

        // When
        var (resultingState, successful) = backOffStrategy.Perform(map, initialState);

        // Then
        Assert.True(successful);
        Assert.Equal(initialState.Battery - 7, resultingState.Battery);
        Assert.Equal(new Coordinate(1, 2), resultingState.Coordinate);
        Assert.Equal(Direction.W, resultingState.Direction);
        Assert.Empty(map.GetCleaned());
        Assert.Collection(map.GetVisited(), c => Assert.Equal(new Coordinate(1, 2), c));
    }

    [Fact]
    public void IndicatesUnsuccessfulBackOffWhenRobotHitsObstacleDuringLastSequence()
    {
        var map = new Map(
            new[]
            {
                new[] { MapCellType.C, MapCellType.C, MapCellType.C },
                new[] { MapCellType.C, MapCellType.S, MapCellType.C },
                new[] { MapCellType.C, MapCellType.C, MapCellType.C }
            });
        var initialState = new RobotState(Battery: 99, Coordinate: new(1, 1), Direction.N);
        var backOffStrategy = InstantiateBackOffStrategy();

        // When
        var (resultingState, successful) = backOffStrategy.Perform(map, initialState);

        // Then
        Assert.False(successful);
        Assert.Equal(initialState.Battery - 17, resultingState.Battery);
        Assert.Equal(new Coordinate(1, 1), resultingState.Coordinate);
        Assert.Equal(Direction.S, resultingState.Direction);
        Assert.Empty(map.GetCleaned());
        Assert.Empty(map.GetVisited());
    }

    private static BackOffStrategy InstantiateBackOffStrategy()
    {
        var commandProvider = new CommandProvider(
            new ICommand[]
            {
                new AdvanceCommand(NullLogger<AdvanceCommand>.Instance),
                new BackCommand(NullLogger<BackCommand>.Instance),
                new CleanCommand(NullLogger<CleanCommand>.Instance),
                new TurnLeftCommand(NullLogger<TurnLeftCommand>.Instance),
                new TurnRightCommand(NullLogger<TurnRightCommand>.Instance)
            });

        return new BackOffStrategy(commandProvider, NullLogger<BackOffStrategy>.Instance);
    }
}