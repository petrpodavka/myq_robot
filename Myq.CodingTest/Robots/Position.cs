namespace Myq.CodingTest.Robots;

/// <summary>
/// Represents a position of the robot on the map.
/// </summary>
/// <param name="X">X coordinate on the map.</param>
/// <param name="Y">Y coordinate on the map.</param>
/// <param name="Facing">Direction in which the robot is facing.</param>
internal readonly record struct Position(int X, int Y, Direction Facing)
{
    public Coordinate GetCoordinate()
    {
        return new Coordinate(X, Y);
    }

    public override string ToString()
    {
        return $"[{X},{Y}]{Facing:G}";
    }
}