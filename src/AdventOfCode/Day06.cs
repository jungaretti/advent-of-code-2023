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
        throw new NotImplementedException();
    }

    class Race
    {
        public int Time { get; }
        public int RecordDistance { get; }

        public Race(int time, int distance)
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

            return times.Zip(distances, (time, distance) => new Race(int.Parse(time), int.Parse(distance)));
        }

        public IEnumerable<int> GetWinningButtonHoldTimes(int startingSpeed = 0, int acceleration = 1)
        {
            for (int holdButtonForTime = 0; holdButtonForTime < Time; holdButtonForTime++)
            {
                int boatSpeed = startingSpeed + (acceleration * holdButtonForTime);
                int remainingTime = Time - holdButtonForTime;

                int distance = boatSpeed * remainingTime;

                if (distance > RecordDistance)
                {
                    yield return distance;
                }
            }
        }
    }
}