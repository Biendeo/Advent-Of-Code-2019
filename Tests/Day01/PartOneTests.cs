using AdventOfCodeLib.Day01.Part1;
using Xunit;

namespace Tests.Day01 {
	public class PartOneTests {
		[Theory]
		[InlineData(12, 2)]
		[InlineData(14, 2)]
		[InlineData(1969, 654)]
		[InlineData(100756, 33583)]
		public void TestMass(int massValue, int expectedValue) {
			Assert.Equal(expectedValue, Solution.SolveFromMass(massValue));
		}
	}
}
