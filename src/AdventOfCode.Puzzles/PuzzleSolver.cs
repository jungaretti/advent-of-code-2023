namespace AdventOfCode.Puzzles;

public class PuzzleSolver
{
    private readonly IEnumerable<IPuzzleStrategy> strategies;

    public PuzzleSolver()
    {
        strategies = new IPuzzleStrategy[]
        {
            new Day00Part1Strategy(),
            new Day00Part2Strategy(),
        };
    }

    public PuzzleSolver(IEnumerable<IPuzzleStrategy> strategies)
    {
        this.strategies = strategies;
    }

    public string SolvePuzzle(int day, int part, IEnumerable<string> inputLines)
    {
        IPuzzleStrategy? strategy = strategies.SingleOrDefault(s => s.Day == day && s.Part == part);

        if (strategy == default)
        {
            throw new Exception($"No strategy found for day {day} part {part}");
        }

        return strategy.SolvePuzzle(inputLines);
    }
}