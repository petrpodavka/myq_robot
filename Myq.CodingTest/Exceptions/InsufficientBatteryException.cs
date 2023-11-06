using Myq.CodingTest.Commands;

namespace Myq.CodingTest.Exceptions;

internal class InsufficientBatteryException : RobotException
{
    public InsufficientBatteryException(CommandType commandType, int currentBattery, int batteryConsumption)
        : base($"{nameof(CommandType)} '{commandType}' for '{batteryConsumption}' battery units cannot be performed, remaining battery units is only '{currentBattery}'.")
    {
    }
}