using Myq.CodingTest.Robots;

namespace Myq.CodingTest.Maps;

/// <summary>
/// Map of the world. Encapsulates the input and provides methods that use that input.
/// </summary>
internal class Map
{
    private readonly MapCellType[][] _map;
    private readonly List<Coordinate> _visited;
    private readonly List<Coordinate> _cleaned;

    public Map(MapCellType[][] map)
    {
        _map = map;
        _visited = new();
        _cleaned = new();
    }

    /// <summary>
    /// Get collection of visited coordinates on this map.
    /// </summary>
    /// <returns>Collection of visited coordinates on this map.</returns>
    public IReadOnlyCollection<Coordinate> GetVisited()
    {
        return _visited.ToArray();
    }

    /// <summary>
    /// Get collection of cleaned coordinates on this map.
    /// </summary>
    /// <returns>Collection of cleaned coordinates on this map.</returns>
    public IReadOnlyCollection<Coordinate> GetCleaned()
    {
        return _cleaned.ToArray();
    }

    /// <summary>
    /// Try to add the coordinate to the collection of visited coordinates.
    /// </summary>
    /// <param name="coordinate">Coordinate to be visited.</param>
    /// <returns><see langword="true"/> when visited, otherwise false.</returns>
    public bool TryVisit(Coordinate coordinate)
    {
        if (IsValid(coordinate) && CanVisit(coordinate))
        {
            _visited.Add(coordinate);
            return true;
        }
        else
        {
            return false;
        }
    }

    /// <summary>
    /// Try to add the coordinate to the collection of cleaned coordinates.
    /// </summary>
    /// <param name="coordinate">Coordinate to be visited.</param>
    /// <returns><see langword="true"/> when visited, otherwise false.</returns>
    public bool Clean(Coordinate coordinate)
    {
        if (IsValid(coordinate) && CanClean(coordinate))
        {
            _cleaned.Add(coordinate);
            return true;
        }
        else
        {
            return false;
        }
    }

    // X and Y are switched here because the map is transposed during loading.

    private bool IsValid(Coordinate coordinate) => coordinate.X >= 0 && coordinate.X < _map[0].Length && coordinate.Y >= 0 && coordinate.Y < _map.Length;

    private bool CanVisit(Coordinate coordinate) => _map[coordinate.Y][coordinate.X] is MapCellType.S;

    private bool CanClean(Coordinate coordinate) => _map[coordinate.Y][coordinate.X] is MapCellType.S;
}