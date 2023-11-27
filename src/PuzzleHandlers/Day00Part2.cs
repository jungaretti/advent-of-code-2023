

using System.Collections.Immutable;

namespace advent_of_code_2023;

public class Day00Part2 : PuzzleHandler
{
    public override int Day => 0;

    public override int Part => 2;

    public Day00Part2(IEnumerable<string> input) : base(input)
    {
    }

    public override string Solve()
    {
        var allCalories = new PriorityQueue<int, int>();
        var currentCalories = 0;

        foreach (var line in Input)
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

        return answer.ToString();
    }
}
