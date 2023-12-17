namespace AdventOfCode;

class Day15 : IPuzzleDay
{
    public int Day => 15;

    public string PartOne(IEnumerable<string> inputLines)
    {
        IEnumerable<string> steps = inputLines.First().Split(',');
        IEnumerable<int> hashes = steps.Select(GetHash);
        int answer = hashes.Sum();

        return answer.ToString();
    }

    public string PartTwo(IEnumerable<string> inputLines)
    {
        throw new NotImplementedException();
    }

    private int GetHash(string input)
    {
        int value = 0;
        foreach (char character in input)
        {
            value += character;
            value *= 17;
            value %= 256;
        }

        return value;
    }
}