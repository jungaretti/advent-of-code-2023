using AdventOfCode;
using System.CommandLine;
using System.Diagnostics;

namespace AdventOfCode.CLI;

class Program
{
    static async Task<int> Main(string[] args)
    {
        var rootCommand = new RootCommand("Advent of Code 2023");

        var inputFileArgument = new Argument<string>(
            name: "inputFile",
            description: "The input file to use."
        );
        rootCommand.AddArgument(inputFileArgument);

        var dayArgument = new Argument<int>(
            name: "day",
            description: "The day to run."
        );
        rootCommand.AddArgument(dayArgument);

        var partArgument = new Argument<int>(
            name: "part",
            description: "The part to run."
        );
        rootCommand.AddArgument(partArgument);

        rootCommand.SetHandler(async (inputFile, day, part) =>
        {
            Console.WriteLine("Advent of Code 2023");
            Console.WriteLine($"Day {day} Part {part}");

            IEnumerable<string> inputLines = await File.ReadAllLinesAsync(inputFile);
            PuzzleSolver solver = new PuzzleSolver();
            Stopwatch stopwatch = new Stopwatch();

            stopwatch.Start();
            var answer = solver.SolvePuzzle(day, part, inputLines);
            stopwatch.Stop();

            var elapsed = stopwatch.Elapsed;
            var elapsedPretty = Math.Round(elapsed.TotalSeconds, 3);

            Console.WriteLine($"Answer: {answer} ({elapsedPretty} seconds)");
        }, inputFileArgument, dayArgument, partArgument);

        return await rootCommand.InvokeAsync(args);
    }
}
