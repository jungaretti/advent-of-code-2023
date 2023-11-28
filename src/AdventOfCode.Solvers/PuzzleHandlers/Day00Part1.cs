namespace AdventOfCode.Solvers;

public class Day00Part1 : PuzzleHandler
{
    public override int Day => 0;

    public override int Part => 1;

    public Day00Part1(IEnumerable<string> input) : base(input)
    {
    }

    public override string Solve()
    {
        var maxCalories = 0;
        var currentCalories = 0;

        foreach (var line in Input)
        {
            var didParseCalories = int.TryParse(line, out var calories);

            if (didParseCalories == false)
            {
                currentCalories = 0;
                continue;
            }

            currentCalories += calories;

            if (currentCalories > maxCalories)
            {
                maxCalories = currentCalories;
            }
        }

        return maxCalories.ToString();
    }
}
