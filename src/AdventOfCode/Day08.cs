using System.Text.RegularExpressions;

namespace AdventOfCode;

class Day08 : IPuzzleDay
{
    public int Day => 8;

    public string PartOne(IEnumerable<string> inputLines)
    {
        var instructions = ParseInstructions(inputLines.First());
        var nodes = ParseNodes(inputLines.Skip(2));

        int instructionIndex = 0;
        int stepCount = 0;
        Node currentNode = nodes.Single(node => node.Id == "AAA");
        do
        {
            var oldNode = currentNode;
            var instruction = instructions.ElementAt(instructionIndex % instructions.Count());
            switch (instruction.Direction)
            {
                case Direction.Left:
                    currentNode = nodes.Single(node => node.Id == currentNode.Left);
                    break;
                case Direction.Right:
                    currentNode = nodes.Single(node => node.Id == currentNode.Right);
                    break;
            }

            Console.WriteLine($"Moving {instruction.Direction} from {oldNode.Id} to {currentNode.Id}");

            stepCount++;
            instructionIndex++;
        } while (currentNode.Id != "ZZZ");

        return stepCount.ToString();
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

    private IEnumerable<Node> ParseNodes(IEnumerable<string> networkLines)
    {
        var regex = new Regex(@"^(\w+) = \((\w+), (\w+)\)$");
        return networkLines.Select(line =>
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
    }

    private record Node(string Id, string Left, string Right);

    private record Instruction(Direction Direction);

    private enum Direction
    {
        Left,
        Right,
    }
}