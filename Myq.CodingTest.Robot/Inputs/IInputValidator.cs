namespace Myq.CodingTest.Robot.Inputs;

/// <summary>
/// Provides capabilities to validate program input.
/// </summary>
public interface IInputValidator
{
    /// <summary>
    /// Validates program input.
    /// </summary>
    /// <param name="input">Input to be validated.</param>
    void Validate(Input input);
}