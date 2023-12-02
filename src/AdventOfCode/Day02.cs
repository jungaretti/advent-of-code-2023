using System.Text.RegularExpressions;

namespace AdventOfCode;

class Day02 : IPuzzleDay
{
    private class GameResult
    {
        public int Id { get; }

        public IEnumerable<IDictionary<string, int>> Rounds { get; }

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

        public bool IsPossible(IDictionary<string, int> allowedColorCounts)
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

        public IDictionary<string, int> FewestColorCounts()
        {
            var fewestColorCounts = new Dictionary<string, int>();
            foreach (var roundColorCounts in Rounds)
            {
                foreach (var cubeColor in roundColorCounts.Keys)
                {
                    var existingFewestCount = fewestColorCounts.GetValueOrDefault(cubeColor, 0);
                    var newFewestCount = Math.Max(existingFewestCount, roundColorCounts[cubeColor]);
                    fewestColorCounts[cubeColor] = newFewestCount;
                }
            }

            return fewestColorCounts;
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

        var answer = possibleResults.Select(result => result.Id).Sum();
        return answer.ToString();
    }

    public string PartTwo(IEnumerable<string> inputLines)
    {
        IEnumerable<GameResult> results = inputLines.Select(line => new GameResult(line));

        IEnumerable<IDictionary<string, int>> fewestColorCounts = results.Select(result => result.FewestColorCounts());

        IEnumerable<int> powersOfFewestColorCounts = fewestColorCounts.Select(fewestColorCounts =>
        {
            int power = 1;
            foreach (var colorCount in fewestColorCounts.Values)
            {
                power *= colorCount;
            }

            return power;
        });

        var answer = powersOfFewestColorCounts.Sum();
        return answer.ToString();
    }
}