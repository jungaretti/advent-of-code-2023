public interface PuzzleSolver
{
    int Day { get; }

    int Part { get; }

    Task<string> SolveAsync(PuzzleInput input);
}
