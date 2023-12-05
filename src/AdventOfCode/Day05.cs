
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
        throw new NotImplementedException();
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
            var soil = seedToSoil.Convert(seed);
            var fertilizer = soilToFertilizer.Convert(soil);
            var water = fertilizerToWater.Convert(fertilizer);
            var light = waterToLight.Convert(water);
            var temperature = lightToTemperature.Convert(light);
            var humidity = temperatureToHumidity.Convert(temperature);
            var location = humidityToLocation.Convert(humidity);

            return location;
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

        public AlmanacMap(IEnumerable<string> edgeLines)
        {
            edges = edgeLines.Select(line => new AlmanacEdge(line));
        }

        public long Convert(long source)
        {
            var edge = edges.SingleOrDefault(edge => edge.SourceContains(source));
            var conversion = edge?.Convert(source);

            return conversion ?? source;
        }
    }

    class AlmanacEdge
    {
        private long sourceBase;
        private long sourceLessThan;
        private long destinationBase;
        private long destinationLessThan;

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

        public long? Convert(long source)
        {
            if (!SourceContains(source))
            {
                return null;
            }

            var offset = source - sourceBase;
            return destinationBase + offset;
        }
    }
}