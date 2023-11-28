public class PuzzleProvider
{
    private readonly IEnumerable<PuzzleInput> inputs;

    private readonly IEnumerable<PuzzleSolver> solvers;

    public PuzzleProvider(IEnumerable<PuzzleInput> inputs, IEnumerable<PuzzleSolver> solvers)
    {
        this.inputs = inputs;
        this.solvers = solvers;
    }

    public async Task<string> SolveAsync(int day, int part)
    {
        var input = inputs.FirstOrDefault(i => i.Day == day);

        if (input is null)
        {
            return await Task.FromException<string>(new Exception($"No input found for day {day}"));
        }

        var solver = solvers.FirstOrDefault(s => s.Day == day && s.Part == part);

        if (solver is null)
        {
            return await Task.FromException<string>(new Exception($"No solver found for day {day} part {part}"));
        }

        return await solver.SolveAsync(input);
    }
}
