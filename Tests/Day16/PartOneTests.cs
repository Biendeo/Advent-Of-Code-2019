using AdventOfCodeLib.Day16.Part1;
using System.Linq;
using Xunit;

namespace Tests.Day16 {
	public class PartOneTests {
		[Theory]
		[InlineData("12345678", 0, "12345678")]
		[InlineData("12345678", 1, "48226158")]
		[InlineData("12345678", 2, "34040438")]
		[InlineData("12345678", 3, "03415518")]
		[InlineData("12345678", 4, "01029498")]
		[InlineData("80871224585914546619083218645595", 100, "24176176")]
		[InlineData("19617804207202209144916044189917", 100, "73745418")]
		[InlineData("69317163492948606335995924319873", 100, "52432133")]
		public void TestFFT(string inputSignal, int phases, string expectedOutput) {
			Assert.Equal(expectedOutput, Solution.Solve(inputSignal.Select(c => c - '0').ToList(), phases));
		}
	}
}
