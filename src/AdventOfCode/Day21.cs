using System.Text;

namespace AdventOfCode;

class Day21 : IPuzzleDay
{
    public int Day => 21;

    public string PartOne(IEnumerable<string> inputLines)
    {
        var garden = new Garden(inputLines);
        var possibleTiles = garden.Walk(6);

        var answer = possibleTiles.Count();
        return answer.ToString();
    }

    public string PartTwo(IEnumerable<string> inputLines)
    {
        throw new NotImplementedException();
    }

    private class Garden
    {
        private readonly Tile[][] tiles;

        public Garden(IEnumerable<string> inputLines)
        {
            tiles = inputLines.Select((row, rowIndex) => row.Select((column, columnIndex) =>
            {
                return new Tile(column, rowIndex, columnIndex);
            }).ToArray()).ToArray();
        }

        public IEnumerable<Tile> Walk(int steps)
        {
            Direction north = new(-1, 0);
            Direction south = new(1, 0);
            Direction east = new(0, 1);
            Direction west = new(0, -1);
            List<Direction> possibleDirections = [north, south, east, west];

            Tile startTile = tiles
                .SelectMany(row => row)
                .Single(tile => tile.Type == 'S');

            HashSet<Tile> currentTiles = [startTile];
            for (int stepCount = 0; stepCount < steps; stepCount++)
            {
                HashSet<Tile> nextTiles = new();
                foreach (Tile tile in currentTiles)
                {
                    foreach (Direction direction in possibleDirections)
                    {
                        Tile nextTile = tiles[tile.Row + direction.RowDiff][tile.Col + direction.ColDiff];
                        if (nextTile.Type == '.' || nextTile.Type == 'S')
                        {
                            nextTiles.Add(nextTile);
                        }
                    }
                }
                currentTiles = nextTiles;
            }

            return currentTiles;
        }
    }

    private record Tile(char Type, int Row, int Col);

    private record Direction(int RowDiff, int ColDiff);
}
