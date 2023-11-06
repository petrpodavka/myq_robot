using Myq.CodingTest.Maps;
using Myq.CodingTest.Robots;

namespace Myq.CodingTest.BackOffStrategies;

internal interface IBackOffStrategy
{
    /// <summary>
    /// Performs the back off strategy.
    /// </summary>
    /// <param name="map">Map of the world.</param>
    /// <param name="robotState">State of the robot.</param>
    /// <returns>State of the robot resulting from performing the back off strategy and whether the back off was successful.</returns>
    (RobotState robotState, bool successful) Perform(Map map, RobotState robotState);
}