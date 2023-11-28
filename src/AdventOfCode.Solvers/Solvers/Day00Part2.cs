
namespace AdventOfCode.Solvers;

public class Day00Part2 : PuzzleSolver
{
    public int Day => 0;

    public int Part => 2;

    public Task<string> SolveAsync(PuzzleInput input)
    {
        var allCalories = new PriorityQueue<int, int>();
        var currentCalories = 0;

        foreach (var line in input.Input)
        {
            var didParseCalories = int.TryParse(line, out var calories);

            if (didParseCalories == false)
            {
                allCalories.Enqueue(currentCalories, int.MaxValue - currentCalories);

                currentCalories = 0;
                continue;
            }

            currentCalories += calories;
        }

        allCalories.Enqueue(currentCalories, int.MaxValue - currentCalories);

        var answer = 0;
        answer += allCalories.Dequeue();
        answer += allCalories.Dequeue();
        answer += allCalories.Dequeue();

        return Task.FromResult(answer.ToString());
    }
}
