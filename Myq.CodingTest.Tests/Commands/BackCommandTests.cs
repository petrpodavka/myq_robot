using Microsoft.Extensions.Logging.Abstractions;
using Myq.CodingTest.Commands;
using Myq.CodingTest.Robots;
using Xunit;

namespace Myq.CodingTest.Tests.Commands;

public class BackCommandTests
{
    [Fact]
    public void BackCommandImplementsBackCommandType()
    {
        // Given
        var backCommand = InstantiateBackCommand();

        // Then
        Assert.Equal(CommandType.B, backCommand.ImplementedCommandType);
        Assert.Equal(3, backCommand.BatteryConsumption);
    }

    [Theory]
    [InlineData(Direction.N, 2, 1)]
    [InlineData(Direction.S, 0, 1)]
    [InlineData(Direction.W, 1, 2)]
    [InlineData(Direction.E, 1, 0)]
    internal void BackingToAccessibleCellDecreasesBatteryAndChangesCoordinateAndAddsCoordinateToVisitedButDoesNotChangeDirection(
        Direction direction,
        int expectedCoordinateX,
        int expectedCoordinateY)
    {
        // Given
        var map = MapGenerator.Instantiate3X3Map();
        var initialState = new RobotState(Battery: 99, Coordinate: new(1, 1), direction);
        var command = InstantiateBackCommand();

        // When
        var (resultingState, backOff) = command.Perform(map, initialState);

        // Then
        var expectedCoordinate = new Coordinate(expectedCoordinateX, expectedCoordinateY);

        Assert.Equal(initialState.Battery - 3, resultingState.Battery);
        Assert.Equal(expectedCoordinate, resultingState.Coordinate);
        Assert.Equal(initialState.Direction, resultingState.Direction);
        Assert.Empty(map.GetCleaned());
        Assert.Collection(map.GetVisited(), c => Assert.Equal(expectedCoordinate, c));

        Assert.False(backOff);
    }

    [Fact]
    public void BackingToCoordinateThatCannotBeOccupiedConsumesBatteryAndIndicatesObstacleHit()
    {
        // Given
        var map = MapGenerator.InstantiateSingleCellMap();
        var initialState = new RobotState(Battery: 99, Coordinate: new(0, 0), Direction.N);
        var command = InstantiateBackCommand();

        // When
        var (resultingState, obstacleHit) = command.Perform(map, initialState);

        // Then
        Assert.Equal(initialState.Battery - 3, resultingState.Battery);
        Assert.Equal(initialState.Coordinate, resultingState.Coordinate);
        Assert.Equal(initialState.Direction, resultingState.Direction);
        Assert.Empty(map.GetCleaned());
        Assert.Empty(map.GetVisited());

        Assert.True(obstacleHit);
    }

    private static ICommand InstantiateBackCommand()
    {
        return new BackCommand(NullLogger<BackCommand>.Instance);
    }
}