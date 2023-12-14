
using System.Collections.Immutable;

namespace AdventOfCode;

class Day14 : IPuzzleDay
{
    public int Day => 14;

    public string PartOne(IEnumerable<string> inputLines)
    {
        var rocks = ParseRocks(inputLines);
        IEnumerable<IEnumerable<Rock>> rockCols = Rotate90Clockwise(rocks);
        IEnumerable<IEnumerable<Rock>> tiltedRocks = rockCols.Select(TiltRocks);
        var rockScores = tiltedRocks.Select(rocks => RockScores(rocks).Sum());

        var answer = rockScores.Sum();
        return answer.ToString();
    }

    public string PartTwo(IEnumerable<string> inputLines)
    {
        throw new NotImplementedException();
    }

    private IEnumerable<int> RockScores(IEnumerable<Rock> values)
    {
        int nextScore = values.Count();
        foreach (Rock rock in values)
        {
            int score = nextScore;
            nextScore--;

            if (rock.Type == RockType.Rounded)
            {
                yield return score;
            }
        }
    }

    private IEnumerable<IEnumerable<TSource>> Rotate90Clockwise<TSource>(TSource[][] values)
    {
        var colCount = values[0].Length;
        for (int colIndex = 0; colIndex < colCount; colIndex++)
        {
            yield return values.Select(row => row[colIndex]);
        }
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