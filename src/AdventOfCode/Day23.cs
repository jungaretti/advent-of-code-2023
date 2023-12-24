namespace AdventOfCode;

class Day23 : IPuzzleDay
{
    public int Day => 23;

    public string PartOne(IEnumerable<string> inputLines)
    {
        char[][] grid = inputLines.Select(s => s.ToCharArray()).ToArray();

        Coordinate start = new(0, grid.First().TakeWhile(c => c != '.').Count());
        Coordinate end = new(grid.Length - 1, grid.Last().TakeWhile(c => c != '.').Count());

        // Find "interesting" points where we can make a decision
        // Trim walled points to reduce the number of points to consider
        IEnumerable<Coordinate> points = grid
            .SelectMany((row, rowIndex) => row.Select((value, colIndex) =>
            {
                if (value == '#')
                    return null;

                var neighbors = new Coordinate[] {
                    new(rowIndex - 1, colIndex),
                    new(rowIndex + 1, colIndex),
                    new(rowIndex, colIndex - 1),
                    new(rowIndex, colIndex + 1),
                };
                var validNeighbors = neighbors
                    .Where(c => c.Row >= 0 && c.Row < grid.Length)
                    .Where(c => c.Column >= 0 && c.Column < row.Length)
                    .Where(c => grid[c.Row][c.Column] != '#');

                if (validNeighbors.Count() >= 3)
                    return new Coordinate(rowIndex, colIndex);
                else
                    return null;
            }))
            .OfType<Coordinate>()
            .Append(start)
            .Append(end);

        Dictionary<char, Coordinate[]> directions = new() {
            { '^', [new(-1, 0)] },
            { 'v', [new(1, 0)] },
            { '<', [new(0, -1)] },
            { '>', [new(0, 1)] },
            { '.', [new(-1, 0), new(1, 0), new(0, -1), new(0, 1)]}
        };

        // Build a graph of all the points and their distances
        // We'll use this graph to find the longest path
        Dictionary<Coordinate, Dictionary<Coordinate, int>> graph = points
            .ToDictionary(p => p, p => new Dictionary<Coordinate, int>());
        foreach (var startingPoint in points)
        {
            var stack = new Stack<(Coordinate coordinate, int distance)>([(startingPoint, 0)]);
            var seen = new HashSet<Coordinate>([startingPoint]);

            while (stack.TryPop(out var point))
            {
                if (point.distance > 0 && points.Contains(point.coordinate))
                {
                    graph[startingPoint][point.coordinate] = point.distance;
                    continue;
                }

                foreach (var direction in directions[grid[point.coordinate.Row][point.coordinate.Column]])
                {
                    var newCoordinate = new Coordinate(point.coordinate.Row + direction.Row, point.coordinate.Column + direction.Column);

                    if (
                        newCoordinate.Row >= 0 && newCoordinate.Row < grid.Length &&
                        newCoordinate.Column >= 0 && newCoordinate.Column < grid[newCoordinate.Row].Length &&
                        grid[newCoordinate.Row][newCoordinate.Column] != '#' &&
                        !seen.Contains(newCoordinate)
                    )
                    {
                        stack.Push((newCoordinate, point.distance + 1));
                        seen.Add(newCoordinate);
                    }
                }
            }
        }

        // Find the longest path
        var dfsSeen = new HashSet<Coordinate>();

        int LongestDistance(Coordinate coordinate)
        {
            if (coordinate == end)
                return 0;

            var m = int.MinValue;

            dfsSeen.Add(coordinate);
            foreach (var nextCoordinate in graph[coordinate].Keys)
            {
                m = Math.Max(m, LongestDistance(nextCoordinate) + graph[coordinate][nextCoordinate]);
            }
            dfsSeen.Remove(coordinate);

            return m;
        }

        var answer = LongestDistance(start);
        return answer.ToString();
    }

    public string PartTwo(IEnumerable<string> inputLines)
    {
        return "0";
    }

    private record Coordinate(int Row, int Column);
}
