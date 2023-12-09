
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
        IEnumerable<History> histories = inputLines.Select(line => new History(line));
        IEnumerable<int> previousValues = histories.Select(history => history.GetPreviousValue());

        var answer = previousValues.Sum();
        return answer.ToString();
    }

    private class History
    {
        public IEnumerable<int> Values { get; }

        public History(string historyLine)
        {
            Values = historyLine.Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse);
        }

        private History(IEnumerable<int> values)
        {
            Values = values;
        }

        public int GetNextValue()
        {
            if (Values.All(value => value == 0))
            {
                return 0;
            }

            int nextValue = Values.Last() + DeriveHistory().GetNextValue();
            return nextValue;
        }

        public int GetPreviousValue()
        {
            if (Values.All(value => value == 0))
            {
                return 0;
            }

            int previousValue = Values.First() - DeriveHistory().GetPreviousValue();
            return previousValue;
        }

        private History DeriveHistory()
        {
            IEnumerable<int> derivatives = Values.Zip(Values.Skip(1), (last, current) => current - last);
            return new History(derivatives);
        }
    }
}
