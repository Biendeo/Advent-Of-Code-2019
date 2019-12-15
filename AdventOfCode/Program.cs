using System;
using System.Diagnostics;
using System.IO;

namespace AdventOfCode {
	class Program {
		static void Main() {
			RunSolution("Day 01 - Part 1", () => AdventOfCodeLib.Day01.Part1.Solution.SolveFromInputFile(Path.Combine("input", "01.txt")));
			RunSolution("Day 01 - Part 2", () => AdventOfCodeLib.Day01.Part2.Solution.SolveFromInputFile(Path.Combine("input", "01.txt")));
			RunSolution("Day 02 - Part 1", () => AdventOfCodeLib.Day02.Part1.Solution.SolveFromInputFile(Path.Combine("input", "02.txt")));
			RunSolution("Day 02 - Part 2", () => AdventOfCodeLib.Day02.Part2.Solution.SolveFromInputFile(Path.Combine("input", "02.txt")));
			RunSolution("Day 03 - Part 1", () => AdventOfCodeLib.Day03.Part1.Solution.SolveFromInputFile(Path.Combine("input", "03.txt")));
			RunSolution("Day 03 - Part 2", () => AdventOfCodeLib.Day03.Part2.Solution.SolveFromInputFile(Path.Combine("input", "03.txt")));
			RunSolution("Day 04 - Part 1", () => AdventOfCodeLib.Day04.Part1.Solution.SolveFromInputFile(Path.Combine("input", "04.txt")));
			RunSolution("Day 04 - Part 2", () => AdventOfCodeLib.Day04.Part2.Solution.SolveFromInputFile(Path.Combine("input", "04.txt")));
			RunSolution("Day 05 - Part 1", () => AdventOfCodeLib.Day05.Part1.Solution.SolveFromInputFile(Path.Combine("input", "05.txt")));
			RunSolution("Day 05 - Part 2", () => AdventOfCodeLib.Day05.Part2.Solution.SolveFromInputFile(Path.Combine("input", "05.txt")));
			RunSolution("Day 06 - Part 1", () => AdventOfCodeLib.Day06.Part1.Solution.SolveFromInputFile(Path.Combine("input", "06.txt")));
			RunSolution("Day 06 - Part 2", () => AdventOfCodeLib.Day06.Part2.Solution.SolveFromInputFile(Path.Combine("input", "06.txt")));
			RunSolution("Day 07 - Part 1", () => AdventOfCodeLib.Day07.Part1.Solution.SolveFromInputFile(Path.Combine("input", "07.txt")));
			RunSolution("Day 07 - Part 2", () => AdventOfCodeLib.Day07.Part2.Solution.SolveFromInputFile(Path.Combine("input", "07.txt")));
			RunSolution("Day 08 - Part 1", () => AdventOfCodeLib.Day08.Part1.Solution.SolveFromInputFile(Path.Combine("input", "08.txt")));
			RunSolution("Day 08 - Part 2", () => AdventOfCodeLib.Day08.Part2.Solution.SolveFromInputFile(Path.Combine("input", "08.txt")));
			RunSolution("Day 09 - Part 1", () => AdventOfCodeLib.Day09.Part1.Solution.SolveFromInputFile(Path.Combine("input", "09.txt")));
			RunSolution("Day 09 - Part 2", () => AdventOfCodeLib.Day09.Part2.Solution.SolveFromInputFile(Path.Combine("input", "09.txt")));
			RunSolution("Day 10 - Part 1", () => AdventOfCodeLib.Day10.Part1.Solution.SolveFromInputFile(Path.Combine("input", "10.txt")));
			RunSolution("Day 10 - Part 2", () => AdventOfCodeLib.Day10.Part2.Solution.SolveFromInputFile(Path.Combine("input", "10.txt")));
			RunSolution("Day 11 - Part 1", () => AdventOfCodeLib.Day11.Part1.Solution.SolveFromInputFile(Path.Combine("input", "11.txt")));
			RunSolution("Day 11 - Part 2", () => AdventOfCodeLib.Day11.Part2.Solution.SolveFromInputFile(Path.Combine("input", "11.txt")));
			RunSolution("Day 12 - Part 1", () => AdventOfCodeLib.Day12.Part1.Solution.SolveFromInputFile(Path.Combine("input", "12.txt")));
			RunSolution("Day 12 - Part 2", () => AdventOfCodeLib.Day12.Part2.Solution.SolveFromInputFile(Path.Combine("input", "12.txt")));
			RunSolution("Day 13 - Part 1", () => AdventOfCodeLib.Day13.Part1.Solution.SolveFromInputFile(Path.Combine("input", "13.txt")));
			RunSolution("Day 13 - Part 2", () => AdventOfCodeLib.Day13.Part2.Solution.SolveFromInputFile(Path.Combine("input", "13.txt")));
			RunSolution("Day 14 - Part 1", () => AdventOfCodeLib.Day14.Part1.Solution.SolveFromInputFile(Path.Combine("input", "14.txt")));
			RunSolution("Day 14 - Part 2", () => AdventOfCodeLib.Day14.Part2.Solution.SolveFromInputFile(Path.Combine("input", "14.txt")));
			RunSolution("Day 15 - Part 1", () => AdventOfCodeLib.Day15.Part1.Solution.SolveFromInputFile(Path.Combine("input", "15.txt")));
			RunSolution("Day 15 - Part 2", () => AdventOfCodeLib.Day15.Part2.Solution.SolveFromInputFile(Path.Combine("input", "15.txt")));
		}

		private static void RunSolution<T>(string solutionName, Func<T> solution) {
			Console.WriteLine(solutionName);
			var stopwatch = new Stopwatch();
			stopwatch.Start();
			var result = solution();
			stopwatch.Stop();
			Console.WriteLine(result);
			Console.WriteLine($"Completed in {stopwatch.ElapsedMilliseconds}ms");
			Console.WriteLine();
		}
	}
}
