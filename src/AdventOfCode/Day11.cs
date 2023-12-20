
namespace AdventOfCode;

class Day11 : IPuzzleDay
{
    public int Day => 11;

    public string PartOne(IEnumerable<string> inputLines)
    {
        var galaxies = ParseGalaxies(inputLines);
        var grownGalaxies = GrowGalaxies(galaxies);

        var galaxyPairs = GetGalaxyPairs(grownGalaxies);
        var galaxyDistances = galaxyPairs
            .Select(pair => DistanceBetweenGalaxies(pair.Item1, pair.Item2));

        var answer = galaxyDistances.Sum();
        return answer.ToString();
    }

    public string PartTwo(IEnumerable<string> inputLines)
    {
        var galaxies = ParseGalaxies(inputLines);
        var grownGalaxies = GrowGalaxies(galaxies, 1000000);

        var galaxyPairs = GetGalaxyPairs(grownGalaxies);
        var galaxyDistances = galaxyPairs
            .Select(pair => DistanceBetweenGalaxies(pair.Item1, pair.Item2));

        var answer = galaxyDistances.Sum();
        return answer.ToString();
    }

    private IEnumerable<Galaxy> ParseGalaxies(IEnumerable<string> inputLines)
    {
        return inputLines.SelectMany((row, rowIndex) => row
            .Select((value, colIndex) => (value, rowIndex, colIndex))
            .Where(cell => cell.value == '#')
            .Select(cell => new Galaxy(Row: cell.rowIndex, Column: cell.colIndex))
        );
    }

    private IEnumerable<Galaxy> GrowRows(IEnumerable<Galaxy> galaxies, int growthFactor)
    {
        var sortedByRow = galaxies
            .OrderBy(galaxy => galaxy.Row);

        int lastRow = -1;
        int rowOffset = 0;
        foreach (var galaxy in sortedByRow)
        {
            if (galaxy.Row > lastRow + 1)
            {
                rowOffset += (galaxy.Row - lastRow - 1) * (growthFactor - 1);
            }
            if (galaxy.Row != lastRow)
            {
                lastRow = galaxy.Row;
            }
            yield return galaxy with { Row = galaxy.Row + rowOffset };
        }
    }

    private IEnumerable<Galaxy> GrowCols(IEnumerable<Galaxy> galaxies, int growthFactor)
    {
        var sortedByCol = galaxies
            .OrderBy(galaxy => galaxy.Column);

        int lastCol = -1;
        int colOffset = 0;
        foreach (var galaxy in sortedByCol)
        {
            if (galaxy.Column > lastCol + 1)
            {
                colOffset += (galaxy.Column - lastCol - 1) * (growthFactor - 1);
            }
            if (galaxy.Column != lastCol)
            {
                lastCol = galaxy.Column;
            }
            yield return galaxy with { Column = galaxy.Column + colOffset };
        }
    }

    private IEnumerable<Galaxy> GrowGalaxies(IEnumerable<Galaxy> galaxies, int growthFactor = 2)
    {
        var grownRows = GrowRows(galaxies, growthFactor);
        var grownCols = GrowCols(grownRows, growthFactor);
        return grownCols;
    }

    private IEnumerable<(Galaxy, Galaxy)> GetGalaxyPairs(IEnumerable<Galaxy> galaxies)
    {
        var galaxiesArray = galaxies.ToArray();
        for (int firstIndex = 0; firstIndex < galaxiesArray.Length - 1; firstIndex++)
        {
            for (int secondIndex = firstIndex + 1; secondIndex < galaxiesArray.Length; secondIndex++)
            {
                yield return (galaxiesArray[firstIndex], galaxiesArray[secondIndex]);
            }
        }
    }

    private long DistanceBetweenGalaxies(Galaxy first, Galaxy second)
    {
        return Math.Abs(first.Row - second.Row) + Math.Abs(first.Column - second.Column);
    }

    private record Galaxy(int Row, int Column);
}