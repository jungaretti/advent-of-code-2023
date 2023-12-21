
namespace AdventOfCode;

class Day21 : IPuzzleDay
{
    public int Day => 21;

    public string PartOne(IEnumerable<string> inputLines)
    {
        var garden = new Garden(inputLines);
        return "0";
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
    }

    private record Tile(char Type, int Row, int Col);
}
