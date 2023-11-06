namespace Myq.CodingTest.Maps;

/// <summary>
/// Type of cell of the map.
/// </summary>
internal enum MapCellType
{
    /// <summary>
    /// Cell that can be occupied and cleaned.
    /// </summary>
    S,

    /// <summary>
    /// Cell that can’t be occupied or cleaned.
    /// </summary>
    C,

    /// <summary>
    /// Cell that empty or outside the boundaries.
    /// </summary>
    Null,
}