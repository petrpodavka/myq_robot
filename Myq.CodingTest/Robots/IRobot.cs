using Myq.CodingTest.Commands;
using Myq.CodingTest.Maps;

namespace Myq.CodingTest.Robots;

/// <summary>
/// Represents a robot.
/// </summary>
internal interface IRobot
{
    /// <summary>
    /// Runs the robot.
    /// </summary>
    /// <returns>Achieved final state of the robot.</returns>
    RobotState Run(Map map, RobotState state, IReadOnlyCollection<CommandType> commandTypes);
}