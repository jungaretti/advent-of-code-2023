
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
        var goalSeedRanges = ParseSeedRanges(seedStrings);

        throw new Exception();
    }

    private IEnumerable<AlmanacRange> ParseSeedRanges(IEnumerable<string> seedStrings)
    {
        long? seedBase = null;
        long? seedRange = null;

        foreach (string seedString in seedStrings)
        {
            if (seedBase == null)
            {
                seedBase = long.Parse(seedString);
                continue;
            }

            if (seedRange == null)
            {
                seedRange = long.Parse(seedString);
            }

            yield return new AlmanacRange(seedBase.Value, seedRange.Value);

            seedBase = null;
            seedRange = null;
        }
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
        private AlmanacRange sourceRange;
        private AlmanacRange destinationRange;

        public AlmanacEdge(string edgeLine)
        {
            var regex = new Regex(@"(\d+)\s+(\d+)\s+(\d+)");
            var match = regex.Match(edgeLine);

            var sourceBase = long.Parse(match.Groups[2].Value);
            var destinationBase = long.Parse(match.Groups[1].Value);
            var range = long.Parse(match.Groups[3].Value);

            sourceRange = new AlmanacRange(sourceBase, range);
            destinationRange = new AlmanacRange(destinationBase, range);
        }

        public bool SourceContains(long maybeSource) => sourceRange.Contains(maybeSource);

        public bool DestinationContains(long maybeDestination) => destinationRange.Contains(maybeDestination);

        public long? Convert(long source)
        {
            if (!SourceContains(source))
            {
                return null;
            }

            var offset = source - sourceRange.MinValue;
            return destinationRange.MinValue + offset;
        }
    }

    class AlmanacRange
    {
        private long greaterThanOrEqualTo;
        private long lessThan;

        public AlmanacRange(long greaterThanOrEqualTo, long length)
        {
            this.greaterThanOrEqualTo = greaterThanOrEqualTo;
            this.lessThan = greaterThanOrEqualTo + length;
        }

        public long MinValue => greaterThanOrEqualTo;

        public long MaxValue => lessThan - 1;

        public bool Contains(long value) => value >= greaterThanOrEqualTo && value < lessThan;
    }
}