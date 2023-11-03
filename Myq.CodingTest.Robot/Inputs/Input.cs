namespace Myq.CodingTest.Robot.Inputs;

/// <summary>
/// Input of the program defining the environment and what to do.
/// </summary>
public class Input
{
    /// <summary>
    /// Two dimensional array representing the world. Null item represents an obstacle. The (0,0) coordinate is N/W (up/right).
    /// </summary>
    public MapCellType?[][] Map { get; init; } = null!;

    /// <summary>
    /// Starting position of the robot.
    /// </summary>
    public Position Start { get; init; } = null!;

    /// <summary>
    /// Commands to be performed by the robot.
    /// </summary>
    public Command[] Commands { get; init; } = null!;

    /// <summary>
    /// Starting number of battery units.
    /// </summary>
    public int Battery { get; init; }
}

/// <summary>
/// Type of cell of the map.
/// </summary>
public enum MapCellType
{
    /// <summary>
    /// Cell that can be occupied and cleaned.
    /// </summary>
    S,

    /// <summary>
    /// Cell that can’t be occupied or cleaned.
    /// </summary>
    C,

    /// <summary>
    /// Cell that empty or outside the boundaries.
    /// </summary>
    Null,
}

/// <summary>
/// Represents a position of the robot on the map.
/// </summary>
public class Position
{
    /// <summary>
    /// X coordinate on the map.
    /// </summary>
    public int X { get; init; }

    /// <summary>
    /// Y coordinate on the map.
    /// </summary>
    public int Y { get; init; }

    /// <summary>
    /// Direction in which the robot is facing.
    /// </summary>
    public Direction Facing { get; init; }
}

/// <summary>
/// Direction on the map.
/// </summary>
public enum Direction
{
    /// <summary>
    /// North (up).
    /// </summary>
    N,

    /// <summary>
    /// South (down).
    /// </summary>
    S,

    /// <summary>
    /// West (right).
    /// </summary>
    W,

    /// <summary>
    /// East (left).
    /// </summary>
    E
}

/// <summary>
/// Command that the robot can do.
/// </summary>
public enum Command
{
    /// <summary>
    /// Turn left 90 degrees.
    /// </summary>
    TL,

    /// <summary>
    /// Turn right 90 degrees.
    /// </summary>
    TR,

    /// <summary>
    /// Advance to the next cell.
    /// </summary>
    A,

    /// <summary>
    /// Move back one cell.
    /// </summary>
    B,

    /// <summary>
    /// Clean current cell.
    /// </summary>
    C
}