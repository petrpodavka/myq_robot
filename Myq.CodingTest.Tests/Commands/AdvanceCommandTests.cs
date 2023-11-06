using Microsoft.Extensions.Logging.Abstractions;
using Myq.CodingTest.Commands;
using Myq.CodingTest.Exceptions;
using Myq.CodingTest.Robots;
using Xunit;

namespace Myq.CodingTest.Tests.Commands;

public class AdvanceCommandTests
{
    [Fact]
    public void AdvanceCommandImplementsAdvanceCommandType()
    {
        // Given
        var advanceCommand = InstantiateAdvanceCommand();

        // Then
        Assert.Equal(CommandType.A, advanceCommand.ImplementedCommandType);
        Assert.Equal(2, advanceCommand.BatteryConsumption);
    }

    [Theory]
    [InlineData(Direction.N, 1, 0)]
    [InlineData(Direction.S, 1, 2)]
    [InlineData(Direction.W, 0, 1)]
    [InlineData(Direction.E, 2, 1)]
    internal void AdvancingToAccessibleCellDecreasesBatteryAndChangesCoordinateAndAddsCoordinateToVisitedButDoesNotChangeDirection(
        Direction direction,
        int expectedCoordinateX,
        int expectedCoordinateY)
    {
        // Given
        var map = MapGenerator.Instantiate3X3Map();
        var initialState = new RobotState(Battery: 99, Coordinate: new(1, 1), direction);
        var command = InstantiateAdvanceCommand();

        // When
        var (resultingState, obstacleHit) = command.Perform(map, initialState);

        // Then
        var expectedCoordinate = new Coordinate(expectedCoordinateX, expectedCoordinateY);

        Assert.Equal(initialState.Battery - 2, resultingState.Battery);
        Assert.Equal(expectedCoordinate, resultingState.Coordinate);
        Assert.Equal(initialState.Direction, resultingState.Direction);
        Assert.Empty(map.GetCleaned());
        Assert.Collection(map.GetVisited(), c => Assert.Equal(expectedCoordinate, c));

        Assert.False(obstacleHit);
    }

    [Fact]
    public void AdvancingToCoordinateThatCannotBeOccupiedConsumesBatteryAndIndicatesObstacleHit()
    {
        // Given
        var map = MapGenerator.InstantiateSingleCellMap();
        var initialState = new RobotState(Battery: 99, Coordinate: new(0, 0), Direction.N);
        var command = InstantiateAdvanceCommand();

        // When
        var (resultingState, obstacleHit) = command.Perform(map, initialState);

        // Then
        Assert.Equal(initialState.Battery - 2, resultingState.Battery);
        Assert.Equal(initialState.Coordinate, resultingState.Coordinate);
        Assert.Equal(initialState.Direction, resultingState.Direction);
        Assert.Empty(map.GetCleaned());
        Assert.Empty(map.GetVisited());

        Assert.True(obstacleHit);
    }

    [Fact]
    public void ThrowsInsufficientBatteryWhenAdvancingWithNotEnoughBattery()
    {
        // Given
        var insufficientBattery = 1;

        var map = MapGenerator.Instantiate3X3Map();
        var initialState = new RobotState(insufficientBattery, Coordinate: new(1, 1), Direction.N);
        var command = InstantiateAdvanceCommand();

        // When
        var exception = Record.Exception(() => command.Perform(map, initialState));

        // Then
        Assert.IsType<InsufficientBatteryException>(exception);
    }

    private static ICommand InstantiateAdvanceCommand()
    {
        return new AdvanceCommand(NullLogger<AdvanceCommand>.Instance);
    }
}