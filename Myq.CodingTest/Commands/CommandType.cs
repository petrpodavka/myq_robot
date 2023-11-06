namespace Myq.CodingTest.Commands;

/// <summary>
/// Command types that the robot can do.
/// </summary>
internal enum CommandType
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