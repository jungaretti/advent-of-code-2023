
namespace AdventOfCode;

class Day14 : IPuzzleDay
{
    public int Day => 14;

    public string PartOne(IEnumerable<string> inputLines)
    {
        Rock[][] rocks = ParseRocks(inputLines);

        Rock[][] rotatedRocks = RotateRocksClockwise(rocks);
        IEnumerable<IEnumerable<Rock>> tiltedRocks = rotatedRocks.Select(TiltRocks);
        IEnumerable<int> totalLoads = tiltedRocks.Select(GetTotalLoad);
        int answer = totalLoads.Sum();

        return answer.ToString();
    }

    public string PartTwo(IEnumerable<string> inputLines)
    {
        throw new NotImplementedException();
    }

    private int GetTotalLoad(IEnumerable<Rock> rocks)
    {
        int totalLoad = 0;
        int nextScore = rocks.Count();
        foreach (Rock rock in rocks)
        {
            if (rock.Type == RockType.Rounded)
            {
                totalLoad += nextScore;
            }
            nextScore--;
        }
        return totalLoad;
    }

    private IEnumerable<Rock> TiltRocks(IEnumerable<Rock> rocks)
    {
        if (rocks.Count() == 0)
        {
            return [];
        }

        var barrierIndex = rocks.ToList().FindIndex(rock => rock.Type == RockType.Cube);
        if (barrierIndex == -1)
        {
            barrierIndex = rocks.Count();
        }

        var tiledRocks = rocks
            .Take(barrierIndex + 1)
            .OrderBy(rock => rock.Type switch
            {
                RockType.Rounded => 1,
                null => 2,
                RockType.Cube => 3,
                _ => throw new Exception($"Unknown rock type: {rock}")
            });
        var otherRocks = TiltRocks(rocks.Skip(barrierIndex + 1));

        return [
            ..tiledRocks,
            ..otherRocks,
        ];
    }

    private Rock[][] RotateRocksClockwise(Rock[][] values)
    {
        var colCount = values[0].Length;
        var rowCount = values.Length;
        Rock[][] rotatedValues = new Rock[colCount][];
        for (int colIndex = 0; colIndex < colCount; colIndex++)
        {
            rotatedValues[colIndex] = new Rock[rowCount];
            for (int rowIndex = 0; rowIndex < rowCount; rowIndex++)
            {
                rotatedValues[colIndex][rowIndex] = values[rowIndex][colIndex];
            }
        }
        return rotatedValues;
    }

    private Rock[][] ParseRocks(IEnumerable<string> inputLines)
    {
        Rock[][] rocks = inputLines.Select((row, rowIndex) => row.Select((value, colIndex) => value switch
        {
            'O' => new Rock(RockType.Rounded, rowIndex, colIndex),
            '#' => new Rock(RockType.Cube, rowIndex, colIndex),
            '.' => new Rock(null, rowIndex, colIndex),
            _ => throw new Exception($"Unknown rock type: {value}")
        }).ToArray()).ToArray();
        return rocks;
    }

    record Rock(RockType? Type, int Row, int Col);

    enum RockType
    {
        Rounded,
        Cube,
    }
}