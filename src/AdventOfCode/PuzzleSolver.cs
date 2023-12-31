namespace AdventOfCode;

public class PuzzleSolver
{
    private readonly IEnumerable<IPuzzleDay> days;

    public PuzzleSolver()
    {
        days = new IPuzzleDay[]
        {
            new Day01(),
            new Day02(),
            new Day03(),
            new Day04(),
            new Day05(),
            new Day06(),
            new Day07(),
            new Day08(),
            new Day09(),
            new Day11(),
            new Day14(),
            new Day15(),
            new Day19(),
            new Day21(),
            new Day22(),
            new Day23(),
        };
    }

    public PuzzleSolver(IEnumerable<IPuzzleDay> days)
    {
        this.days = days;
    }

    public string SolvePuzzle(int day, int part, IEnumerable<string> inputLines)
    {
        IPuzzleDay? puzzleDay = days.SingleOrDefault(s => s.Day == day);

        if (puzzleDay == default)
        {
            throw new Exception($"Could not find solver for day {day}");
        }

        switch (part)
        {
            case 1:
                return puzzleDay.PartOne(inputLines);
            case 2:
                return puzzleDay.PartTwo(inputLines);
            default:
                throw new Exception($"Could not find solver for part {part}");
        }
    }
}
