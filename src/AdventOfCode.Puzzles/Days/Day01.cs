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
        var inputLinesWithDigits = inputLines.Select(line =>
        {
            string newLine = line.Replace("one", "o1e");
            newLine = newLine.Replace("two", "t2o");
            newLine = newLine.Replace("three", "t3e");
            newLine = newLine.Replace("four", "f4r");
            newLine = newLine.Replace("five", "f5e");
            newLine = newLine.Replace("six", "s6x");
            newLine = newLine.Replace("seven", "s7n");
            newLine = newLine.Replace("eight", "e8t");
            newLine = newLine.Replace("nine", "n9e");

            return newLine;
        });

        return PartOne(inputLinesWithDigits);
    }
}