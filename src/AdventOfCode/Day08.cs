using System.Text.RegularExpressions;

namespace AdventOfCode;

class Day08 : IPuzzleDay
{
    public int Day => 8;

    public string PartOne(IEnumerable<string> inputLines)
    {
        var instructions = ParseInstructions(inputLines.First());
        var network = new Network(inputLines.Skip(2));

        var answer = network.GetStepsToFollowInstructions(instructions, new[] { "AAA" });
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

        public int GetStepsToFollowInstructions(IEnumerable<Instruction> instructions, IEnumerable<string> startNodeIds)
        {
            int instructionIndex = 0;
            int stepCount = 0;
            Node currentNode = nodesById[startNodeIds.First()];
            do
            {
                var oldNode = currentNode;
                var instruction = instructions.ElementAt(instructionIndex % instructions.Count());
                switch (instruction.Direction)
                {
                    case Direction.Left:
                        currentNode = nodesById[currentNode.LeftId];
                        break;
                    case Direction.Right:
                        currentNode = nodesById[currentNode.RightId];
                        break;
                }

                Console.WriteLine($"Moving {instruction.Direction} from {oldNode.Id} to {currentNode.Id}");

                stepCount++;
                instructionIndex++;
            } while (currentNode.Id != "ZZZ");

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