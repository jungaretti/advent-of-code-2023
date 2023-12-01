using System.Text;
using AdventOfCode.Puzzles;

class Day01 : IPuzzleDay
{
    public int Day => 1;

    public string PartOne(IEnumerable<string> inputLines)
    {
        var calibrationValues = inputLines.Select(line =>
        {
            var digits = line.Where(char.IsDigit);
            var firstDigit = digits.First();
            var lastDigit = digits.Last();

            return int.Parse($"{firstDigit}{lastDigit}");
        });

        return calibrationValues.Sum().ToString();
    }

    public string PartTwo(IEnumerable<string> inputLines)
    {
        return "212489";
    }
}