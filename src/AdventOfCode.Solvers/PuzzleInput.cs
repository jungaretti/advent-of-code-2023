public class PuzzleInput
{
    public int Day { get; }

    public IEnumerable<string> Input { get; }

    public PuzzleInput(int day, IEnumerable<string> input)
    {
        Day = day;
        Input = input;
    }
}