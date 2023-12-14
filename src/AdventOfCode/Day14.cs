
using System.Collections.Immutable;

namespace AdventOfCode;

class Day14 : IPuzzleDay
{
    public int Day => 14;

    public string PartOne(IEnumerable<string> inputLines)
    {
        var platform = ParsePlatform(inputLines);
        IEnumerable<IEnumerable<RockType?>> rockCols = GetCols(platform.Rocks);
        IEnumerable<IEnumerable<RockType?>> tiltedRocks = rockCols.Select(TiltRocks);
        var rockScores = tiltedRocks.Select(rocks => RockScores(rocks).Sum());

        var answer = rockScores.Sum();
        return answer.ToString();
    }

    public string PartTwo(IEnumerable<string> inputLines)
    {
        throw new NotImplementedException();
    }

    private IEnumerable<int> RockScores(IEnumerable<RockType?> values)
    {
        int nextScore = values.Count();
        foreach (RockType? rock in values)
        {
            int score = nextScore;
            nextScore--;

            if (rock == RockType.Rounded)
            {
                yield return score;
            }
        }
    }

    private IEnumerable<IEnumerable<TSource>> GetCols<TSource>(TSource[][] values)
    {
        var colCount = values[0].Length;
        for (int colIndex = 0; colIndex < colCount; colIndex++)
        {
            yield return values.Select(row => row[colIndex]);
        }
    }

    private IEnumerable<RockType?> TiltRocks(IEnumerable<RockType?> rocks)
    {
        if (rocks.Count() == 0)
        {
            return rocks;
        }

        var barrierIndex = rocks.ToList().IndexOf(RockType.Cube);
        if (barrierIndex == -1)
        {
            barrierIndex = rocks.Count();
        }

        var tiledRocks = rocks
            .Take(barrierIndex + 1)
            .OrderBy(rock => rock switch
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

    private Platform ParsePlatform(IEnumerable<string> inputLines)
    {
        var rocks = inputLines.Select(line => line.Select<char, RockType?>(rockChar => rockChar switch
        {
            'O' => RockType.Rounded,
            '#' => RockType.Cube,
            '.' => null,
            _ => throw new Exception($"Unknown rock type: {rockChar}")
        }).ToArray()).ToArray();
        return new Platform(rocks);
    }

    record Platform(RockType?[][] Rocks);

    enum RockType
    {
        Rounded,
        Cube,
    }
}