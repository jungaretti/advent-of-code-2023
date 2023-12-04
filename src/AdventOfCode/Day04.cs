
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
        throw new NotImplementedException();
    }

    class Card
    {
        public int Id { get; }

        public IEnumerable<int> Wins => myNumbers.Where(currentNumber => goalNumbers.Contains(currentNumber));

        private readonly List<int> myNumbers;

        private HashSet<int> goalNumbers { get; }

        public Card(string cardLine)
        {
            var cardRegex = new Regex(@"Card.+(\d+): (.+) \| (.+)");
            var cardMatch = cardRegex.Match(cardLine);

            Id = int.Parse(cardMatch.Groups[1].Value);

            goalNumbers = new HashSet<int>();
            foreach (var number in cardMatch.Groups[2].Value.Split(" "))
            {
                if (string.IsNullOrEmpty(number))
                {
                    continue;
                }

                goalNumbers.Add(int.Parse(number));
            }

            myNumbers = new List<int>();
            foreach (var number in cardMatch.Groups[3].Value.Split())
            {
                if (string.IsNullOrEmpty(number))
                {
                    continue;
                }

                myNumbers.Add(int.Parse(number));
            }
        }

        public int Score()
        {
            var winCount = Wins.Count();

            if (winCount == 0)
            {
                return 0;
            }

            var rawScore = Math.Pow(2, winCount - 1);
            int score = Convert.ToInt32(rawScore);

            return score;
        }
    }
}