
using System.Text.RegularExpressions;

namespace AdventOfCode;

class Day22 : IPuzzleDay
{
    public int Day => 22;

    public string PartOne(IEnumerable<string> inputLines)
    {
        var bricks = ParseBricks(inputLines);
        var settledBricks = SettleBricks(bricks);

        var safeToRemoveBricks = settledBricks.Where(removedBrick =>
        {
            // Try to settle the bricks without this brick
            var newlySettledBricks = SettleBricks(settledBricks.Except(new[] { removedBrick }));

            // If removing this brick causes other bricks to move, it is not safe to remove
            if (newlySettledBricks.Except(settledBricks).Any(brick => brick != removedBrick))
            {
                return false;
            }

            // If removing this brick does not change the settled bricks, then we can remove it
            return true;
        });

        var answer = safeToRemoveBricks.Count();
        return answer.ToString();
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

    private List<Brick> SettleBricks(IEnumerable<Brick> bricks)
    {
        var allOrderedBricks = bricks.OrderBy(brick => brick.Start.Z);

        List<Brick> allSettledBricks = new();
        foreach (Brick brick in allOrderedBricks)
        {
            IEnumerable<Coordinate> allSettledCoordinates = allSettledBricks.SelectMany(GetAllCoordinates);

            var settledBrick = brick;
            var shiftedBrick = ShiftBrick(settledBrick, z: -1);
            while (GetAllCoordinates(shiftedBrick).Intersect(allSettledCoordinates).Any() == false)
            {
                // Do not allow bricks to go below Z=0
                if (shiftedBrick.Start.Z <= 0 || shiftedBrick.End.Z <= 0)
                {
                    break;
                }

                // Move the brick down one layer
                settledBrick = shiftedBrick;
                shiftedBrick = ShiftBrick(shiftedBrick, z: -1);
            }

            allSettledBricks.Add(settledBrick);
        }
        return allSettledBricks;
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

    private Brick ShiftBrick(Brick brick, int x = 0, int y = 0, int z = 0)
    {
        return brick with
        {
            Start = brick.Start with
            {
                X = brick.Start.X + x,
                Y = brick.Start.Y + y,
                Z = brick.Start.Z + z
            },
            End = brick.End with
            {
                X = brick.End.X + x,
                Y = brick.End.Y + y,
                Z = brick.End.Z + z
            }
        };
    }

    private record Brick(Coordinate Start, Coordinate End);

    private record Coordinate(int X, int Y, int Z);
}