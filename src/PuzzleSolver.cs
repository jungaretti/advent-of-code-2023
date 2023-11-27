namespace advent_of_code_2023;

public class PuzzleSolver
{
    private readonly IEnumerable<PuzzleHandler> handlers;

    public PuzzleSolver(IEnumerable<PuzzleHandler> handlers)
    {
        this.handlers = handlers;
    }

    public Task<string> Solve(int day, int part)
    {
        PuzzleHandler? handler = handlers.FirstOrDefault(h => h.Day == day && h.Part == part);

        if (handler is null)
        {
            return Task.FromException<string>(new Exception("No handler found."));
        }

        return Task.FromResult(handler.Solve());
    }
}
