
namespace AdventOfCode;

class Day09 : IPuzzleDay
{
    public int Day => 9;

    public string PartOne(IEnumerable<string> inputLines)
    {
        IEnumerable<History> histories = inputLines.Select(line => new History(line));
        IEnumerable<int> nextValues = histories.Select(history => history.GetNextValue());

        var answer = nextValues.Sum();
        return answer.ToString();
    }

    public string PartTwo(IEnumerable<string> inputLines)
    {
        return "0";
    }

    private class History
    {
        public IEnumerable<int> Values { get; }

        public History(string historyLine)
        {
            Values = historyLine.Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse);
        }

        public int GetNextValue()
        {
            var nextValue = DeriveNextValue(Values);
            return nextValue;
        }

        private int DeriveNextValue(IEnumerable<int> values)
        {
            if (values.All(value => value == 0))
            {
                return 0;
            }

            IEnumerable<int> derivatives = values.Zip(values.Skip(1), (last, current) => current - last);
            int nextValue = values.Last() + DeriveNextValue(derivatives);
            return nextValue;
        }
    }
}
