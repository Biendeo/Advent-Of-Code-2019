using AdventOfCodeLib.Day04.Part2;
using Xunit;

namespace Tests.Day04 {
	public class PartTwoTests {
		[Theory]
		[InlineData(112233, true)]
		[InlineData(123444, false)]
		[InlineData(111122, true)]
		public void TestProgram(int password, bool expectedResult) {
			Assert.Equal(expectedResult, Solution.IsPasswordValid(password));
		}
	}
}
