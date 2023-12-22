
using System.Text.RegularExpressions;

namespace AdventOfCode;

class Day22 : IPuzzleDay
{
    public int Day => 22;

    public string PartOne(IEnumerable<string> inputLines)
    {
        var bricks = ParseBricks(inputLines);
        var platform = BuildPlatform(bricks);

        // Brick to the bricks that it supports
        Dictionary<Brick, HashSet<Brick>> supportedBy = new();

        // Brick to the bricks that support it (inverse of supportedBy)
        Dictionary<Brick, HashSet<Brick>> supports = new();

        foreach (var brick in bricks.OrderBy(b => b.Start.Z))
        {
            IEnumerable<Coordinate> brickCoordinates = GetAllCoordinates(brick);
            IEnumerable<Brick> bricksBelow = brickCoordinates
                .Select(coordinate => platform[coordinate.Y][coordinate.X])
                .OfType<Brick>();

            int supportingZ = bricksBelow.MaxBy(b => b.End.Z)?.End.Z ?? 0;
            HashSet<Brick> supportingBricks = bricksBelow.Where(b => b.End.Z == supportingZ).ToHashSet();

            var brickHeight = brick.End.Z - brick.Start.Z + 1;
            var settledBrick = brick with
            {
                Start = brick.Start with { Z = supportingZ + 1 },
                End = brick.End with { Z = supportingZ + brickHeight }
            };

            supports.Add(settledBrick, new());
            supportedBy.Add(settledBrick, supportingBricks);
            foreach (var supportingBrick in supportingBricks)
            {
                supports[supportingBrick].Add(settledBrick);
            }

            foreach (var coordinate in brickCoordinates)
            {
                platform[coordinate.Y][coordinate.X] = settledBrick;
            }
        }

        IEnumerable<Brick> settledBricks = supports.Keys;
        HashSet<Brick> answer = new();

        foreach (var brick in settledBricks)
        {
            var canRemoveBrick = true;
            foreach (var dependentBrick in supports[brick])
            {
                if (supportedBy[dependentBrick].Count == 1)
                {
                    canRemoveBrick = false;
                }
            }

            if (canRemoveBrick)
            {
                answer.Add(brick);
            }
        }

        return answer.Count.ToString();
    }

    public string PartTwo(IEnumerable<string> inputLines)
    {
        return "0";
    }

    private readonly Regex brickRegex = new(@"^(\d+),(\d+),(\d+)~(\d+),(\d+),(\d+)$", RegexOptions.Compiled);

    Brick?[][] BuildPlatform(IEnumerable<Brick> bricks)
    {
        int maxX = bricks.MaxBy(b => b.End.X)?.End.X ?? throw new Exception("Could not find max X");
        int maxY = bricks.MaxBy(b => b.End.Y)?.End.Y ?? throw new Exception("Could not find max Y");

        Brick?[][] platform = new Brick[maxY + 1][];
        for (int y = 0; y < platform.Length; y++)
        {
            platform[y] = Enumerable.Repeat<Brick?>(null, maxX + 1).ToArray();
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