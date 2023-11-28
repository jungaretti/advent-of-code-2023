# Advent of Code 2023

My solutions for Advent of Code 2023.

## Usage

```bash
dotnet run --project src/AdventOfCode.CLI -- <day> <part> <inputFile>
```

### Add a new puzzle handler

1. Create a new handler class in `src/PuzzleHandlers` that extends `PuzzleHandler`
2. Construct a new handler and add it to the array of handlers in `Program.cs`
