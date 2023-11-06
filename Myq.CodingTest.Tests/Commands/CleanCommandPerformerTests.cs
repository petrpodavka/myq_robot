using Microsoft.Extensions.Logging.Abstractions;
using Myq.CodingTest.Commands;
using Myq.CodingTest.Exceptions;
using Myq.CodingTest.Robots;
using Xunit;

namespace Myq.CodingTest.Tests.Commands;

public class CleanCommandPerformerTests
{
    [Fact]
    public void CleaningCommandImplementsCleanCommandType()
    {
        // Given
        var command = InstantiateCleanCommand();

        // Then
        Assert.Equal(CommandType.C, command.ImplementedCommandType);
        Assert.Equal(5, command.BatteryConsumption);
    }

    [Fact]
    public void CleaningDecreasesBatteryAndAddsCoordinateToCleanedButDoesNotChangeCoordinateAndDirection()
    {
        // Given
        var battery = 99;
        var coordinate = new Coordinate(0, 0);
        var direction = Direction.N;

        var map = MapGenerator.InstantiateSingleCellMap();
        var initialState = new RobotState(battery, coordinate, direction);
        var command = InstantiateCleanCommand();

        // When
        var (resultingState, _) = command.Perform(map, initialState);

        // Then
        Assert.Equal(battery - 5, resultingState.Battery);
        Assert.Equal(coordinate, resultingState.Coordinate);
        Assert.Equal(direction, resultingState.Direction);
        Assert.Collection(map.GetCleaned(), c => Assert.Equal(coordinate, c));
        Assert.Empty(map.GetVisited());
    }

    [Fact]
    public void ThrowsInsufficientBatteryWhenCleaningWithNotEnoughBattery()
    {
        // Given
        var insufficientBattery = 4;
        var coordinate = new Coordinate(0, 0);
        var direction = Direction.N;

        var map = MapGenerator.InstantiateSingleCellMap();
        var initialState = new RobotState(insufficientBattery, coordinate, direction);
        var command = InstantiateCleanCommand();

        // When
        var exception = Record.Exception(() => command.Perform(map, initialState));

        // Then
        Assert.IsType<InsufficientBatteryException>(exception);
    }

    private static ICommand InstantiateCleanCommand()
    {
        return new CleanCommand(NullLogger<CleanCommand>.Instance);
    }
}