
using System.Collections.Immutable;

namespace AdventOfCode;

class Day14 : IPuzzleDay
{
    public int Day => 14;

    public string PartOne(IEnumerable<string> inputLines)
    {
        var rocks = ParsePlatform(inputLines);
        return "5";
    }

    public string PartTwo(IEnumerable<string> inputLines)
    {
        throw new NotImplementedException();
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