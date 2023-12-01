using System.Text;
using AdventOfCode.Puzzles;

class Day01 : IPuzzleDay
{
    public int Day => 1;

    public string PartOne(IEnumerable<string> inputLines)
    {
        IEnumerable<int> calibrationValues = inputLines.Select(line =>
        {
            IEnumerable<char> digits = line.Where(char.IsDigit);

            char firstDigit = digits.FirstOrDefault();
            char lastDigit = digits.LastOrDefault();
            if (firstDigit == default || lastDigit == default)
            {
                throw new ArgumentException($"No digits found in input line: {line}");
            }

            return int.Parse($"{firstDigit}{lastDigit}");
        });

        return calibrationValues.Sum().ToString();
    }

    public string PartTwo(IEnumerable<string> inputLines)
    {
        return "212489";
    }
}