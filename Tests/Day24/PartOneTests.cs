using AdventOfCodeLib.Day24.Part1;
using System.Linq;
using Xunit;

namespace Tests.Day24 {
	public class PartOneTests {
		[Theory]
		[InlineData("....#\n#..#.\n#..##\n..#...\n#....", 2129920)]
		public void TestMatchingLayout(string initialState, int expectedOutput) {
			Assert.Equal(expectedOutput, Solution.Solve(initialState.Split("\n").ToList()));
		}
	}
}
