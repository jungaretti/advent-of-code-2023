using System.Text.RegularExpressions;

namespace AdventOfCode;

class Day02 : IPuzzleDay
{
    private class GameResult
    {
        public int Id { get; }

        public IEnumerable<IReadOnlyDictionary<string, int>> Rounds { get; }

        public GameResult(string gameLine)
        {
            var gameRegex = new Regex(@"Game (\d+): (.+)");
            var gameMatch = gameRegex.Match(gameLine);

            string gameId = gameMatch.Groups[1].Value;
            Id = int.Parse(gameId);

            string gameText = gameMatch.Groups[2].Value;
            Rounds = gameText.Split("; ").Select(roundText =>
            {
                var results = new Dictionary<string, int>();

                foreach (var cubeText in roundText.Split(", "))
                {
                    var cubeRegex = new Regex(@"(\d+) (\w+)");
                    var cubeMatch = cubeRegex.Match(cubeText);

                    int cubeCount = int.Parse(cubeMatch.Groups[1].Value);
                    string cubeColor = cubeMatch.Groups[2].Value;

                    results.Add(cubeColor, cubeCount);
                }

                return results;
            });
        }

        public bool IsPossible(IReadOnlyDictionary<string, int> allowedColorCounts)
        {
            foreach (var round in Rounds)
            {
                foreach (var color in round.Keys)
                {
                    if (!allowedColorCounts.ContainsKey(color))
                    {
                        return false;
                    }

                    if (round[color] > allowedColorCounts[color])
                    {
                        return false;
                    }
                }
            }

            return true;
        }
    }

    public int Day => 2;

    public string PartOne(IEnumerable<string> inputLines)
    {
        IEnumerable<GameResult> results = inputLines.Select(line => new GameResult(line));

        var allowedColorCounts = new Dictionary<string, int>
        {
            { "red", 12 },
            { "green", 13 },
            { "blue", 14 },
        };
        IEnumerable<GameResult> possibleResults = results.Where(result => result.IsPossible(allowedColorCounts));

        return possibleResults.Select(result => result.Id).Sum().ToString();
    }

    public string PartTwo(IEnumerable<string> inputLines)
    {
        throw new NotImplementedException();
    }
}