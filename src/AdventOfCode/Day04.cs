
using System.Text.RegularExpressions;

namespace AdventOfCode;

class Day04 : IPuzzleDay
{
    public int Day => 4;

    public string PartOne(IEnumerable<string> inputLines)
    {
        IEnumerable<Card> cards = inputLines.Select(line => new Card(line));
        IEnumerable<int> scores = cards.Select(card => card.Score());

        var answer = scores.Sum();
        return answer.ToString();
    }

    public string PartTwo(IEnumerable<string> inputLines)
    {
        IEnumerable<Card> originalCards = inputLines.Select(line => new Card(line));

        var stackSizeById = new Dictionary<int, int>();
        foreach (var card in originalCards.Reverse())
        {
            var currentStackSize = 1;

            foreach (var copyId in card.CopyIds())
            {
                currentStackSize += stackSizeById[copyId];
            }

            stackSizeById[card.Id] = currentStackSize;
        }

        return stackSizeById.Values.Sum().ToString();
    }

    class Card
    {
        public int Id { get; }

        public IEnumerable<int> MatchingNumbers { get; }

        private static readonly Regex cardRegex = new Regex(@"Card\s*(\d+): (.+) \| (.+)", RegexOptions.Compiled);

        public Card(string cardLine)
        {
            var match = cardRegex.Match(cardLine);

            Id = int.Parse(match.Groups[1].Value);

            var goalNumbers = match.Groups[2].Value
                .Split(" ", StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse);
            var myNumbers = match.Groups[3].Value
                .Split(" ", StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse);
            MatchingNumbers = myNumbers.Where(number => goalNumbers.Contains(number));
        }

        public int Score()
        {
            var count = MatchingNumbers.Count();

            if (count == 0)
            {
                return 0;
            }

            var score = Math.Pow(2, count - 1);
            return Convert.ToInt32(score);
        }

        public IEnumerable<int> CopyIds()
        {
            var matchingCount = MatchingNumbers.Count();
            var copyIds = new List<int>();

            for (int idOffset = 1; idOffset <= matchingCount; idOffset++)
            {
                var copyId = Id + idOffset;
                copyIds.Add(copyId);
            }

            return copyIds;
        }
    }
}