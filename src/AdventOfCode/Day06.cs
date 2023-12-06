namespace AdventOfCode;

class Day06 : IPuzzleDay
{
    public int Day => 6;

    public string PartOne(IEnumerable<string> inputLines)
    {
        IEnumerable<Race> races = Race.ParseRaces(inputLines);

        IEnumerable<int> winningButtonHoldTimesCount = races.Select(race => race.GetWinningButtonHoldTimes().Count());

        int answer = winningButtonHoldTimesCount.Aggregate(1, (acc, count) => acc * count);
        return answer.ToString();
    }

    public string PartTwo(IEnumerable<string> inputLines)
    {
        IEnumerable<string> cleanedInputLines = inputLines.Select(line => line.Replace(" ", ""));
        IEnumerable<Race> races = Race.ParseRaces(cleanedInputLines);

        IEnumerable<long> winningButtonHoldTimesCount = races.Select(race => race.GetWinningButtonHoldTimes().LongCount());

        long answer = winningButtonHoldTimesCount.Aggregate<long, long>(1, (acc, count) => acc * count);
        return answer.ToString();
    }

    class Race
    {
        public long Time { get; }
        public long RecordDistance { get; }

        public Race(long time, long distance)
        {
            Time = time;
            RecordDistance = distance;
        }

        static public IEnumerable<Race> ParseRaces(IEnumerable<string> inputLines)
        {
            const string TIME_PREFIX = "Time:";
            const string DISTANCE_PREFIX = "Distance:";

            string timeLine = inputLines.Single(line => line.StartsWith(TIME_PREFIX));
            IEnumerable<string> times = timeLine.Substring(TIME_PREFIX.Length).Split(" ", StringSplitOptions.RemoveEmptyEntries);

            string distanceLine = inputLines.Single(line => line.StartsWith(DISTANCE_PREFIX));
            IEnumerable<string> distances = distanceLine.Substring(DISTANCE_PREFIX.Length).Split(" ", StringSplitOptions.RemoveEmptyEntries);

            return times.Zip(distances, (time, distance) => new Race(long.Parse(time), long.Parse(distance)));
        }

        public IEnumerable<long> GetWinningButtonHoldTimes(long startingSpeed = 0, long acceleration = 1)
        {
            for (long holdButtonForTime = 0; holdButtonForTime < Time; holdButtonForTime++)
            {
                long boatSpeed = startingSpeed + (acceleration * holdButtonForTime);
                long remainingTime = Time - holdButtonForTime;

                long distance = boatSpeed * remainingTime;

                if (distance > RecordDistance)
                {
                    yield return distance;
                }
            }
        }
    }
}