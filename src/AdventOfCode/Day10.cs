
namespace AdventOfCode;

class Day10 : IPuzzleDay
{
    public int Day => 10;

    public string PartOne(IEnumerable<string> inputLines)
    {
        Node[][] nodes = inputLines.Select(line => line.Select(c => new Node(c)).ToArray()).ToArray();

        Dictionary<Node, List<Edge>> edges = new();
        foreach (var (row, rowIndex) in nodes.Select((row, index) => (row, index)))
        {
            foreach (var (node, nodeIndex) in row.Select((node, index) => (node, index)))
            {
                edges[node] = new();
                switch (node.Value)
                {
                    case '|':
                        // Vertical pipe connecting north and south.
                        edges[node].Add(new(nodes[rowIndex - 1][nodeIndex], nodes[rowIndex + 1][nodeIndex]));
                        break;
                    case '-':
                        // Horizontal pipe connecting east and west.
                        edges[node].Add(new(nodes[rowIndex][nodeIndex - 1], nodes[rowIndex][nodeIndex + 1]));
                        break;
                    case 'L':
                        // 90-degree bend connecting north and east.
                        edges[node].Add(new(nodes[rowIndex - 1][nodeIndex], nodes[rowIndex][nodeIndex + 1]));
                        break;
                    case 'J':
                        // 90-degree bend connecting north and west.
                        edges[node].Add(new(nodes[rowIndex - 1][nodeIndex], nodes[rowIndex][nodeIndex - 1]));
                        break;
                    case '7':
                        // 90-degree bend connecting south and west.
                        edges[node].Add(new(nodes[rowIndex + 1][nodeIndex], nodes[rowIndex][nodeIndex - 1]));
                        break;
                    case 'F':
                        // 90-degree bend connecting south and east.
                        edges[node].Add(new(nodes[rowIndex + 1][nodeIndex], nodes[rowIndex][nodeIndex + 1]));
                        break;
                    case 'S':
                        // Ignore start node
                        break;
                    case '.':
                        // Ignore empty nodes
                        break;
                    default:
                        throw new Exception($"Unexpected node value: {node.Value}");
                }
            }
        }

        throw new NotImplementedException();
    }

    public string PartTwo(IEnumerable<string> inputLines)
    {
        throw new NotImplementedException();
    }

    private record Node(char Value);

    private record Edge(Node From, Node To);
}