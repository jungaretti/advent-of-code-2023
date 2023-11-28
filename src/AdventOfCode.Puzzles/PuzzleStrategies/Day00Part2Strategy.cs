namespace AdventOfCode.Puzzles;

class Day00Part2Strategy : IPuzzleStrategy
{
    public int Day => 0;

    public int Part => 2;

    public string SolvePuzzle(IEnumerable<string> inputLines)
    {
        var allCalories = new PriorityQueue<int, int>();
        var currentCalories = 0;

        foreach (var line in inputLines)
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