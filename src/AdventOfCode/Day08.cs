namespace AdventOfCode;

class Day08 : IPuzzleDay
{
    public int Day => 8;

    public string PartOne(IEnumerable<string> inputLines)
    {
        var instructions = ParseInstructions(inputLines.First());

        throw new NotImplementedException();
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

    private record Instruction(Direction Direction);

    private enum Direction
    {
        Left,
        Right,
    }
}