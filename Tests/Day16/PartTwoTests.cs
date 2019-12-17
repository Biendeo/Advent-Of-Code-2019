using AdventOfCodeLib.Day16.Part2;
using System.Linq;
using Xunit;

namespace Tests.Day16 {
	public class PartTwoTests {
		[Theory]
		[InlineData("03036732577212944063491565474664", 10000, 100, "84462026")]
		[InlineData("02935109699940807407585447034323", 10000, 100, "78725270")]
		[InlineData("03081770884921959731165446850517", 10000, 100, "53553731")]
		public void TestFFT(string inputSignal, int inputSignalRepeats, int phases, string expectedOutput) {
			Assert.Equal(expectedOutput, Solution.Solve(inputSignal.Select(c => c - '0').ToList(), inputSignalRepeats, phases).Substring(0, 8));
		}
	}
}
