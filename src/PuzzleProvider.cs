namespace advent_of_code_2023;

public class PuzzleProvider
{
    private readonly IEnumerable<PuzzleSolver> solvers;

    public PuzzleProvider(IEnumerable<PuzzleSolver> solvers)
    {
        this.solvers = solvers;
    }

    public Task<string> Solve(int day, int part)
    {
        PuzzleSolver? handler = solvers.FirstOrDefault(h => h.Day == day && h.Part == part);

        if (handler is null)
        {
            return Task.FromException<string>(new Exception($"No puzzle handler found for day {day} part {part}"));
        }

        return Task.FromResult(handler.Solve());
    }
}
