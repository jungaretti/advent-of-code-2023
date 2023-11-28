using AdventOfCode.Puzzles;
using System.CommandLine;

namespace AdventOfCode.CLI;

class Program
{
    static async Task<int> Main(string[] args)
    {
        var rootCommand = new RootCommand("Advent of Code 2023");

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

        var inputFileArgument = new Argument<string>(
            name: "inputFile",
            description: "The input file to use."
        );
        rootCommand.AddArgument(inputFileArgument);

        rootCommand.SetHandler(async (day, part, inputFile) =>
        {
            Console.WriteLine("Advent of Code 2023");
            Console.WriteLine($"Solving day {day} part {part}");

            var input = await File.ReadAllLinesAsync(inputFile);

            var solver = new PuzzleSolver();
            var answer = solver.SolvePuzzle(day, part, input);

            Console.WriteLine();
            Console.WriteLine($"Answer: {answer}");
        }, dayArgument, partArgument, inputFileArgument);

        return await rootCommand.InvokeAsync(args);
    }
}
