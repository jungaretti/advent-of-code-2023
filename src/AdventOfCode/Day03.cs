using System.Security.Cryptography.X509Certificates;

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

            for (int rowIndex = 0; rowIndex < cellGrid.Length; rowIndex++)
            {
                EngineNode previousNode = nodes[rowIndex][0];
                for (int colIndex = 1; colIndex < cellGrid[rowIndex].Length; colIndex++)
                {
                    EngineNode currentNode = nodes[rowIndex][colIndex];
                    currentNode.MergeWith(previousNode, nodes);
                }
            }

            var someNode = nodes[1][1];
            var neighbors = someNode.FindNeighbors(nodes);
        }
    }

    class EngineNode
    {
        private List<EngineCell> cells;

        public string Value => new string(cells.Select(cell => cell.Value).ToArray());

        public EngineNode(EngineCell cell)
        {
            cells = new List<EngineCell> { cell };
        }

        public HashSet<EngineNode> FindNeighbors(EngineNode[][] allNodes)
        {
            var neighbors = new HashSet<EngineNode>();
            foreach (var cell in cells)
            {
                foreach (var rowOffset in new int[] { -1, 0, 1 })
                {
                    foreach (var colOffset in new int[] { -1, 0, 1 })
                    {
                        EngineNode? adjacent = allNodes.ElementAtOrDefault(cell.Row + rowOffset)?.ElementAtOrDefault(cell.Column + colOffset);

                        if (adjacent == default)
                        {
                            continue;
                        }

                        neighbors.Add(adjacent);
                    }
                }
            }

            neighbors.Remove(this);

            return neighbors;
        }

        public void MergeWith(EngineNode leftNode, EngineNode[][] allNodes)
        {
            var newCells = leftNode.cells;
            newCells.AddRange(cells);
            newCells.Sort((leftCell, rightCell) => leftCell.Column.CompareTo(rightCell.Column));

            foreach (var cell in newCells)
            {
                allNodes[cell.Row][cell.Column] = this;
            }

            cells = newCells;
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