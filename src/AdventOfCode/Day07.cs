using System.Reflection.Emit;
using System.Text.RegularExpressions;

namespace AdventOfCode;

class Day07 : IPuzzleDay
{
    public int Day => 7;

    public string PartOne(IEnumerable<string> inputLines)
    {
        IEnumerable<Hand> hands = inputLines.Select(line => new Hand(line));

        IEnumerable<Hand> sortedHands = hands.Order();
        IEnumerable<int> winnings = sortedHands.Zip(Enumerable.Range(1, int.MaxValue), (hand, rank) => hand.Bid * rank);
        int answer = winnings.Sum();

        return answer.ToString();
    }

    public string PartTwo(IEnumerable<string> inputLines)
    {
        throw new NotImplementedException();
    }

    class Hand : IComparable<Hand>
    {
        public Card[] Cards { get; }

        public int Bid { get; }

        public Hand(string handLine)
        {
            var regex = new Regex(@"(\S+)\s+(\d+)");
            var match = regex.Match(handLine);

            var cardLabels = match.Groups[1].Value.ToCharArray();
            var bid = int.Parse(match.Groups[2].Value);

            Cards = cardLabels.Select(label => new Card(label)).ToArray();
            Bid = bid;
        }

        private int GetValue()
        {
            var cardCounts = new Dictionary<char, int>();
            foreach (Card card in Cards)
            {
                var newCount = cardCounts.GetValueOrDefault(card.Label, 0) + 1;
                cardCounts[card.Label] = newCount;
            }

            if (cardCounts.Values.Any(count => count == 5))
            {
                // Five of a kind
                return 500;
            }
            if (cardCounts.Values.Any(count => count == 4))
            {
                // Four of a kind
                return 400;
            }
            if (cardCounts.Values.Any(count => count == 3) && cardCounts.Values.Any(count => count == 2))
            {
                // Full house
                return 350;
            }
            if (cardCounts.Values.Any(count => count == 3))
            {
                // Three of a kind
                return 300;
            }
            if (cardCounts.Values.Where(count => count == 2).Count() == 2)
            {
                // Two pair
                return 200;
            }
            if (cardCounts.Values.Where(count => count == 2).Count() == 1)
            {
                // One pair
                return 100;
            }

            // High card
            return 1;
        }

        public int CompareTo(Hand? other)
        {
            int typeCompare = GetValue().CompareTo(other?.GetValue());
            if (typeCompare != 0)
            {
                return typeCompare;
            }

            for (int cardIndex = 0; cardIndex < Cards.Count(); cardIndex++)
            {
                int cardCompare = Cards[cardIndex].CompareTo(other?.Cards[cardIndex]);
                if (cardCompare != 0)
                {
                    return cardCompare;
                }
            }

            return 0;
        }
    }

    class Card : IComparable<Card>
    {
        public char Label { get; }

        public Card(char label)
        {
            Label = label;
        }

        private int GetValue()
        {
            return Label switch
            {
                'A' => 100,
                'K' => 90,
                'Q' => 80,
                'J' => 70,
                'T' => 60,
                _ => int.Parse(Label.ToString()),
            };
        }

        public int CompareTo(Card? other)
        {
            return GetValue().CompareTo(other?.GetValue());
        }
    }
}