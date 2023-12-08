using System.Text.RegularExpressions;

namespace AdventOfCode;

class Day08 : IPuzzleDay
{
    public int Day => 8;

    public string PartOne(IEnumerable<string> inputLines)
    {
        var instructions = ParseInstructions(inputLines.First());
        var network = new Network(inputLines.Skip(2));

        var answer = network.GetStepCountForInstructions(instructions, (node) => node.Id == "AAA", (node) => node.Id == "ZZZ");
        return answer.ToString();
    }

    public string PartTwo(IEnumerable<string> inputLines)
    {
        throw new NotImplementedException();
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

        public int GetStepCountForInstructions(IEnumerable<Instruction> instructions, Func<Node, bool> startNodePredicate, Func<Node, bool> endNodePredicate)
        {
            int stepCount = 0;

            Instruction[] instructionsArray = instructions.ToArray();
            int currentInstructionIndex = 0;

            IEnumerable<Node> currentNodes = nodesById.Values.Where(startNodePredicate);
            while (currentNodes.Any(node => !endNodePredicate(node)))
            {
                Instruction currentInstruction = instructionsArray[currentInstructionIndex % instructionsArray.Length];

                var newNodes = currentNodes.Select(currentNode => currentInstruction.Direction switch
                {
                    Direction.Left => nodesById[currentNode.LeftId],
                    Direction.Right => nodesById[currentNode.RightId],
                    _ => throw new Exception($"Unknown instruction: {currentInstruction.Direction}"),
                });

                currentNodes = newNodes;
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