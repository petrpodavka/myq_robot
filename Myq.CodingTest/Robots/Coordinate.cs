namespace Myq.CodingTest.Robots;

/// <summary>
/// Represents a 2D coordinate.
/// </summary>
/// <param name="X">Coordinate on the X axis.</param>
/// <param name="Y">Coordinate on the Y axis.</param>
internal readonly record struct Coordinate(int X, int Y);