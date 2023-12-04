
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
        IEnumerable<Card> cards = inputLines.Select(line => new Card(line));

        // Each card has a stack size. Its stack size is equivalent to the number of copies that it awards, plus one.
        // Some card copies grant even more copies. For example, if a card awards 2 copies, and each of those copies
        // awards 2 more copies, then the stack size of the original card is 7 (1 original, 2 copies, 4 subcopies).
        Dictionary<int, int> cardIdToStackSize = new Dictionary<int, int>();

        foreach (Card card in cards.Reverse())
        {
            var subStackSizes = card.CopyIds().Select(copyId => cardIdToStackSize[copyId]);
            var stackSize = subStackSizes.Sum() + 1;
            cardIdToStackSize[card.Id] = stackSize;
        }

        return cardIdToStackSize.Values.Sum().ToString();
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