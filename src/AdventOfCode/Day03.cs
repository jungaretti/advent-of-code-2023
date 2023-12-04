
using System.ComponentModel.Design;

namespace AdventOfCode;

public class Day03 : IPuzzleDay
{
    public int Day => 3;

    public string PartOne(IEnumerable<string> inputLines)
    {
        var engineGrid = new EngineGrid(inputLines);

        throw new NotImplementedException();
    }

    public string PartTwo(IEnumerable<string> inputLines)
    {
        throw new NotImplementedException();
    }

    class EngineGrid
    {
        EngineNode[][] nodes;

        public EngineGrid(IEnumerable<string> inputLines)
        {
            IEnumerable<IEnumerable<char>> charGrid = inputLines.Select(line => line.AsEnumerable());
            char[][] paddedCharGrid = charGrid.Select(row => row.Append('.').ToArray()).ToArray();

            EngineCell[][] cellGrid = new EngineCell[paddedCharGrid.Length][];
            for (int rowIndex = 0; rowIndex < paddedCharGrid.Length; rowIndex++)
            {
                cellGrid[rowIndex] = new EngineCell[paddedCharGrid[rowIndex].Length];
                for (int colIndex = 0; colIndex < paddedCharGrid[rowIndex].Length; colIndex++)
                {
                    cellGrid[rowIndex][colIndex] = new EngineCell(rowIndex, colIndex, paddedCharGrid[rowIndex][colIndex]);
                }
            }

            nodes = new EngineNode[cellGrid.Length][];
            for (int rowIndex = 0; rowIndex < cellGrid.Length; rowIndex++)
            {
                nodes[rowIndex] = new EngineNode[cellGrid[rowIndex].Length];
                for (int colIndex = 0; colIndex < cellGrid[rowIndex].Length; colIndex++)
                {
                    EngineCell engineCell = cellGrid[rowIndex][colIndex];
                    nodes[rowIndex][colIndex] = new EngineNode(engineCell);
                }
            }

            var thing = "hello";
        }
    }

    class EngineNode
    {
        private readonly List<EngineCell> cells;

        public string Value => new string(cells.Select(cell => cell.Value).ToArray());

        public EngineNode(EngineCell cell)
        {
            cells = new List<EngineCell> { cell };
        }
    }

    class EngineCell
    {
        public int Row { get; }

        public int Column { get; }

        public char Value { get; }

        public EngineCell(int row, int column, char value)
        {
            Row = row;
            Column = column;
            Value = value;
        }
    }
}