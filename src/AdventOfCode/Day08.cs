using System.Text.RegularExpressions;

namespace AdventOfCode;

class Day08 : IPuzzleDay
{
    public int Day => 8;

    public string PartOne(IEnumerable<string> inputLines)
    {
        var instructions = ParseInstructions(inputLines.First());
        var network = new Network(inputLines.Skip(2));

        var answer = network.GetStepCountForInstructions(instructions, "AAA", (node) => node.Id == "ZZZ");
        return answer.ToString();
    }

    public string PartTwo(IEnumerable<string> inputLines)
    {
        var instructions = ParseInstructions(inputLines.First());
        var network = new Network(inputLines.Skip(2));

        IEnumerable<Node> startNodes = network.Nodes.Where((node) => node.Id.EndsWith("A"));
        IEnumerable<int> stepCounts = startNodes.Select((node) => network.GetStepCountForInstructions(instructions, node.Id, (node) => node.Id.EndsWith("Z")));

        long answer = stepCounts.Aggregate((long)1, (acc, current) => Helpers.Lcm(acc, current));
        return answer.ToString();
    }

    private IEnumerable<Instruction> ParseInstructions(string instructionsLine)
    {
        return instructionsLine.Select(instruction => instruction switch
        {
            'L' => new Instruction(Direction.Left),
            'R' => new Instruction(Direction.Right),
            _ => throw new Exception($"Unknown instruction: {instruction}"),
        });
    }

    class Network
    {
        private Dictionary<string, Node> nodesById;

        public IEnumerable<Node> Nodes => nodesById.Values;

        public Network(IEnumerable<string> networkLines)
        {
            var regex = new Regex(@"^(\w+) = \((\w+), (\w+)\)$");
            var nodes = networkLines.Select(line =>
            {
                var match = regex.Match(line);
                if (!match.Success)
                {
                    throw new Exception($"Failed to parse line: {line}");
                }

                string id = match.Groups[1].Value;
                string leftId = match.Groups[2].Value;
                string rightId = match.Groups[3].Value;

                Node node = new Node(id, leftId, rightId);
                return node;
            });

            nodesById = nodes.ToDictionary(node => node.Id);
        }

        public int GetStepCountForInstructions(IEnumerable<Instruction> instructions, string startNodeId, Func<Node, bool> endNodePredicate)
        {
            int stepCount = 0;

            Instruction[] instructionsArray = instructions.ToArray();
            int currentInstructionIndex = 0;

            Node currentNode = nodesById[startNodeId];
            while (!endNodePredicate(currentNode))
            {
                Instruction currentInstruction = instructionsArray[currentInstructionIndex % instructionsArray.Length];

                currentNode = currentInstruction.Direction switch
                {
                    Direction.Left => nodesById[currentNode.LeftId],
                    Direction.Right => nodesById[currentNode.RightId],
                    _ => throw new Exception($"Unknown instruction: {currentInstruction.Direction}"),
                };

                currentInstructionIndex++;
                stepCount++;
            }

            return stepCount;
        }
    }

    private record Node(string Id, string LeftId, string RightId);

    private record Instruction(Direction Direction);

    private enum Direction
    {
        Left,
        Right,
    }
}