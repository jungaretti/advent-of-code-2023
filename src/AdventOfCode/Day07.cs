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
        IEnumerable<Hand> hands = inputLines.Select(line => new Hand(line, parseJokers: true));

        IEnumerable<Hand> sortedHands = hands.Order();
        IEnumerable<int> winnings = sortedHands.Zip(Enumerable.Range(1, int.MaxValue), (hand, rank) => hand.Bid * rank);
        int answer = winnings.Sum();

        return answer.ToString();
    }

    class Hand : IComparable<Hand>
    {
        public Card[] Cards { get; }

        public int Bid { get; }

        public Hand(string handLine, bool parseJokers = false)
        {
            var regex = new Regex(@"(\S+)\s+(\d+)");
            var match = regex.Match(handLine);

            var cardLabels = match.Groups[1].Value.ToCharArray();
            var bid = int.Parse(match.Groups[2].Value);

            Cards = cardLabels.Select(label => new Card(label, parseJokers)).ToArray();
            Bid = bid;
        }

        private HandType GetHandType()
        {
            var cardCounts = new Dictionary<CardType, int>();
            foreach (Card card in Cards)
            {
                var newCount = cardCounts.GetValueOrDefault(card.Type, 0) + 1;
                cardCounts[card.Type] = newCount;
            }

            if (cardCounts.TryGetValue(CardType.Joker, out int jokerCount))
            {
                if (jokerCount == 5)
                {
                    return HandType.FiveOfAKind;
                }

                CardType mostCommonNonJoker = cardCounts.Where(pair => pair.Key != CardType.Joker).MaxBy(pair => pair.Value).Key;
                cardCounts[mostCommonNonJoker] += jokerCount;
                cardCounts[CardType.Joker] = 0;
            }

            if (cardCounts.Values.Any(count => count == 5))
            {
                return HandType.FiveOfAKind;
            }
            if (cardCounts.Values.Any(count => count == 4))
            {
                return HandType.FourOfAKind;
            }
            if (cardCounts.Values.Any(count => count == 3) && cardCounts.Values.Any(count => count == 2))
            {
                return HandType.FullHouse;
            }
            if (cardCounts.Values.Any(count => count == 3))
            {
                return HandType.ThreeOfAKind;
            }
            if (cardCounts.Values.Where(count => count == 2).Count() == 2)
            {
                return HandType.TwoPair;
            }
            if (cardCounts.Values.Where(count => count == 2).Count() == 1)
            {
                return HandType.OnePair;
            }

            return HandType.HighCard;
        }

        public int CompareTo(Hand? other)
        {
            int typeCompare = GetHandType().CompareTo(other?.GetHandType());
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

    enum HandType
    {
        FiveOfAKind = 500,
        FourOfAKind = 400,
        FullHouse = 350,
        ThreeOfAKind = 300,
        TwoPair = 200,
        OnePair = 100,
        HighCard = 1,
    }

    class Card : IComparable<Card>
    {
        public CardType Type { get; }

        public Card(char label, bool parseJokers = false)
        {
            Type = GetType(label, parseJokers);
        }

        private CardType GetType(char label, bool parseJokers = false)
        {
            return label switch
            {
                'A' => CardType.Ace,
                'K' => CardType.King,
                'Q' => CardType.Queen,
                'J' => parseJokers ? CardType.Joker : CardType.Jack,
                'T' => CardType.Ten,
                '9' => CardType.Nine,
                '8' => CardType.Eight,
                '7' => CardType.Seven,
                '6' => CardType.Six,
                '5' => CardType.Five,
                '4' => CardType.Four,
                '3' => CardType.Three,
                '2' => CardType.Two,
                _ => throw new Exception("Invalid card label"),
            };
        }

        public int CompareTo(Card? other)
        {
            return Type.CompareTo(other?.Type);
        }
    }

    enum CardType
    {
        Ace = 100,
        King = 90,
        Queen = 80,
        Jack = 70,
        Ten = 60,
        Nine = 50,
        Eight = 40,
        Seven = 30,
        Six = 20,
        Five = 10,
        Four = 4,
        Three = 3,
        Two = 2,
        Joker = 1,
    }
}