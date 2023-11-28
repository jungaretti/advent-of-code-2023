namespace AdventOfCode.Puzzles;

class Day00Part1Strategy : IPuzzleStrategy
{
    public int Day => 0;

    public int Part => 1;

    public string SolvePuzzle(IEnumerable<string> inputLines)
    {
        var maxCalories = 0;
        var currentCalories = 0;

        foreach (var line in inputLines)
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
