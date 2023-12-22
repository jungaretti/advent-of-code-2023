
using System.Text.RegularExpressions;

namespace AdventOfCode;

class Day22 : IPuzzleDay
{
    public int Day => 22;

    public string PartOne(IEnumerable<string> inputLines)
    {
        var bricks = ParseBricks(inputLines);
        var platform = BuildPlatform(bricks);

        return "0";
    }

    public string PartTwo(IEnumerable<string> inputLines)
    {
        return "0";
    }

    private readonly Regex brickRegex = new(@"^(\d+),(\d+),(\d+)~(\d+),(\d+),(\d+)$", RegexOptions.Compiled);

    Brick[][] BuildPlatform(IEnumerable<Brick> bricks)
    {
        int maxX = bricks.MaxBy(b => b.End.X)?.End.X ?? throw new Exception("Could not find max X");
        int maxY = bricks.MaxBy(b => b.End.Y)?.End.Y ?? throw new Exception("Could not find max Y");

        Brick platformBrick = new Brick(
            new Coordinate(0, 0, 0),
            new Coordinate(maxX, maxY, 0));

        Brick[][] platform = new Brick[maxY + 1][];
        for (int y = 0; y < platform.Length; y++)
        {
            platform[y] = Enumerable.Repeat(platformBrick, maxX + 1).ToArray();
        }

        return platform;
    }

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

    private IEnumerable<Coordinate> GetAllCoordinates(Brick brick)
    {
        for (int x = brick.Start.X; x <= brick.End.X; x++)
        {
            for (int y = brick.Start.Y; y <= brick.End.Y; y++)
            {
                for (int z = brick.Start.Z; z <= brick.End.Z; z++)
                {
                    yield return new Coordinate(x, y, z);
                }
            }
        }
    }

    private record Brick(Coordinate Start, Coordinate End);

    private record Coordinate(int X, int Y, int Z);
}