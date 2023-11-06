namespace Myq.CodingTest.Robots;

/// <summary>
/// Represents the state of the robot.
/// </summary>
/// <param name="Battery">Remaining battery units.</param>
/// <param name="Coordinate">Coordinate of the robot on the map.</param>
/// <param name="Direction">Direction in which the robot is facing.</param>
internal readonly record struct RobotState(int Battery, Coordinate Coordinate, Direction Direction);