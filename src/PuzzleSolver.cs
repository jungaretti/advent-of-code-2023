namespace advent_of_code_2023;

public abstract class PuzzleSolver
{
    public abstract int Day { get; }

    public abstract int Part { get; }

    public IEnumerable<string> Input { get; }

    public PuzzleSolver(IEnumerable<string> input)
    {
        Input = input;
    }

    public abstract string Solve();
}
