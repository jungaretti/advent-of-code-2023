
using System.Data.SqlTypes;

namespace AdventOfCode;

class Day06 : IPuzzleDay
{
    public int Day => 6;

    public string PartOne(IEnumerable<string> inputLines)
    {
        IEnumerable<Race> races = Race.ParseRaces(inputLines);

        var answer = races.Count();
        return answer.ToString();
    }

    public string PartTwo(IEnumerable<string> inputLines)
    {
        throw new NotImplementedException();
    }

    class Race
    {
        public int Time { get; }
        public int Distance { get; }

        public Race(int time, int distance)
        {
            Time = time;
            Distance = distance;
        }

        static public IEnumerable<Race> ParseRaces(IEnumerable<string> inputLines)
        {
            const string TIME_PREFIX = "Time:";
            const string DISTANCE_PREFIX = "Distance:";

            string timeLine = inputLines.Single(line => line.StartsWith(TIME_PREFIX));
            IEnumerable<string> times = timeLine.Substring(TIME_PREFIX.Length).Split(" ", StringSplitOptions.RemoveEmptyEntries);

            string distanceLine = inputLines.Single(line => line.StartsWith(DISTANCE_PREFIX));
            IEnumerable<string> distances = distanceLine.Substring(DISTANCE_PREFIX.Length).Split(" ", StringSplitOptions.RemoveEmptyEntries);

            return times.Zip(distances, (time, distance) => new Race(int.Parse(time), int.Parse(distance)));
        }
    }
}