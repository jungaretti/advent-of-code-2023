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