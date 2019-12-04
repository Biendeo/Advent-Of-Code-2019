using AdventOfCodeLib.Day04.Part1;
using Xunit;

namespace Tests.Day04 {
	public class PartOneTests {
		[Theory]
		[InlineData(111111, true)]
		[InlineData(223450, false)]
		[InlineData(123789, false)]
		public void TestProgram(int password, bool expectedResult) {
			Assert.Equal(expectedResult, Solution.IsPasswordValid(password));
		}
	}
}
