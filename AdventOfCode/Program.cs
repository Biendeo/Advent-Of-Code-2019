using System;
using System.IO;

namespace AdventOfCode {
	class Program {
		static void Main() {
			Console.WriteLine("Day 01 - Part 1");
			Console.WriteLine(AdventOfCodeLib.Day01.Part1.Solution.SolveFromInputFile(Path.Combine("input", "01.txt")));
			Console.WriteLine();

			Console.WriteLine("Day 01 - Part 2");
			Console.WriteLine(AdventOfCodeLib.Day01.Part2.Solution.SolveFromInputFile(Path.Combine("input", "01.txt")));
			Console.WriteLine();

			Console.WriteLine("Day 02 - Part 1");
			Console.WriteLine(AdventOfCodeLib.Day02.Part1.Solution.SolveFromInputFile(Path.Combine("input", "02.txt")));
			Console.WriteLine();

			Console.WriteLine("Day 02 - Part 2");
			Console.WriteLine(AdventOfCodeLib.Day02.Part2.Solution.SolveFromInputFile(Path.Combine("input", "02.txt")));
			Console.WriteLine();

			Console.WriteLine("Day 03 - Part 1");
			Console.WriteLine(AdventOfCodeLib.Day03.Part1.Solution.SolveFromInputFile(Path.Combine("input", "03.txt")));
			Console.WriteLine();

			Console.WriteLine("Day 03 - Part 2");
			Console.WriteLine(AdventOfCodeLib.Day03.Part2.Solution.SolveFromInputFile(Path.Combine("input", "03.txt")));
			Console.WriteLine();

			Console.WriteLine("Day 04 - Part 1");
			Console.WriteLine(AdventOfCodeLib.Day04.Part1.Solution.SolveFromInputFile(Path.Combine("input", "04.txt")));
			Console.WriteLine();

			Console.WriteLine("Day 04 - Part 2");
			Console.WriteLine(AdventOfCodeLib.Day04.Part2.Solution.SolveFromInputFile(Path.Combine("input", "04.txt")));
			Console.WriteLine();

			Console.WriteLine("Day 05 - Part 1");
			Console.WriteLine(AdventOfCodeLib.Day05.Part1.Solution.SolveFromInputFile(Path.Combine("input", "05.txt")));
			Console.WriteLine();

			Console.WriteLine("Day 05 - Part 2");
			Console.WriteLine(AdventOfCodeLib.Day05.Part2.Solution.SolveFromInputFile(Path.Combine("input", "05.txt")));
			Console.WriteLine();
		}
	}
}
