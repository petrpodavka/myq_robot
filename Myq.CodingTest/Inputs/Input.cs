using Myq.CodingTest.Commands;
using Myq.CodingTest.Maps;
using Myq.CodingTest.Robots;

namespace Myq.CodingTest.Inputs;

/// <summary>
/// Input of the program defining the environment and what to do.
/// </summary>
internal class Input
{
    /// <summary>
    /// Two dimensional array representing the world. Null item represents an obstacle. The (0,0) coordinate is N/W (up/right).
    /// </summary>
    public MapCellType[][] Map { get; init; } = null!;

    /// <summary>
    /// Starting position of the robot.
    /// </summary>
    public Position Start { get; init; }

    /// <summary>
    /// Commands to be performed by the robot.
    /// </summary>
    public CommandType[] Commands { get; init; } = null!;

    /// <summary>
    /// Starting number of battery units.
    /// </summary>
    public int Battery { get; init; }
}