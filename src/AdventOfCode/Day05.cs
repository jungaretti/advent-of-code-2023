
using System.Data;
using System.Text.RegularExpressions;

namespace AdventOfCode;

class Day05 : IPuzzleDay
{
    public int Day => 5;

    public string PartOne(IEnumerable<string> inputLines)
    {
        var seedsLine = inputLines.First();
        var seedStrings = seedsLine.Remove(0, "seeds: ".Length).Split(" ", StringSplitOptions.RemoveEmptyEntries);
        var goalSeeds = seedStrings.Select(long.Parse);

        var almanac = new Almanac(inputLines);
        var goalLocations = goalSeeds.Select(almanac.ConvertSeedToLocation);

        var answer = goalLocations.Min();
        return answer.ToString();
    }

    public string PartTwo(IEnumerable<string> inputLines)
    {
        var seedsLine = inputLines.First();
        var seedStrings = seedsLine.Remove(0, "seeds: ".Length).Split(" ", StringSplitOptions.RemoveEmptyEntries);

        IEnumerable<long> goalSeeds = new List<long>();
        long? seedBase = null;
        long? seedLessThan = null;
        foreach (string token in seedStrings)
        {
            if (seedBase is null)
            {
                seedBase = long.Parse(token);
                continue;
            }

            if (seedLessThan is null)
            {
                seedLessThan = seedBase + long.Parse(token);
                continue;
            }

            // Ready to add a range to goalSeeds
            while (seedBase < seedLessThan)
            {
                goalSeeds = goalSeeds.Append(seedBase.Value);
                seedBase++;
            }

            seedBase = null;
            seedLessThan = null;
        }

        var almanac = new Almanac(inputLines);
        var goalLocations = goalSeeds.Select(almanac.ConvertSeedToLocation);

        var answer = goalLocations.Min();
        return answer.ToString();
    }

    class Almanac
    {
        private AlmanacMap seedToSoil;
        private AlmanacMap soilToFertilizer;
        private AlmanacMap fertilizerToWater;
        private AlmanacMap waterToLight;
        private AlmanacMap lightToTemperature;
        private AlmanacMap temperatureToHumidity;
        private AlmanacMap humidityToLocation;

        public Almanac(IEnumerable<string> inputLines)
        {
            var seedToSoilLines = GetMapLines(inputLines, "seed-to-soil");
            var soilToFertilizerLines = GetMapLines(inputLines, "soil-to-fertilizer");
            var fertilizerToWaterLines = GetMapLines(inputLines, "fertilizer-to-water");
            var waterToLightLines = GetMapLines(inputLines, "water-to-light");
            var lightToTemperatureLines = GetMapLines(inputLines, "light-to-temperature");
            var temperatureToHumidityLines = GetMapLines(inputLines, "temperature-to-humidity");
            var humidityToLocationLines = GetMapLines(inputLines, "humidity-to-location");

            seedToSoil = new AlmanacMap(seedToSoilLines);
            soilToFertilizer = new AlmanacMap(soilToFertilizerLines);
            fertilizerToWater = new AlmanacMap(fertilizerToWaterLines);
            waterToLight = new AlmanacMap(waterToLightLines);
            lightToTemperature = new AlmanacMap(lightToTemperatureLines);
            temperatureToHumidity = new AlmanacMap(temperatureToHumidityLines);
            humidityToLocation = new AlmanacMap(humidityToLocationLines);
        }

        public long ConvertSeedToLocation(long seed)
        {
            var soil = seedToSoil.DestinationOf(seed);
            var fertilizer = soilToFertilizer.DestinationOf(soil);
            var water = fertilizerToWater.DestinationOf(fertilizer);
            var light = waterToLight.DestinationOf(water);
            var temperature = lightToTemperature.DestinationOf(light);
            var humidity = temperatureToHumidity.DestinationOf(temperature);
            var location = humidityToLocation.DestinationOf(humidity);

            return location;
        }

        public long SeedAtMinLocation()
        {
            var minLocation = humidityToLocation.MinDestination;
            var humidity = humidityToLocation.SourceOf(minLocation);
            var temperature = temperatureToHumidity.SourceOf(humidity);
            var light = lightToTemperature.SourceOf(temperature);
            var water = waterToLight.SourceOf(light);
            var fertilizer = fertilizerToWater.SourceOf(water);
            var soil = soilToFertilizer.SourceOf(fertilizer);
            var seed = seedToSoil.SourceOf(soil);

            return seed;
        }

        private IEnumerable<string> GetMapLines(IEnumerable<string> inputLines, string mapName)
        {
            return inputLines
                .SkipWhile(line => !line.StartsWith(mapName))
                .TakeWhile(line => !string.IsNullOrEmpty(line))
                .Skip(1);
        }
    }

    class AlmanacMap
    {
        private IEnumerable<AlmanacEdge> edges;

        public long MinDestination => edges.Min(edge => edge.MinDestination);

        public AlmanacMap(IEnumerable<string> edgeLines)
        {
            edges = edgeLines.Select(line => new AlmanacEdge(line));
        }

        public long DestinationOf(long source)
        {
            var edge = edges.SingleOrDefault(edge => edge.SourceContains(source));
            var destination = edge?.DestinationOf(source);

            return destination ?? source;
        }

        public long SourceOf(long destination)
        {
            var edge = edges.Single(edge => edge.DestinationContains(destination));
            long source = edge.SourceOf(destination);

            return source;
        }
    }

    class AlmanacEdge
    {
        private long sourceBase;
        private long sourceLessThan;
        private long destinationBase;
        private long destinationLessThan;

        public long MinDestination => destinationBase;

        public AlmanacEdge(string edgeLine)
        {
            var regex = new Regex(@"(\d+)\s+(\d+)\s+(\d+)");
            var match = regex.Match(edgeLine);

            sourceBase = long.Parse(match.Groups[2].Value);
            destinationBase = long.Parse(match.Groups[1].Value);

            var range = long.Parse(match.Groups[3].Value);
            sourceLessThan = sourceBase + range;
            destinationLessThan = destinationBase + range;
        }

        public bool SourceContains(long maybeSource) => maybeSource >= sourceBase && maybeSource < sourceLessThan;

        public bool DestinationContains(long maybeDestination) => maybeDestination >= destinationBase && maybeDestination < destinationLessThan;

        public long? DestinationOf(long source)
        {
            if (!SourceContains(source))
            {
                return null;
            }

            var offset = source - sourceBase;
            return destinationBase + offset;
        }

        public long SourceOf(long destination)
        {
            var offset = destination - destinationBase;
            return sourceBase + offset;
        }
    }
}