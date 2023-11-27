namespace advent_of_code_2023;

public abstract class PuzzleHandler
{
    public abstract int Day { get; }

    public abstract int Part { get; }

    public IEnumerable<string> Input { get; }

    public PuzzleHandler(IEnumerable<string> input)
    {
        Input = input;
    }

    public abstract string Solve();
}
