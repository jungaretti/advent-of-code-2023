namespace AdventOfCode.Puzzles;

public interface IPuzzleDay
{
    int Day { get; }

    string PartOne(IEnumerable<string> inputLines);

    string PartTwo(IEnumerable<string> inputLines);
}