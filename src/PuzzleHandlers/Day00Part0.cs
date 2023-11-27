

namespace advent_of_code_2023;

public class Day00Part0 : PuzzleHandler
{
    public override int Day => 0;

    public override int Part => 0;

    public Day00Part0(IEnumerable<string> input) : base(input)
    {
    }

    public override string Solve()
    {
        var answer = Input.FirstOrDefault();
        return answer!;
    }
}
