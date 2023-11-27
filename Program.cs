using System.CommandLine;

namespace advent_of_code_2023;

class Program
{
    private static readonly IEnumerable<PuzzleHandler> handlers = new PuzzleHandler[]
    {
        new Day1Part1(File.ReadAllLines("input/day1.txt"))
    };

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

        rootCommand.SetHandler(RunPuzzle, dayArgument, partArgument);

        return await rootCommand.InvokeAsync(args);
    }

    private static async Task RunPuzzle(int day, int part)
    {
        Console.WriteLine($"Day: {day}");
        Console.WriteLine($"Part: {part}");

        var solver = new PuzzleSolver(handlers);

        var result = solver.Solve(day, part);

        Console.WriteLine($"Result: {await result}");
    }
}
