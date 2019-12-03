using AdventOfCodeLib.Day01.Part2;
using Xunit;

namespace Tests.Day01 {
	public class PartTwoTests {
		[Theory]
		[InlineData(14, 2)]
		[InlineData(1969, 966)]
		[InlineData(100756, 50346)]
		public void TestMass(int massValue, int expectedValue) {
			Assert.Equal(expectedValue, Solution.SolveFromMass(massValue));
		}
	}
}
