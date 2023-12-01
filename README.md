# Advent of Code 2023

My solutions for Advent of Code 2023.

## Usage

```bash
dotnet run --project src/AdventOfCode.CLI -- <inputFile> <day> <part>
```

## Add a New Solution

1. Create a new strategy class in `src/AdventOfCode.Puzzles/PuzzleStrategies` that implements `IPuzzleStrategy`
2. Add the new strategy to `src/AdventOfCode.Puzzles/PuzzleSolver`'s default constructor
