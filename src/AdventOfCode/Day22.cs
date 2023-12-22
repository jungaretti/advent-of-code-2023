
using System.Text.RegularExpressions;

namespace AdventOfCode;

class Day22 : IPuzzleDay
{
    public int Day => 22;

    public string PartOne(IEnumerable<string> inputLines)
    {
        var bricks = ParseBricks(inputLines);

        return "0";
    }

    public string PartTwo(IEnumerable<string> inputLines)
    {
        return "0";
    }

    private readonly Regex brickRegex = new(@"^(\d+),(\d+),(\d+)~(\d+),(\d+),(\d+)$", RegexOptions.Compiled);

    IEnumerable<Brick> ParseBricks(IEnumerable<string> inputLines)
    {
        foreach (string line in inputLines)
        {
            var match = brickRegex.Match(line);
            if (match.Success == false)
            {
                throw new Exception($"Could not parse brick: {line}");
            }

            var coordinates = new Coordinate[]
            {
                new(
                    int.Parse(match.Groups[1].Value),
                    int.Parse(match.Groups[2].Value),
                    int.Parse(match.Groups[3].Value)),
                new(
                    int.Parse(match.Groups[4].Value),
                    int.Parse(match.Groups[5].Value),
                    int.Parse(match.Groups[6].Value))
            };
            var orderedCoordinates = coordinates
                .OrderBy(coordinate => coordinate.Z)
                .ThenBy(coordinate => coordinate.Y)
                .ThenBy(coordinate => coordinate.X);

            yield return new Brick(
                orderedCoordinates.First(),
                orderedCoordinates.Last()
            );
        }
    }

    private record Brick(Coordinate Start, Coordinate End);

    private record Coordinate(int X, int Y, int Z);
}