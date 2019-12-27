using AdventOfCodeLib.Day24.Part2;
using System.Linq;
using Xunit;

namespace Tests.Day24 {
	public class PartTwoTests {
		[Theory]
		[InlineData("....#\n#..#.\n#..##\n..#...\n#....", 10, 99)]
		public void TestMatchingLayout(string initialState, int minutes, int expectedOutput) {
			Assert.Equal(expectedOutput, Solution.Solve(initialState.Split("\n").ToList(), minutes));
		}
	}
}
