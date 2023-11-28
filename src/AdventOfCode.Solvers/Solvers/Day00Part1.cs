
namespace AdventOfCode.Solvers;

public class Day00Part1 : PuzzleSolver
{
    public int Day => 0;

    public int Part => 1;

    public Task<string> SolveAsync(PuzzleInput input)
    {
        var maxCalories = 0;
        var currentCalories = 0;

        foreach (var line in input.Input)
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

        return Task.FromResult(maxCalories.ToString());
    }
}
