using Myq.CodingTest.Exceptions;
using Myq.CodingTest.Maps;
using Myq.CodingTest.Robots;

namespace Myq.CodingTest.Commands;

/// <summary>
/// Represents the implementation of command type.
/// </summary>
internal interface ICommand
{
    /// <summary>
    /// Command type implemented by the instance.
    /// </summary>
    CommandType ImplementedCommandType { get; }

    /// <summary>
    /// Units of battery consumed by this command (without possible back off strategy).
    /// </summary>
    int BatteryConsumption { get; }

    /// <summary>
    /// Makes the robot perform the command on provided map.
    /// </summary>
    /// <param name="map">Map of the world.</param>
    /// <param name="robotState">State of the robot.</param>
    /// <returns>State of the robot resulting from performing the command and indication if obstacle was hit.</returns>
    public (RobotState robotState, bool obstacleHit) Perform(Map map, RobotState robotState)
    {
        robotState = ConsumeBattery(robotState);
        return PerformInner(map, robotState);
    }

    private RobotState ConsumeBattery(RobotState robotState)
    {
        if (robotState.Battery < BatteryConsumption)
        {
            throw new InsufficientBatteryException(ImplementedCommandType, robotState.Battery, BatteryConsumption);
        }

        return robotState with { Battery = robotState.Battery - BatteryConsumption };
    }

    protected (RobotState robotState, bool obstacleHit) PerformInner(Map map, RobotState robotState);
}