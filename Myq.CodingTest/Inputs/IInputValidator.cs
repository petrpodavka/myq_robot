namespace Myq.CodingTest.Inputs;

/// <summary>
/// Provides capabilities to validate program input.
/// </summary>
internal interface IInputValidator
{
    /// <summary>
    /// Validates program input.
    /// </summary>
    /// <param name="input">Input to be validated.</param>
    void Validate(Input input);
}