using Myq.CodingTest.Robots;

namespace Myq.CodingTest.Outputs;

internal class Output
{
    /// <summary>
    /// Coordinates of visited cells.
    /// </summary>
    public IReadOnlyCollection<Coordinate> Visited { get; init; } = null!;

    /// <summary>
    /// Coordinates of cleaned cells.
    /// </summary>
    public IReadOnlyCollection<Coordinate> Cleaned { get; init; } = null!;

    /// <summary>
    /// Final position of the robot.
    /// </summary>
    public Position Final { get; init; }

    /// <summary>
    /// Remaining battery of the robot.
    /// </summary>
    public int Battery { get; init; }
}
