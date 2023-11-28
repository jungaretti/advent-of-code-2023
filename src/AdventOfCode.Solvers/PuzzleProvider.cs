public class PuzzleProvider
{
    private readonly IEnumerable<PuzzleSolver> solvers;

    public PuzzleProvider(IEnumerable<PuzzleSolver> solvers)
    {
        this.solvers = solvers;
    }

    public async Task<string> SolveAsync(int day, int part, IEnumerable<string> input)
    {
        var solver = solvers.FirstOrDefault(s => s.Day == day && s.Part == part);

        if (solver is null)
        {
            return await Task.FromException<string>(new Exception($"No solver found for day {day} part {part}"));
        }

        return await solver.SolveAsync(input);
    }
}
