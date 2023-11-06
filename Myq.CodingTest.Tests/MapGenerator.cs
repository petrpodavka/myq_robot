using Myq.CodingTest.Maps;

namespace Myq.CodingTest.Tests;

public static class MapGenerator
{
    internal static Map InstantiateSingleCellMap() => new(new[] { new[] { MapCellType.S } });

    internal static Map Instantiate3X3Map(MapCellType cellType = MapCellType.S) =>
        new(
            new[]
            {
                new[] { cellType, cellType, cellType },
                new[] { cellType, cellType, cellType },
                new[] { cellType, cellType, cellType }
            });
}