namespace AdventOfCode.Puzzles;

public interface IPuzzleStrategy
{
    int Day { get; }

    int Part { get; }

    string SolvePuzzle(IEnumerable<string> inputLines);
}